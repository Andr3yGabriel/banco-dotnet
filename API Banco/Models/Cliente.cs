using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Banco.Models
{
    [Table("cliente")]
    public class Cliente
    {
        [Key]
        public int numero_conta { get; set; }

        public string nome { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal saldo { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal credito { get; set; }

        public Cliente(string nome, decimal saldo, decimal credito)
        {
            this.nome = nome;
            this.saldo = saldo;
            this.credito = credito;
        }
    }
}
