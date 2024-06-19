using System.ComponentModel.DataAnnotations.Schema;

namespace api_banco.ViewModel
{
    public class MovimentacaoViewModel
    {
        public string tipo_mov { get; set; }

        public int numero_conta { get; set; }

        public int numero_conta_dest { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal valor_mov { get; set; }

        public bool usa_credito { get; set; }
    }
}
