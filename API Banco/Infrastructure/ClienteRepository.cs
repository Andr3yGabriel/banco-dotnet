using API_Banco.Models;

namespace API_Banco.Infrastructure
{
    public class ClienteRepository : InterfaceClienteRepository
    {
        private readonly ConnectionContext _connectionContext = new ConnectionContext();

        public void Add(Cliente cliente)
        {
            _connectionContext.Add(cliente);
            _connectionContext.SaveChanges();
        }

        public void Deposito(int numero_conta, decimal valor)
        {

            Cliente _usuario = _connectionContext.Cliente.Find(numero_conta);

            if (_usuario != null)
            {
                _usuario.saldo += valor;
                _connectionContext.SaveChanges();
            }
        }

        public List<Cliente> Get()
        {
            return _connectionContext.Cliente.ToList();
        }

        public Cliente? Get(int numero_conta)
        {
            return _connectionContext.Cliente.Find(numero_conta);
        }

        public void Saque(int numero_conta, decimal valor)
        {
            Cliente _usuario = _connectionContext.Cliente.Find(numero_conta);


            if (_usuario != null)
            {
                if ((_usuario.saldo + _usuario.credito) > valor)
                {
                    _usuario.saldo -= valor;
                    _connectionContext.SaveChanges();
                }
            }
        }

        public void Transferencia(int numero_conta, int numero_conta_dest, decimal valor)
        {
            Cliente _usuario = _connectionContext.Cliente.Find(numero_conta);
            Cliente _destino = _connectionContext.Cliente.Find(numero_conta_dest);

            if (_usuario != null && _destino != null)
            {
                if (_usuario.saldo + _usuario.credito > valor)
                {
                    _usuario.saldo -= valor;
                    _destino.saldo += valor;
                    _connectionContext.SaveChanges();
                }
            } 
        }
    }
}
