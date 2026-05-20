using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuxxMotorsAPI.Data; // Ou onde quer que seu AppDbContext esteja
using LuxxMotorsAPI.Models;
using Microsoft.EntityFrameworkCore.Update.Internal; // Onde está a classe Agendamento

namespace LuxxMotorsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // DOC: Classe AgendamentosController usada no backend para representar esta entidade ou controller.
public class AgendamentosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AgendamentosController(AppDbContext context)
        {
            _context = context;
        }

            /// <summary>Endpoint da API para esta operação.</summary>
    /// <summary>Endpoint da API para esta operação.</summary>
[HttpPost]
        public async Task<IActionResult> CriarAgendamento([FromBody] TestDrive agendamento) 
        {
            Console.WriteLine("Recebendo agendamento: " + System.Text.Json.JsonSerializer.Serialize(agendamento));
            if (agendamento == null) return BadRequest("Dados inválidos.");

            _context.TESTDRIVE.Add(agendamento);
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Agendamento realizado!" });
        }
    }
}