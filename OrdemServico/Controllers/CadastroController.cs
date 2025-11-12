using Domain.Interfaces;
using Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Invoicing.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CadastroController : ControllerBase
    {
        private readonly ICadastroService _cadastroService;

        public CadastroController(ICadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }
        [HttpGet("TodosCadastros")]
        public async Task<ActionResult> ConsultarTodos()
        {
            var result = await _cadastroService.ConsultarTodos();
            return Ok(result);
        }

        [HttpGet("pecas/{id}")]
        public async Task<ActionResult> ConsultarPecas(int id)
        {
            var result = await _cadastroService.ConsultarPecas(id);
            return Ok(result);
        }

        [HttpPost("pecas/adicionar")]
        public async Task<IActionResult> AdicionarPecas([FromBody] DTOPecasResponse dto)
        {
            await _cadastroService.AdicionarPecas(dto);
            return Ok();
        }

        [HttpPut("pecas/editar")]
        public async Task<IActionResult> EditarPecas([FromBody] DTOPecasResponse dto)
        {
            await _cadastroService.EditarPecas(dto);
            return Ok();
        }
        [HttpGet("veiculos/PesquisarVeiculos")]
        public async Task<ActionResult> PesquisarVeiculos()
        {
            var result = await _cadastroService.PesquisarVeiculos();
            return Ok(result);
        }
        [HttpGet("veiculos/{id}")]
        public async Task<ActionResult> ConsultarVeiculo(int id)
        {
            var result = await _cadastroService.ConsultarVeiculo(id);
            return Ok(result);
        }

        [HttpPost("veiculos/adicionar")]
        public async Task<IActionResult> AdicionarVeiculo([FromBody] DTOVeiculoResponse dto)
        {
            await _cadastroService.AdicionarVeiculo(dto);
            return Ok();
        }

        [HttpPut("veiculos/editar")]
        public async Task<IActionResult> EditarVeiculo([FromBody] DTOVeiculoResponse dto)
        {
            await _cadastroService.EditarVeiculo(dto);
            return Ok();
        }
    }
}
