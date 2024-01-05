using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nimbus.Api.Dtos.NimbusBox;
using Microsoft.AspNetCore.Mvc;
using Nimbus.Api.Dtos.UserNimbus;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Nimbus.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserNimbusController : ControllerBase
    {
        new DefaultResponse Response = new();
        

        public UserNimbusController(IConfiguration config, UserNimbusService service) 
        {
            _config = config;
            this.Service = service;
        }
        private IConfiguration _config { get; set; }
        private UserNimbusService Service {get; set;}
        
        [HttpGet]
        [Route("GetAllUsers")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get() {
            try 
            {
                Response.data = await Service.getAllRegisters();
                Response.msg = "Lista de Usuários";
                return Ok(Response);
            }
            catch (Exception ex) {
                Response.success = false;
                Response.msg = ex.ToString();
                Response.data = null;
                return BadRequest(Response);
            }
        }

        [HttpGet]
        [Route("getUserId")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetId(int id) {
            try 
            {
                Response.data = await Service.getById(id);
                Response.msg = $"Usuário de id {id}";
                return Ok(Response);
            }
            catch (Exception ex) {
                Response.success = false;
                Response.msg = ex.Message;
                Response.data = null;
                return BadRequest(Response);
            }
        }

        [HttpPost]
        [Route("postUserNimbus")]
        public async Task<IActionResult> Post(postDTOuser obj_user) {
            try 
            {
                Response.data = await Service.addRegister(obj_user);
                Response.msg = "Usuário adicionado/alterado com sucesso";
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
        [Route("verifyToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public bool verifyToken()
        {
            return true;
        }

        [HttpGet]
        [Route("SignIn/{email}/{password}")]
        public async Task<IActionResult> SignIn(string email, string password)
        {
            try
            {
                UserNimbus model = new UserNimbus()
                {
                    nmNomeEmailUsuario = email,
                    nmSenha = password,
                };
                var user = await AuthenticationUser(model);
                if (user.idUsuario == 0) throw new Exception("Usuário inválido");
                authorizationUser finalUser = new()
                {
                    user = user,
                    token = GenerateToken(model)
                };
                return Ok(finalUser);
            } 
            catch (Exception ex)
            {
                Response.success = false;
                Response.msg = ex.Message;
                Response.data = null;
                return BadRequest(Response);
            }
        }

        private string GenerateToken(UserNimbus model)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<UserNimbus> AuthenticationUser(UserNimbus user)
        {
            return await Service.getUserByPasswordAndEmail(user);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int id) {
            try 
            {
                Response.data = await Service.deleteRegister(id);
                Response.msg = "Usuário removido com sucesso";
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