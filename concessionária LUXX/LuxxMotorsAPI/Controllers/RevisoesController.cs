using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuxxMotorsAPI.Data;
using LuxxMotorsAPI.Models;

namespace LuxxMotorsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // Isso cria a rota /api/revisoes
    // DOC: Classe RevisoesController usada no backend para representar esta entidade ou controller.
public class RevisoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RevisoesController(AppDbContext context)
        {
            _context = context;
        }

            /// <summary>Endpoint da API para esta operação.</summary>
    /// <summary>Endpoint da API para esta operação.</summary>
[HttpPost]
        public async Task<IActionResult> PostRevisao([FromBody] Revisao agendamento)
        {
            try 
            {
                if (agendamento == null) return BadRequest("Dados inválidos.");

                _context.REVISAO.Add(agendamento);
                await _context.SaveChangesAsync();

                return Ok(new { mensagem = "Sucesso!" });
            }
            catch (Exception ex)
            {
                // Mostra o erro real no terminal do dotnet run
                var erroReal = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                Console.WriteLine("❌ ERRO REVISÃO: " + erroReal);
                return StatusCode(500, erroReal);
            }
        }

            /// <summary>Endpoint da API para esta operação.</summary>
    /// <summary>Endpoint da API para esta operação.</summary>
[HttpGet]
        public async Task<ActionResult<IEnumerable<Revisao>>> GetRevisoes()
        {
            try 
            {
                var lista = await _context.REVISAO.ToListAsync();
                Console.WriteLine($"[API LOG] GetRevisoes chamado. Registros encontrados: {lista.Count}"); // LOG AQUI
                return Ok(lista);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO API] Falha ao listar revisões: {ex.Message}"); // LOG DE ERRO AQUI
                return StatusCode(500, ex.Message);
            }
        }
    }
}