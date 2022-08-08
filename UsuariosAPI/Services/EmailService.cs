using FluentResults;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class EmailService
    {
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void EnviarEmail(string[] destinatario, string assunto, int usuarioId, string codigoAtivacao)
        {
            Mensagem mensagem = new Mensagem(destinatario, assunto, usuarioId, codigoAtivacao);
            var mensagemEmail = CriaCorpoEmail(mensagem);
            Enviar(mensagemEmail);
        }

        private void Enviar(MimeMessage mensagemEmail)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    //Configurando o servidor de email
                    client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"), _configuration.GetValue<int>("EmailSettings:Port"), false);
                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"), _configuration.GetValue<string>("EmailSettings:Password"));

                    client.Send(mensagemEmail);
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage CriaCorpoEmail(Mensagem mensagem)
        {
            var mensagemEmail = new MimeMessage();
            mensagemEmail.From.Add(new MailboxAddress("teste",_configuration.GetValue<string>("EmailSettings:From")));
            mensagemEmail.To.AddRange(mensagem.Destinatario);
            mensagemEmail.Subject = mensagem.Assunto;
            mensagemEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = mensagem.Conteudo
            };

            return mensagemEmail;
        }
    }
}
