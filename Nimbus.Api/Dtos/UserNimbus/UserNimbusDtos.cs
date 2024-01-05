using Nimbus.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nimbus.Api.Models;

namespace Nimbus.Api.Dtos.UserNimbus
{
    public class postDTOuser
    {
        public string nmNomeUsuario {get; set;} = "NullUser";
        public string nmSenha {get; set;} = "DefaultPassword";
        public string nmNomeEmailUsuario {get; set;} = "NullEmail";
        public string nrTelefone {get; set;} = "99999999999";
    }
    public class authorizationUser
    {
        public object? user { get; set; }
        public string? token { get; set; }

    }
}