using System.ComponentModel.DataAnnotations.Schema;

namespace api_banco.ViewModel
{
    public class ClienteViewModel
    {
        public string nome { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal saldo { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal credito { get; set; }

        public DateTime data_criacao { get; set; }
    }
}
