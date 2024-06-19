namespace api_banco.Models
{
    public interface InterfaceMovimentacaoRepository
    {
        void Add(Movimentacao movimentacao);

        List<Movimentacao> Get();

        List<Movimentacao> GetMovimentacoes(int numero_conta);
    }
}
