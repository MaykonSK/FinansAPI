using Finans.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Net.Http;

namespace Finans.Services
{
    public class UsuarioService 
    {
        private FinansContext _context;
        private static IWebHostEnvironment _environment;

        public UsuarioService(FinansContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<string> UploadFile(IFormFile file, int userId)
        {
            if (file.Length > 0)
            {
                try
                {
                    string diretorio = Path.Combine(_environment.ContentRootPath, "Imagens/ImgUsers");
                    string filePath = Path.Combine(diretorio, file.FileName);
                    await using var stream = new FileStream(filePath, FileMode.Create);
                    file.CopyTo(stream);
                    return "\\ImgUsers\\" + file.FileName;
                }
                catch (Exception)
                {

                    return "Não foi possivel salvar a imagem";
                }
            } else
            {
                return "Ocorreu uma falha no envio do arquivo...";
            }
        }

        public Usuario recuperarDadosUsuario(int id)
        {
            Usuario usuario = _context.Usuarios.FirstOrDefault(x => x.Id == id);
            usuario.Imagem = this.GetFile(usuario.Imagem);
            return usuario;
        }

        public string GetFile(string imageName)
        {
            string path = Path.Combine(_environment.ContentRootPath, "Imagens/ImgUsers/" + imageName);
            byte[] b = System.IO.File.ReadAllBytes(path);
            return Convert.ToBase64String(b);
        }

    }
}
