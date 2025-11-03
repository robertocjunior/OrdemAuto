using Domain.Interfaces;
using Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParceiroNegocioController : ControllerBase
    {
        private readonly IParceiroService _parceiroService;
        public ParceiroNegocioController(IParceiroService parceiroService)
        {
            _parceiroService = parceiroService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Consultar(int id) {
            var result = await _parceiroService.Consultar(id);
            return Ok(result);
        }
        [HttpPost("adicionar")]
        public async Task<IActionResult> Adicionar([FromBody] DTOParceiroNegocioResponse dto)
        {
            await _parceiroService.Adicionar(dto);
            return Ok();
        }

        [HttpPost("editar")]
        public async Task<IActionResult> Editar([FromBody] DTOParceiroNegocioResponse dto)
        {
            await _parceiroService.Editar(dto);
            return Ok();
        }
    }
}
