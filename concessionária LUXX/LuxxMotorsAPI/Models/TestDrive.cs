using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuxxMotorsAPI.Models
{
    [Table("TESTDRIVE")]
    // DOC: Classe TestDrive usada no backend para representar esta entidade ou controller.
public class TestDrive
    {
        [Key]
        public int Id_testdrive { get; set; }
        
        public int Id_cliente { get; set; }
        public int Id_veiculo { get; set; }
        
        public int? Id_vendedor { get; set; } 
        
        public TimeSpan Horario { get; set; }
        
        public DateTime Data_agendamento { get; set; }
        public string Status { get; set; } = "Agendado";
    }
}