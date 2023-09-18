using apiTaskList.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace apiTaskList.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Produces("application/json")]
    public class TarefaController : ControllerBase
    {
        private readonly UnitOfService service;

        public TarefaController(UnitOfService service)
        {
            this.service = service;
        }

        [HttpGet("")]
        public ActionResult Get([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var result = service.TarefaService.Get(pageNumber, pageSize);

            if(result.IsSuccess)
            {
                return Ok(result.Successes.FirstOrDefault());
            }
            return BadRequest(result.Errors.FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Post([FromBody] CreateTarefaDTO tarefa)
        {
            var result = service.TarefaService.Post(tarefa);

            if (result.IsSuccess)
            {
                return Ok(result.Successes.FirstOrDefault());
            }
            return BadRequest(result.Errors.FirstOrDefault());
        }
    }
}
