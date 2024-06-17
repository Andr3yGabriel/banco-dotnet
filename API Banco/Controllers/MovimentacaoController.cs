using API_Banco.Models;
using API_Banco.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API_Banco.Controllers
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

            _MovimentacaoRepository.Add(movimentacao);

            return Ok("Movimentacao adicionada");
        }

        [HttpPost("pegar-extrato")]
        public IActionResult PegarExtrato(int numero_conta)
        {
            var movimentacoes = _MovimentacaoRepository.GetMovimentacoes(numero_conta);

            if (movimentacoes == null || movimentacoes.Count == 0)
            {
                return NotFound("Sem movimentações");
            } else
            {
                return Ok(movimentacoes);
            }
        }

        [HttpGet("listar-movimentacoes")]
        public IActionResult ListarMovimentacoes()
        {
            var movimentacoes = _MovimentacaoRepository.Get();

            return Ok(movimentacoes);
        }
    }
}
