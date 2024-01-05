using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nimbus.Api.Models
{
    public class UserNimbus
    {
        [Key]
        public int idUsuario { get; set; }
        public string nmNomeUsuario {get; set;} = "NullUser";
        public string nmSenha {get; set;} = "DefaultPassword";
        public string nmNomeEmailUsuario {get; set;} = "NullEmail";
        public string nrTelefone {get; set;} = "(99) 9999-9999#";
        public DateTime dtCriacao {get; set;}
        public DateTime dtAlteracao {get; set;}
    }
}