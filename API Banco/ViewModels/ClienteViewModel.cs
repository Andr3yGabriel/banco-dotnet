using System.ComponentModel.DataAnnotations.Schema;

namespace API_Banco.ViewModels
{
    public class ClienteViewModel
    {
        public string nome { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal saldo { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal credito { get; set; }
    }
}
