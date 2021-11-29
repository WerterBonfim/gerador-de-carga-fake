using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Werter.GeradorDeCargaFake.API.Parametros;

namespace Werter.GeradorDeCargaFake.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CargaController : ControllerBase
    {
        [HttpPost]
        public IActionResult GerarTabelaFake([FromBody] CargaCommand command)
        {
            try
            {
                if (!command.EstaValido())
                    return BadRequest(command.ListarErros());


                return Ok();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, command.ListarErros());
            }
        }
    }
}