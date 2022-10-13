using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UsuariosAPI.Models;
using UsuariosAPI.Models.DTO;

namespace UsuariosAPI.Services
{
    public class UsuarioService
    {
        private IMapper _mapper;
        private UserManager<CustomIdentityUser> _userManager; //possui diversos metodos para o geneciamento de usuario //serve para cadastrar usuario
        private SignInManager<CustomIdentityUser> _signInManager; //serve para fazer login
        private TokenService _tokenService;
        private EmailService _emailService;

        private FinansDBContext _finansDBContext;

        public UsuarioService(IMapper mapper, UserManager<CustomIdentityUser> userManager, SignInManager<CustomIdentityUser> signInManager, TokenService tokenService, EmailService emailService, FinansDBContext finansDBContext)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
            _finansDBContext = finansDBContext;
        }

        public Result cadastrarUsuario(CreateUsuarioDto usuarioDto)
        {
            //passando os dados do DTO para o usuario
            Usuario usuario = _mapper.Map<Usuario>(usuarioDto);

            #region Cria UserName automatico
            string name = usuario.Name.Trim();
            string[] words = name.Split(' ');

            var word1 = "";
            var word2 = "";
            var w1 = "";
            var w2 = "";

            var username = "";

            if (words.Length > 1)
            {
                word1 = words[0];
                word2 = words[1];

                w1 = word1.Substring(0, 2);
                w2 = word2.Substring(0, 2);

                Random random = new Random();
                int numero = random.Next(999, 1999);

                username = (w2 + w1 + numero).ToUpper();

            } else
            {
                word1 = words[0];
                w1 = word1.Substring(0, 2);

                Random random = new Random();
                int numero = random.Next(999, 1999);

                username = (w1 + numero).ToUpper();
            }

            usuario.Username = username;

            #endregion

            #region Cadastra usuário
            //passando os dados do usuario para o identity
            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);

            //pesquisa o usuario pelo email
            var userEmail = _signInManager.UserManager.FindByEmailAsync(usuarioIdentity.Email).Result;

            if (userEmail == null)
            {
                //obtem o resultado da criação    //criando o usuario no identity
                Task<IdentityResult> resultado = _userManager.CreateAsync(usuarioIdentity, usuarioDto.Password);

                if (resultado.Result.Succeeded)
                {
                    _userManager.AddToRoleAsync(usuarioIdentity, "regular");

                    //recuperar codigo de autenticação de e-mail
                    var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;

                    #region Envia e-mail
                    var encodeCode = HttpUtility.UrlEncode(code);
                    _emailService.EnviarEmailAtivacao(new[] { usuarioIdentity.Email }, "Ativação de Conta", usuarioIdentity.Id, encodeCode);
                    #endregion

                    return Result.Ok().WithSuccess("Cadastrado efetuado! Código de ativação enviado para o e-mail");
                }
                return Result.Fail("Falha ao cadastrar usuário");
            }
            return Result.Fail("E-mail já cadastrado");
            #endregion
        }

        public Result logarUsuario(LoginDto loginDto)
        {
            Login login = _mapper.Map<Login>(loginDto);
            #region login
            //recupera o UserName pelo email
            var user = _signInManager.UserManager.FindByEmailAsync(login.Email).Result.UserName;

            if (user != null)
            {
                //efetuar autenticação via username e senha
                var resultado = _signInManager.PasswordSignInAsync(user, login.Password, false, false);

                if (resultado.Result.Succeeded)
                {
                    //recuperar identity user
                    var identityUser = _signInManager.UserManager.Users.FirstOrDefault(usuario => usuario.NormalizedUserName == user.ToUpper());

                    //gerar token e retornar para o osuaurio
                    Token token = _tokenService.CreateToken(identityUser, _signInManager.UserManager.GetRolesAsync(identityUser).Result.FirstOrDefault());

                    //retorna o token para o controller
                    return Result.Ok().WithSuccess(token.Value);
                }
                return Result.Fail("E-mail ou senha incorreto");
            }
            return Result.Fail("Conta não encontrada");
            #endregion
        }

        public Result deslogarUsuario()
        {
            #region Logout
            var resultado = _signInManager.SignOutAsync();
            if (resultado.IsCompletedSuccessfully)
            {
                return Result.Ok();
            }

            return Result.Fail("Logout falhou");
            #endregion
        }

        public Result ativaContaUsuario(AtivaConta request)
        {
            #region Ativação de conta
            //recupera usuario identity
            var userIdentity = _userManager.Users.FirstOrDefault(user => user.Id == request.UsuarioId);

            //confirmar o e-mail
            var identityResult = _userManager.ConfirmEmailAsync(userIdentity, request.CodigoAtivacao).Result;

            if (identityResult.Succeeded)
            {
                #region Salva ID e e-mail do usuário no banco Finans
                //salva usuario no outro banco
                UsuarioFinans usuario = new UsuarioFinans
                {
                    Id = userIdentity.Id,
                    Nome = userIdentity.UserName,
                };
                _finansDBContext.Usuarios.Add(usuario);
                _finansDBContext.SaveChanges();

                return Result.Ok().WithSuccess("Conta ativada com sucesso");
                #endregion
            }

            return Result.Fail("Falha ao ativar conta de usuário");
            #endregion
        }

        public Result solicitaResetSenha(SolicitaResetSenha request)
        {
            #region Solicita nova senha
            //recupera usuario identity pelo email
            var userIdentity = _userManager.Users.FirstOrDefault(user => user.Email == request.Email);

            if (userIdentity != null)
            {
                //solicita token para redefinição de senha
                string codigoDeRecuperacao = _signInManager.UserManager.GeneratePasswordResetTokenAsync(userIdentity).Result;

                #region Envia e-mail
                //formata o token
                var encodeCode = HttpUtility.UrlEncode(codigoDeRecuperacao);
                _emailService.EnviarEmailSenha(new[] { userIdentity.Email }, "Redefinição de senha", encodeCode);
                #endregion

                return Result.Ok().WithSuccess("Redefinição de senha enviado para o e-mail");
            }

            return Result.Fail("E-mail não encontrado");
            #endregion
        }

        public Result redefinirSenha(RedefinicaoSenha request)
        {
            #region Redefine a senha
            //recupera usuario identity pelo email
            var userIdentity = _userManager.Users.FirstOrDefault(user => user.Email == request.Email);

            if (userIdentity != null)
            {
                IdentityResult resultado = _signInManager.UserManager.ResetPasswordAsync(userIdentity, request.Token, request.Password).Result;

                if (resultado.Succeeded)
                {
                    return Result.Ok().WithSuccess("Senha redefinida com sucesso");
                }
            }

            return Result.Fail("E-mail não encontrado");
            #endregion
        }
    }
}
