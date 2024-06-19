namespace api_banco.Models
{
    public interface InterfaceClienteRepository
    {
        void Saque(int numero_conta, decimal valor);

        void Deposito(int numero_conta, decimal valor);

        void Transferencia(int numero_conta, int numero_conta_dest, decimal valor);

        void Add(Cliente cliente);

        List<Cliente> Get();

        Cliente? Get(int numero_conta);
    }
}
