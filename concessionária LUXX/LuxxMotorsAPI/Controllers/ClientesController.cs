using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuxxMotorsAPI.Data;
using LuxxMotorsAPI.Models;

namespace LuxxMotorsAPI.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    // DOC: Classe ClientesController usada no backend para representar esta entidade ou controller.
public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context) 
        { 
            _context = context; 
        }

        

            /// <summary>Endpoint da API para esta operação.</summary>
    /// <summary>Endpoint da API para esta operação.</summary>
[HttpPost]
        public async Task<IActionResult> PostCliente([FromBody] Cliente cliente)
        {
            try 
            {
                // 1. Log de entrada: Verifica se o JSON chegou preenchido
                Console.WriteLine("\n--- INICIANDO CADASTRO ---");
                Console.WriteLine($"Recebido: Nome={cliente.Nome_completo}, CPF={cliente.Cpf}");

                // 2. Adiciona ao contexto
                _context.CLIENTE.Add(cliente);
                Console.WriteLine("Aguardando persistência no Banco de Dados...");

                // 3. O comando que realmente fala com o SQL Server
                await _context.SaveChangesAsync();

                // 4. Log de sucesso: Só aparece se o banco aceitar o INSERT
                Console.WriteLine("✅ SUCESSO: Cliente gravado com ID: " + cliente.Id_cliente);
                Console.WriteLine("---------------------------\n");

                return Ok(new { mensagem = "Cadastro da Luxx Motors concluído!", id = cliente.Id_cliente });
            }
            catch (Exception ex)
            {
                // 5. Log de erro: Se o banco travar, ele vai dizer o porquê aqui
                Console.WriteLine("\n❌ ERRO AO GRAVAR NO BANCO:");
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null) Console.WriteLine("Detalhe: " + ex.InnerException.Message);
                Console.WriteLine("---------------------------\n");

                return StatusCode(500, new { mensagem = "Erro interno ao salvar no banco", detalhe = ex.Message });
            }
        }

            /// <summary>Endpoint da API para esta operação.</summary>
    /// <summary>Endpoint da API para esta operação.</summary>
[HttpGet("")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            try
            {
                return await _context.CLIENTE.ToListAsync(); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao listar clientes: {ex.Message}");
            }
        }

        [HttpGet("verificar/{cpf}")]
        public async Task<IActionResult> VerificarCpf(string cpf)
        {
            // 1. Limpa o CPF que vem do site
            var cpfEntradaLimpo = cpf.Replace(".", "").Replace("-", "").Trim();

            // 2. Busca o cliente usando uma lógica que ignora espaços e formatação
            // Usamos o .Contains para garantir que ele ache o padrão numérico
            var cliente = await _context.CLIENTE
                .FirstOrDefaultAsync(c => 
                    c.Cpf.Replace(".", "").Replace("-", "").Trim().Contains(cpfEntradaLimpo));

            if (cliente == null)
            {
                return NotFound(new { mensagem = "CPF não localizado mesmo após higienização." });
            }

            // 3. Retorna o objeto Lucas completo para o JavaScript
            return Ok(cliente);
        }

        
    }
}   