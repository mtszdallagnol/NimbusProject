using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Nimbus.Api.Models
{
    public class NimbusBox
    {
        [Key]
        public int idNimbus {get; set;}
        public int idUserNimbus { get; set; }
        public string? macAddress {get; set;} = string.Empty;
        public double? nrTemperatura {get; set;}
        public double? nrPressao {get; set;}
        public double? nrUmidade {get; set;}
        public double? nrEspectroLuz {get; set;}
        public double? nrInfraVermelho {get; set;}
        public double? nrLuzUv {get; set;}
        public double? nrAguaAltura {get; set;}
        public double? nrVelVento { get; set; }
        public DateTime dtCriacao {get; set;}
    }
}