using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Werter.GeradorDeCargaFake.API.Parametros;
using Werter.GeradorDeCargaFake.API.Servico;

namespace Werter.GeradorDeCargaFake.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CargaController : ControllerBase
    {
        private readonly ServicoGeradorDeCarga _servicoGeradorDeCarga;

        public CargaController(ServicoGeradorDeCarga servicoGeradorDeCarga)
        {
            _servicoGeradorDeCarga = servicoGeradorDeCarga;
        }

        [HttpPost]
        public IActionResult GerarTabelaFake([FromBody] CargaCommand command)
        {
            try
            {
                if (!command.EstaValido())
                    return BadRequest(command.ListarErros());

                _servicoGeradorDeCarga.GerarCargaFake(command);

                return Ok("Carga efetua com sucesso");

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}