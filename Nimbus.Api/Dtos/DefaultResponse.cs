using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nimbus.Api.Models
{
    public class DefaultResponse
    {
        public bool success {get; set;} = true;
        public string msg {get; set;} = string.Empty; 
        public object? data {get; set;}
    }
}