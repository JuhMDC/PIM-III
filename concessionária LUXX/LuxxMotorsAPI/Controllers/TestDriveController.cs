using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuxxMotorsAPI.Data;
using LuxxMotorsAPI.Models;

namespace LuxxMotorsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // DOC: Classe TestDriveController usada no backend para representar esta entidade ou controller.
public class TestDriveController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TestDriveController(AppDbContext context) 
        { 
            _context = context; 
        }

            /// <summary>Endpoint da API para esta operação.</summary>
    /// <summary>Endpoint da API para esta operação.</summary>
[HttpGet]
        public async Task<ActionResult<IEnumerable<TestDrive>>> GetTestDrives()
        {
            try 
            {
                // Certifique-se de que este nome após o '_context.' é igual ao do AppDbContext
                var lista = await _context.TESTDRIVE.ToListAsync(); 
                return Ok(lista);
            }
            catch (Exception ex)
            {
                // Esse log vai imprimir o erro exato no seu terminal do dotnet run
                Console.WriteLine($"[ERRO NO CONTEXTO TESTDRIVE]: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[DETALHE DO BANCO]: {ex.InnerException.Message}");
                }
                
                return StatusCode(500, $"Erro interno no Test Drive: {ex.Message}");
            }
        }

            /// <summary>Endpoint da API para esta operação.</summary>
    /// <summary>Endpoint da API para esta operação.</summary>
[HttpPost]
        public async Task<ActionResult<TestDrive>> PostAgendamento(TestDrive agendamento)
        {
            Console.WriteLine("Recebendo agendamento: " + System.Text.Json.JsonSerializer.Serialize(agendamento));
            agendamento.Status = "Pendente";
            _context.TESTDRIVE.Add(agendamento);
            await _context.SaveChangesAsync();
            return Ok(agendamento);
        }
    }

    public class TestDriveRequest
    {
        public int id_cliente { get; set; }
        public int id_veiculo { get; set; }
        public int? id_vendedor { get; set; } // Opcional no início
        public string horario { get; set; } = string.Empty;
        public DateTime data_agendamento { get; set; }
        public string status { get; set; } = "Agendado";
    }
}