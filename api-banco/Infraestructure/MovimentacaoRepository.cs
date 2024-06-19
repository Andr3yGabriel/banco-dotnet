using api_banco.Models;

namespace api_banco.Infraestructure
{
    public class MovimentacaoRepository : InterfaceMovimentacaoRepository
    {
        private readonly ConnectionContext _connectionContext = new ConnectionContext();

        public void Add(Movimentacao movimentacao)
        {
            _connectionContext.Add(movimentacao);
            _connectionContext.SaveChanges();
        }

        public List<Movimentacao> Get()
        {
            return _connectionContext.Movimentacao.ToList();
        }

        public List<Movimentacao> GetMovimentacoes(int numero_conta)
        {
            return _connectionContext.Movimentacao.Where(m => m.numero_conta == numero_conta).ToList();
        }
    }
}
