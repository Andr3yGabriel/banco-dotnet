using api_banco.Models;
using api_banco.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace api_banco.Controllers
{
    [ApiController]
    [Route("/Movimentacoes")]
    public class MovimentacaoController : ControllerBase
    {
        private readonly InterfaceMovimentacaoRepository _MovimentacaoRepository;

        public MovimentacaoController(InterfaceMovimentacaoRepository movimentacaoRepository)
        {
            _MovimentacaoRepository = movimentacaoRepository;
        }

        [HttpPost("adicionar-movimentacao")]
        public IActionResult AdicionarMovimentacao([FromBody] MovimentacaoViewModel movimentacaoViewModel)
        {
            var movimentacao = new Movimentacao(movimentacaoViewModel.tipo_mov, movimentacaoViewModel.numero_conta, movimentacaoViewModel.numero_conta_dest, movimentacaoViewModel.valor_mov, movimentacaoViewModel.usa_credito);
            try
            {
                _MovimentacaoRepository.Add(movimentacao);
                return Ok("Movimentacao adicionada");
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPost("pegar-extrato")]
        public IActionResult PegarExtrato(int numero_conta)
        {
            var movimentacoes = _MovimentacaoRepository.GetMovimentacoes(numero_conta);

            if (movimentacoes == null || movimentacoes.Count == 0)
            {
                return NotFound("Sem movimentações");
            }
            else
            {
                return Ok(movimentacoes);
            }
        }

        [HttpGet("listar-movimentacoes")]
        public IActionResult ListarMovimentacoes()
        {
            try
            {
                var movimentacoes = _MovimentacaoRepository.Get();
                return Ok(movimentacoes);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
