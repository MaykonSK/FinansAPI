using Newtonsoft.Json;
using UsuariosAPI.Utils;

namespace UsuariosAPI.Shared
{
    public class RecaptchaValidate
    {
        public bool Validate(string encodedResponse)
        {
            if (string.IsNullOrEmpty(encodedResponse))
                return false;
            var secret = "6Lcmq3MiAAAAAL4bPqE4TKdWsSGV6NkcfehFkHxW";
            var client = new System.Net.WebClient();
            var googleReply = client.DownloadString(
                $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={encodedResponse}");
            return JsonConvert.DeserializeObject<RecaptchaResponse>(googleReply).Success;
        }
    }
}
