using apiTaskList.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace apiTaskList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UsuarioController : ControllerBase
    {
        private readonly UnitOfService service;

        public UsuarioController(UnitOfService service)
        {
            this.service = service;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterUsuarioDTO usuario)
        {
            var result = await service.UsuarioService.Register(usuario);

            if (result.IsSuccess)
            {
                return Ok(result.Successes.FirstOrDefault());
            }
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginUsuarioDTO usuario)
        {
            var result = await service.UsuarioService.Login(usuario);

            if (result.IsSuccess)
            {
                return Ok(result.Successes.FirstOrDefault());
            }
            return BadRequest(result.Errors.FirstOrDefault());
        }
    }
}
