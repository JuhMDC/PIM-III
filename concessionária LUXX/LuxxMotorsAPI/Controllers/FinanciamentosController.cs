using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuxxMotorsAPI.Data;
using LuxxMotorsAPI.Models;

namespace LuxxMotorsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // DOC: Classe FinanciamentosController usada no backend para representar esta entidade ou controller.
public class FinanciamentosController : ControllerBase
    {
        // 1. Você precisa declarar a variável do contexto aqui
        private readonly AppDbContext _context;

        // 2. Você precisa do Construtor para "injetar" o banco de dados no Controller
        public FinanciamentosController(AppDbContext context)
        {
            _context = context;
        }

            /// <summary>Endpoint da API para esta operação.</summary>
    /// <summary>Endpoint da API para esta operação.</summary>
[HttpPost]
        public async Task<IActionResult> PostSimulacao([FromBody] Financiamento simulacao)
        {
            try 
            {
                // Verifica se os dados chegaram nulos
                if (simulacao == null) return BadRequest("Dados da simulação inválidos.");

                Console.WriteLine($"\n--- RECEBENDO SIMULAÇÃO ---");
                Console.WriteLine($"Cliente ID: {simulacao.Id_cliente}, Veículo ID: {simulacao.Id_veiculo}");

                _context.FINANCIAMENTO.Add(simulacao);
                
                await _context.SaveChangesAsync(); 

                Console.WriteLine("✅ Simulação gravada com sucesso!");
                return Ok(new { mensagem = "Simulação salva!", id = simulacao.Id_financiamento });
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ ERRO NO BANCO: " + ex.Message);
                // Se houver erro de chave estrangeira, o InnerException dirá qual ID não existe
                if (ex.InnerException != null) Console.WriteLine("Detalhe: " + ex.InnerException.Message);
                
                return StatusCode(500, new { erro = ex.Message });
            }
        }

            /// <summary>Endpoint da API para esta operação.</summary>
    /// <summary>Endpoint da API para esta operação.</summary>
[HttpGet]
        public async Task<ActionResult<IEnumerable<Financiamento>>> GetFinanciamentos()
        {
            return await _context.FINANCIAMENTO.ToListAsync();
        }
    }
}