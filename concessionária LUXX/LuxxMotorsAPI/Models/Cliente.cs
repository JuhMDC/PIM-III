using System.ComponentModel.DataAnnotations;

namespace LuxxMotorsAPI.Models
{
    // DOC: Classe Cliente usada no backend para representar esta entidade ou controller.
public class Cliente
    {
        [Key]
        public int Id_cliente { get; set; }
        public string Nome_completo { get; set; } = string.Empty;
        public DateTime Data_nascimento { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal? Renda_aproximada { get; set; }
    }
}