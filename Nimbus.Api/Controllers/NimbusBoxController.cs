using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nimbus.Api.Dtos.NimbusBox;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using System.Diagnostics.CodeAnalysis;

namespace Nimbus.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class NimbusBoxController : ControllerBase
    {
        new DefaultResponse Response = new();
        
        public NimbusBoxController(NimbusBoxService service) 
        {
            this.Service = service;
        }
        private NimbusBoxService Service {get; set;}
        
        [HttpGet]
        [Route("getAllNimbusBox")]
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
        
        [HttpGet]
        [Route("getNimbusId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetId(int id) {
            try 
            {
                Response.data = await Service.getById(id);
                Response.msg = $"Produto de id {id}";
                return Ok(Response);
            }
            catch (Exception ex) {
                Response.success = false;
                Response.msg = ex.Message;
                Response.data = null;
                return BadRequest(Response);
            }
        }

        [HttpGet]
        [Route("getAllNimbusDataMedium")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> getAllNimbusDataMedium()
        {
            try
            {
                Response.data = await Service.getAllNimbusDataMedium();
                Response.msg = $"A média de todos os produtos Nimbos";
                return Ok(Response);
            }
            catch (Exception ex) 
            {
                Response.success = false;
                Response.msg = ex.Message;
                Response.data = null;
                return BadRequest(Response);
            }
        }

        [HttpGet]
        [Route("getAllNimbusMediumById")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> getAllNimbusDataMediumById(int idUserNimbus)
        {
            try
            {
                Response.data = await Service.getAllNimbusDataMediumById(idUserNimbus);
                Response.msg = $"A média de todos os produtos Nimbus relacionados ao usuário de ID {idUserNimbus}";
                return Ok(Response);
            }
            catch (Exception ex)
            {
                Response.success = false;
                Response.msg = ex.Message;
                Response.data = null;
                return BadRequest(Response);
            }
        }

        [HttpGet]
        [Route("postNimbusProduct")]
        public async Task<IActionResult> Post(int idUserNimbus, float nrTemperatura, float nrPressao, float nrUmidade, float nrLuzUv, float nrEspectroLuz, float nrInfraVermelho, float nrVelVento, float nrAguaAltura, string? macAddress) {
            try 
            {
                NimbusBox obj_nimbus = new()
                {
                    idUserNimbus = idUserNimbus,
                    nrTemperatura = nrTemperatura,
                    nrPressao = nrPressao,
                    nrUmidade = nrUmidade,
                    nrLuzUv = nrLuzUv,
                    nrEspectroLuz   = nrEspectroLuz,    
                    nrInfraVermelho = nrInfraVermelho,      
                    nrVelVento  = nrVelVento,   
                    nrAguaAltura    = nrAguaAltura,
                    macAddress = macAddress
                };
                Response.data = await Service.addRegister(obj_nimbus);
                Response.msg = "Produto adicionado/alterado com sucesso";
                return Ok(Response);
            }
            catch (Exception ex)
            {
                Response.success = false;
                Response.msg = ex.Message;
                Response.data = null;
                return BadRequest(Response);
            }
        }

        [HttpDelete]
        [Route("deleteNimbusBox")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id) {
            try 
            {
                Response.data = await Service.deleteRegister(id);
                Response.msg = "Produto removido com sucesso";
                return Ok(Response);
            }
            catch (Exception ex)
            {
                Response.success = false;
                Response.msg = ex.Message;
                Response.data = null;
                return BadRequest(Response);
            }
        }

    }
}