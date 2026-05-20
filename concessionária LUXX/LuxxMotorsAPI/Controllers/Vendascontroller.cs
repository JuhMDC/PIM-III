using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuxxMotorsAPI.Data;
using LuxxMotorsAPI.Models;

namespace LuxxMotorsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // DOC: Classe VendasController usada no backend para representar esta entidade ou controller.
public class VendasController : ControllerBase
    {
        private readonly AppDbContext _context;
        public VendasController(AppDbContext context) { _context = context; }

            /// <summary>Endpoint da API para esta operação.</summary>
    /// <summary>Endpoint da API para esta operação.</summary>
[HttpGet]
        public async Task<ActionResult<IEnumerable<Venda>>> GetVendas()
        {
            try 
            {
                var listaDeVendas = await _context.VENDA.ToListAsync(); 
                return Ok(listaDeVendas);
            }
            catch (Exception ex)
            {
                // IMPRIME O ERRO REAL NO TERMINAL DO DOTNET RUN
                Console.WriteLine($"[ERRO CRÍTICO EM VENDAS]: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[DETALHE DO BANCO - VENDAS]: {ex.InnerException.Message}");
                }
                return StatusCode(500, $"Erro interno na tabela Venda: {ex.Message}");
            }
        }
    }
}