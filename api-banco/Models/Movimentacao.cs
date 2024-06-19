using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace api_banco.Models
{
    [Table("movimentacoes")]
    public class Movimentacao
    {
        [Key]
        public int id { get; set; }

        public string tipo_mov { get; set; }

        public int numero_conta { get; set; }

        public int numero_conta_dest { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal valor_mov { get; set; }

        public bool usa_credito { get; set; }

        public Movimentacao(string tipo_mov, int numero_conta, int numero_conta_dest, decimal valor_mov, bool usa_credito)
        {
            this.tipo_mov = tipo_mov;
            this.numero_conta = numero_conta;
            this.numero_conta_dest = numero_conta_dest;
            this.valor_mov = valor_mov;
            this.usa_credito = usa_credito;
        }
    }
}
