using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nimbus.Api.Dtos.NimbusBox;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Nimbus.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NimbusUserUnionController : ControllerBase
    {
        new DefaultResponse Response = new();
        
        public NimbusUserUnionController(NimbusUserUnionService service) 
        {
            this.Service = service;
        }
        private NimbusUserUnionService Service {get; set;}
        
        [HttpGet]
        [Route("getAllUserNimbusBoxes")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get() {
            try 
            {
                Response.data = await Service.getAllRegisters();
                Response.msg = "Lista de Produtos";
                return Ok(Response);
            }
            catch (Exception ex) {
                Response.success = false;
                Response.msg = ex.Message;
                Response.data = null;
                return BadRequest(Response);
            }
        }
    }
}