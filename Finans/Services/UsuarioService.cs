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

        public async Task<string> UploadFile(IFormFile file)
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

        //public string GetFile()
        //{
        //    HttpResponseMessage response = new HttpResponseMessage();
        //    Byte[] b = (GetImageByteArray());
        //    response.Content = new ByteArrayContent(b);
        //    response.Content.LoadIntoBufferAsync(b.Length).Wait();
        //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
        //    return response;
        //}

    }
}
