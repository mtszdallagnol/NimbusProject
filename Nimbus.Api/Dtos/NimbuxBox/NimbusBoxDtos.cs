using Nimbus.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Nimbus.Api.Dtos.NimbusBox
{
    public class DefaultNimbusBox
    {
        public int idNimbus {get; set;}
        public int idUserNimbus { get; set; }
        public double? nrTemperatura {get; set;}
        public double? nrPressao {get; set;}
        public double? nrUmidade {get; set;}
        public double? nrEspectroLuz {get; set;}
        public double? nrInfraVermelho {get; set;}
        public double? nrLuzUv {get; set;}
        public double? nrAguaAltura {get; set;}
        public DateTime dtCriacao {get; set;}
    }
    public class NimbusOnlyData
    {
        public double nrTemperatura { get; set; }
        public double nrPressao { get; set; }
        public double nrUmidade { get; set; }
        public double nrEspectroLuz { get; set; }
        public double nrInfraVermelho { get; set; }
        public double nrLuzUv { get; set; }
        public double nrAguaAltura { get; set; }
        public double nrVelVento { get; set; }
    }

    public class NimbusOnlyDataWithRecent
    {
        public List<object>? listMediumMonts { get; set; }
        public NimbusOnlyData? nimbusMostRecent { get; set; }
    }

    public class NimbusOnlyDataWithRecentWithId
    {
        public List<object>? listMedium5days { get; set; }
        public NimbusOnlyData? nimbusMostRecent { get; set; }
        public int currentIdNimbusProduct { get; set; }
    }
}