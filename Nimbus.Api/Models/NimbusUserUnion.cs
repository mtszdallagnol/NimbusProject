using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nimbus.Api.Models
{
    public class NimbusUserUnion
    {
        [Key]
        public int idUserNimbus {get; set;}
        public int idUsuario { get; set; }
        public string? nmProduto { get; set; }
        public string stsProduto { get; set; } = "Off-Line";
    }
}