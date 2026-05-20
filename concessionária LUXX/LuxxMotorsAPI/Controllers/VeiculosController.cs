using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuxxMotorsAPI.Data;
using LuxxMotorsAPI.Models;

namespace LuxxMotorsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // DOC: Classe VeiculosController usada no backend para representar esta entidade ou controller.
public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Injeta o contexto do banco na controller
        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        // Endpoint para listagem de veículos com filtros opcionais via query
            /// <summary>Endpoint da API para esta operação.</summary>
    /// <summary>Endpoint da API para esta operação.</summary>
[HttpGet]
        public async Task<ActionResult<IEnumerable<Veiculo>>> GetVeiculos(
            [FromQuery] string? marca, 
            [FromQuery] string? modelo, 
            [FromQuery] string? cor,
            [FromQuery] decimal? precoMax,
            [FromQuery] int? kmMin,
            [FromQuery] int? kmMax,
            [FromQuery] string? cambio,
            [FromQuery] string? categoria)
        {
            // Query base
            var query = _context.VEICULO.AsQueryable();

            // Filtros de texto
            if (!string.IsNullOrEmpty(marca)) query = query.Where(v => v.Marca == marca);
            if (!string.IsNullOrEmpty(modelo)) query = query.Where(v => v.Modelo == modelo);
            if (!string.IsNullOrEmpty(cor)) query = query.Where(v => v.Cor == cor);
            if (!string.IsNullOrEmpty(cambio)) query = query.Where(v => v.Cambio == cambio);
            if (!string.IsNullOrWhiteSpace(categoria)) query = query.Where(v => v.Categoria == categoria);

            // Filtros numéricos
            if (precoMax.HasValue) query = query.Where(v => v.Preco <= precoMax.Value);
            if (kmMin.HasValue) query = query.Where(v => v.Quilometragem >= kmMin.Value);
            if (kmMax.HasValue) query = query.Where(v => v.Quilometragem <= kmMax.Value);

            return await query.ToListAsync();
        }

        // Retorna valores distintos para uso em filtros no front-end
            /// <summary>Endpoint da API para esta operação.</summary>
    /// <summary>Endpoint da API para esta operação.</summary>
[HttpGet("filtros")]
        public async Task<IActionResult> GetFiltros()
        {
            try
            {
                // 1. Busca os modelos para o seletor de agendamento
                var modelosCompletos = await _context.VEICULO
                    .Select(v => new { 
                        id_veiculo = v.Id_veiculo, 
                        modelo = v.Modelo 
                    })
                    .ToListAsync();

                // 2. Busca as marcas distintas para o filtro da vitrine
                var marcasDistintas = await _context.VEICULO
                    .Select(v => v.Marca)
                    .Distinct()
                    .OrderBy(m => m) // Organiza de A-Z
                    .ToListAsync();

                // 3. Busca cores e câmbios distintos
                var coresDistintas = await _context.VEICULO
                    .Select(v => v.Cor)
                    .Distinct()
                    .ToListAsync();

                var cambiosDistintos = await _context.VEICULO
                    .Select(v => v.Cambio)
                    .Distinct()
                    .ToListAsync();

                return Ok(new {
                    veiculosCompletos = modelosCompletos,
                    marcas = marcasDistintas,
                    cores = coresDistintas,
                    cambios = cambiosDistintos
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao carregar filtros: {ex.Message}");
            }
        }

            /// <summary>Endpoint da API para esta operação.</summary>
    /// <summary>Endpoint da API para esta operação.</summary>
[HttpPost]
        public async Task<IActionResult> PostVeiculo([FromBody] Veiculo veiculo)
        {
            try 
            {
                // Regra de Negócio: Todo veículo novo entra como Disponível
                veiculo.Status = "Disponível";

                _context.VEICULO.Add(veiculo);
                await _context.SaveChangesAsync();
                return Ok(new { mensagem = "Veículo cadastrado com sucesso!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}