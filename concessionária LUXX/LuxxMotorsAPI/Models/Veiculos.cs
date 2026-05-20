using System.ComponentModel.DataAnnotations;

namespace LuxxMotorsAPI.Models
{
    // DOC: Classe Veiculo usada no backend para representar esta entidade ou controller.
public class Veiculo
    {
        [Key] 
        public int Id_veiculo { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int Ano { get; set; }
        public string? Capacidade { get; set; }
        public decimal Preco { get; set; }
        public string Cor { get; set; } = string.Empty;
        public string Cambio { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int Quilometragem { get; set; }
        public string? Imagem { get; set; }
    }
}