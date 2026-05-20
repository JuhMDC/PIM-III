using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuxxMotorsAPI.Models
{
    [Table("REVISAO")]
    // DOC: Classe Revisao usada no backend para representar esta entidade ou controller.
public class Revisao
    {
        [Key]
        public int id_revisao { get; set; }
        
        [Required]
        public int id_cliente { get; set; }
        
        [Required]
        public int id_veiculo { get; set; }
        
        [Required]
        public int id_vendedor { get; set; } // Precisamos enviar um ID de vendedor válido
        
        [Required]
        public DateTime data_revisao { get; set; } = DateTime.Now;
        
        [Required]
        public string status { get; set; } = "Agendado"; // Valor padrão
        
        [Required]
        public string forma_pagamento { get; set; } = "A definir"; // Valor padrão
        
        public string servico { get; set; } = string.Empty;
    }
}