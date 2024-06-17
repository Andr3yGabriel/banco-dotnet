using API_Banco.Infrastructure;
using API_Banco.Models;
using API_Banco.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API_Banco.Controllers
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
        public IActionResult AdicionarCliente([FromForm] ClienteViewModel clienteViewModel)
        {
            var cliente = new Cliente(clienteViewModel.nome, clienteViewModel.saldo, clienteViewModel.credito);

            _ClienteRepository.Add(cliente);

            return Ok("Cliente adicionado");
        }

        [HttpPost("info-cliente")]
        public IActionResult InfoCliente(int numero_conta)
        {
            var cliente = _ClienteRepository.Get(numero_conta);

            return Ok(cliente);
        }

        [HttpGet("listar-clientes")]
        public IActionResult ListarClientes()
        {
            var clientes = _ClienteRepository.Get();

            return Ok(clientes);
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
            } else
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
                if(cliente.saldo > valor)
                {
                    _ClienteRepository.Saque(numero_conta, valor);
                    _MovimentacaoRepository.Add(new Movimentacao("Saque", numero_conta, 0, valor, false));
                    return Ok("Saque concluído!");
                } else if (cliente.saldo + cliente.credito > valor)
                {
                    _ClienteRepository.Saque(numero_conta, valor);
                    _MovimentacaoRepository.Add(new Movimentacao("Saque", numero_conta, 0, valor, true));
                    return Ok("Saque concluído com uso de crédito especial!");
                } else
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
                }else if (cliente.saldo + cliente.credito > valor)
                {
                    _ClienteRepository.Saque(numero_conta, valor);
                    _MovimentacaoRepository.Add(new Movimentacao("Transferência", numero_conta, numero_conta_dest, valor, true));
                    return Ok("Transferência concluída com uso de crédito especial!");
                }else
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
