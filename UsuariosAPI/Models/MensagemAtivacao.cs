using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace UsuariosAPI.Models
{
    public class MensagemAtivacao
    {
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public MensagemAtivacao(IEnumerable<string> destinatario, string assunto, int usuarioId, string codigo )
        {
            Destinatario = new List<MailboxAddress>();
            Destinatario.AddRange(destinatario.Select(d => new MailboxAddress("", d)));
            //Destinatario.AddRange((IEnumerable<MailboxAddress>)destinatario);
            Assunto = assunto;
            Conteudo = $"https://localhost:6001/ativa?UsuarioId={usuarioId}&CodigoAtivacao={codigo}";
        }

    }
}
