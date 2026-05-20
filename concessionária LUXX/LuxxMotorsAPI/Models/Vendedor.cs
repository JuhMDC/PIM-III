using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuxxMotorsAPI.Models
{
    [Table("VENDEDOR")] // Garante que o C# aponte para a tabela correta no SQL
    // DOC: Classe Vendedor usada no backend para representar esta entidade ou controller.
public class Vendedor
    {
        [Key] // Define como Chave Primária
        public int Id_vendedor { get; set; }
        [Required]
        public string Nome { get; set; } = string.Empty;
        public string cargo { get; set; } = string.Empty;
        // Email para login do vendedor
        public string Email { get; set; } = string.Empty;
        // Senha de login interna. Não existe coluna correspondente no banco;
        // todos os usuários internos usam a senha fixa "123".
        [NotMapped]
        public string Senha { get; set; } = "123";
    }
}