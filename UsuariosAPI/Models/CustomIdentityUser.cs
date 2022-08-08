using Microsoft.AspNetCore.Identity;
using System;

namespace UsuariosAPI.Models
{
    //Essa model serve para adicionar novos campos no cadastro do usuario
    public class CustomIdentityUser : IdentityUser<int>
    {
        //public DateTime DataNascimento { get; set; }
    }
}
