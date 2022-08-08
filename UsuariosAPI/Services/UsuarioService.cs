using AutoMapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
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

        public UsuarioService(IMapper mapper, UserManager<CustomIdentityUser> userManager, SignInManager<CustomIdentityUser> signInManager, TokenService tokenService, EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public Result cadastrarUsuario(CreateUsuarioDto usuarioDto)
        {
            //passando os dados do DTO para o usuario
            Usuario usuario = _mapper.Map<Usuario>(usuarioDto);

            //passando os dados do usuario para o identity
            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);

            //obtem o resultado da criação    //criando o usuario no identity
            Task<IdentityResult> resultado = _userManager.CreateAsync(usuarioIdentity, usuarioDto.Password);

            

            if (resultado.Result.Succeeded)
            {
                //cria role
                //var createRoleResult = _roleManager.CreateAsync(new IdentityRole<int>("admin")).Result;

                //adiciona uma role admin para o usuario
                //var usuarioRoleResult = _userManager.AddToRoleAsync(usuarioIdentity, "admin").Result;

                _userManager.AddToRoleAsync(usuarioIdentity, "regular");

                var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result; //recuperar codigo de autenticação de e-mail
                var encodeCode = HttpUtility.UrlEncode(code);

                _emailService.EnviarEmail(new[] { usuarioIdentity.Email }, "Ativação de Conta", usuarioIdentity.Id, encodeCode);
                return Result.Ok().WithSuccess(code);
            }
            return Result.Fail("Falha ao cadastrar usuário");
        }

        public Result logarUsuario(Login login)
        {
            //recupera o UserName pelo email
            var user = _signInManager.UserManager.FindByEmailAsync(login.Email).Result.UserName;

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
            return Result.Fail("Login falhou");
        }

        public Result deslogarUsuario()
        {
            var resultado = _signInManager.SignOutAsync();
            if (resultado.IsCompletedSuccessfully)
            {
                return Result.Ok();
            }

            return Result.Fail("Logout falhou");
        }

        public Result ativaContaUsuario(AtivaConta request)
        {
            //recupera usuario identity
            var userIdentity = _userManager.Users.FirstOrDefault(user => user.Id == request.UsuarioId);

            //confirmar o e-mail
            var identityResult = _userManager.ConfirmEmailAsync(userIdentity, request.CodigoAtivacao).Result;

            if (identityResult.Succeeded)
            {
                return Result.Ok();
            }

            return Result.Fail("Falha ao ativar conta de usuário");
            
        }

        public Result solicitaResetSenha(SolicitaResetSenha request)
        {
            //recupera usuario identity pelo email
            var userIdentity = _userManager.Users.FirstOrDefault(user => user.Email == request.Email);

            if (userIdentity != null)
            {
                //solicita token para redefinição de senha
                string codigoDeRecuperacao = _signInManager.UserManager.GeneratePasswordResetTokenAsync(userIdentity).Result;
                return Result.Ok().WithSuccess(codigoDeRecuperacao);
            }

            return Result.Fail("Falha ao solicitar redefinição");
        }

        public Result redefinirSenha(RedefinicaoSenha request)
        {
            //recupera usuario identity pelo email
            var userIdentity = _userManager.Users.FirstOrDefault(user => user.Email == request.Email);

            IdentityResult resultado = _signInManager.UserManager.ResetPasswordAsync(userIdentity, request.Token, request.Password).Result;
            if (resultado.Succeeded)
            {
                return Result.Ok().WithSuccess("Senha definida com sucesso");
            }

            return Result.Fail("Não foi possivel redefinir a senha");
        }
    }
}
