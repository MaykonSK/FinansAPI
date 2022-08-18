using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosAPI.Models
{
    public class MensagemRedefinirSenha
    {
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public MensagemRedefinirSenha(IEnumerable<string> destinatario, string assunto, string token)
        {
            Destinatario = new List<MailboxAddress>();
            Destinatario.AddRange(destinatario.Select(d => new MailboxAddress("", d)));
            //Destinatario.AddRange((IEnumerable<MailboxAddress>)destinatario);
            Assunto = assunto;
            Conteudo = $"http://localhost:4200/redefinir-senha?token={token}";
        }
    }
}
