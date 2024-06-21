using api_banco.Models;
using api_banco.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace api_banco.Controllers
{
    [ApiController]
    [Route("/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly InterfaceClienteRepository _ClienteRepository;
        private readonly InterfaceMovimentacaoRepository _MovimentacaoRepository;

        public ClienteController(InterfaceClienteRepository clienteRepository, InterfaceMovimentacaoRepository movimentacaoRepository)
        {
            _ClienteRepository = clienteRepository;
            _MovimentacaoRepository = movimentacaoRepository;
        }


        [HttpPost("adicionar-cliente")]
        public IActionResult AdicionarCliente([FromBody] ClienteViewModel clienteViewModel)
        {
            var cliente = new Cliente(clienteViewModel.nome, clienteViewModel.saldo, clienteViewModel.credito);
            try
            {
                _ClienteRepository.Add(cliente);
                return Ok("Cliente adicionado");
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpDelete("excluir-cliente")]
        public IActionResult ExcluirCliente(int numero_conta)
        {
            var cliente = _ClienteRepository.Get(numero_conta);
            try
            {
                if (cliente == null)
                {
                    return NotFound("Cliente inexistente");
                }
                _ClienteRepository.Delete(cliente);
                return Ok("Cliente Excluído com sucesso!");
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut("atualizar-dados")]
        public IActionResult AtualizarDados(int numero_conta, [FromBody] ClienteViewModel clienteViewModel)
        {
            try
            {
                var cliente = _ClienteRepository.Get(numero_conta);
                if (cliente == null)
                {
                    return NotFound("Cliente não encontrado!");
                }
                cliente.nome = clienteViewModel.nome;
                cliente.saldo = clienteViewModel.saldo;
                cliente.credito = clienteViewModel.credito;

                _ClienteRepository.Update(cliente);

                return Ok("Dados atualizados com sucesso!");
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPost("info-cliente")]
        public IActionResult InfoCliente(int numero_conta)
        {
            try
            {
                var cliente = _ClienteRepository.Get(numero_conta);
                return Ok(cliente);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet("listar-clientes")]
        public IActionResult ListarClientes()
        {
            try
            {
                var clientes = _ClienteRepository.Get();
                return Ok(clientes);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPost("deposito")]
        public IActionResult Deposito(int numero_conta, decimal valor)
        {
            var cliente = _ClienteRepository.Get(numero_conta);
            if (cliente != null)
            {
                _ClienteRepository.Deposito(numero_conta, valor);
                _MovimentacaoRepository.Add(new Movimentacao("Deposito", numero_conta, 0, valor, false));
                return Ok("Deposito concluído!");
            }
            else
            {
                return BadRequest("Usuario Inexistente");
            }
        }

        [HttpPost("saque")]
        public IActionResult Saque(int numero_conta, decimal valor)
        {
            var cliente = _ClienteRepository.Get(numero_conta);
            if (cliente != null)
            {
                if (cliente.saldo > valor)
                {
                    _ClienteRepository.Saque(numero_conta, valor);
                    _MovimentacaoRepository.Add(new Movimentacao("Saque", numero_conta, 0, valor, false));
                    return Ok("Saque concluído!");
                }
                else if (cliente.saldo + cliente.credito > valor)
                {
                    _ClienteRepository.Saque(numero_conta, valor);
                    _MovimentacaoRepository.Add(new Movimentacao("Saque", numero_conta, 0, valor, true));
                    return Ok("Saque concluído com uso de crédito especial!");
                }
                else
                {
                    return BadRequest("Saldo insuficiente para saque!");
                }
            }
            else
            {
                return BadRequest("Usuario Inexistente");
            }
        }

        [HttpPost("transferencia")]
        public IActionResult Transferencia(int numero_conta, int numero_conta_dest, decimal valor)
        {
            var cliente = _ClienteRepository.Get(numero_conta);
            var destino = _ClienteRepository.Get(numero_conta_dest);

            if (cliente != null && destino != null)
            {
                if (cliente.saldo > valor)
                {
                    _ClienteRepository.Transferencia(numero_conta, numero_conta_dest, valor);
                    _MovimentacaoRepository.Add(new Movimentacao("Transferência", numero_conta, numero_conta_dest, valor, false));
                    return Ok("Transferência concluída!");
                }
                else if (cliente.saldo + cliente.credito > valor)
                {
                    _ClienteRepository.Saque(numero_conta, valor);
                    _MovimentacaoRepository.Add(new Movimentacao("Transferência", numero_conta, numero_conta_dest, valor, true));
                    return Ok("Transferência concluída com uso de crédito especial!");
                }
                else
                {
                    return BadRequest("Saldo insuficiente para transferência!");
                }
            }
            else
            {
                return BadRequest("Usuario Inexistente");
            }
        }
    }
}
