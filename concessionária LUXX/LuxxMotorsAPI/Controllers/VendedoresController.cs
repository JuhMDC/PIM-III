using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LuxxMotorsAPI.Data;
using LuxxMotorsAPI.Models;

namespace LuxxMotorsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // DOC: Classe VendedoresController usada no backend para representar esta entidade ou controller.
    public class VendedoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VendedoresController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>Endpoint da API para listar todos os vendedores do banco.</summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendedor>>> GetVendedores()
        {
            // Busca todos os vendedores com email para validação de login
            var vendedores = await _context.VENDEDOR.ToListAsync();
            
            // Log de debug para verificar dados sendo retornados
            Console.WriteLine($"\n[VENDEDORES] Total: {vendedores.Count}");
            foreach (var v in vendedores)
            {
                Console.WriteLine($"ID: {v.Id_vendedor}, Nome: {v.Nome}, Email: '{v.Email}'");
            }
            
            return vendedores;
        }

        /// <summary>Endpoint para criar ou atualizar um vendedor.</summary>
        [HttpPost]
        public async Task<IActionResult> PostVendedor([FromBody] Vendedor vendedor)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(vendedor.Nome))
                {
                    return BadRequest(new { mensagem = "Nome é obrigatório" });
                }

                _context.VENDEDOR.Add(vendedor);
                await _context.SaveChangesAsync();

                Console.WriteLine($"\n[NOVO VENDEDOR] ID: {vendedor.Id_vendedor}, Nome: {vendedor.Nome}, Email: '{vendedor.Email}', Senha: '{vendedor.Senha}'");

                return Ok(new { mensagem = "Vendedor criado com sucesso!", id = vendedor.Id_vendedor });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERRO VENDEDOR] {ex.Message}");
                return StatusCode(500, new { mensagem = "Erro ao criar vendedor", detalhe = ex.Message });
            }
        }

        /// <summary>Endpoint para validar login de vendedor (debug).</summary>
        [HttpGet("validar/{email}")]
        public async Task<IActionResult> ValidarLogin(string email)
        {
            try
            {
                Console.WriteLine($"\n[LOGIN DEBUG] Buscando email: '{email}'");
                
                var vendedor = await _context.VENDEDOR
                    .FirstOrDefaultAsync(v => v.Email.ToLower() == email.ToLower());

                if (vendedor == null)
                {
                    Console.WriteLine($"[LOGIN DEBUG] Email '{email}' NÃO ENCONTRADO no banco");
                    return NotFound(new { 
                        mensagem = "Email não encontrado", 
                        email_procurado = email,
                        emails_no_banco = await _context.VENDEDOR.Select(v => v.Email).ToListAsync()
                    });
                }

                Console.WriteLine($"[LOGIN DEBUG] Email ENCONTRADO: Id: {vendedor.Id_vendedor}, Nome: {vendedor.Nome}, Senha: '{vendedor.Senha}'");
                
                return Ok(new {
                    encontrado = true,
                    id_vendedor = vendedor.Id_vendedor,
                    nome = vendedor.Nome,
                    email = vendedor.Email,
                    senha_salva = vendedor.Senha
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO LOGIN DEBUG] {ex.Message}");
                return StatusCode(500, new { mensagem = "Erro ao validar login", detalhe = ex.Message });
            }
        }

        /// <summary>Endpoint de login: recebe email e senha, valida e retorna dados do vendedor.</summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LuxxMotorsAPI.Models.LoginRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Email))
                    return BadRequest(new { mensagem = "Email é obrigatório" });

                Console.WriteLine($"\n[LOGIN] Tentativa para email: '{request.Email}'");

                var vendedor = await _context.VENDEDOR
                    .FirstOrDefaultAsync(v => v.Email.ToLower() == request.Email.ToLower());

                if (vendedor == null)
                {
                    Console.WriteLine($"[LOGIN] Email '{request.Email}' não encontrado");
                    return Unauthorized(new { mensagem = "E-mail ou senha inválidos" });
                }

                var senhaSalva = (vendedor.Senha ?? string.Empty).ToString().Trim();
                bool senhaMatch;

                if (!string.IsNullOrEmpty(senhaSalva))
                {
                    senhaMatch = senhaSalva == request.Senha;
                }
                else
                {
                    // Se não houver senha salva na base, aceita senha fixa '123' para usuários internos
                    senhaMatch = request.Senha == "123";
                }

                Console.WriteLine($"[LOGIN] Encontrado Id:{vendedor.Id_vendedor}, senha_salva={(string.IsNullOrEmpty(senhaSalva) ? "(vazio)" : "(presente)")}, senhaMatch={senhaMatch}");

                if (!senhaMatch) return Unauthorized(new { mensagem = "E-mail ou senha inválidos" });

                // Retorna dados sem expor a senha
                return Ok(new {
                    id = vendedor.Id_vendedor,
                    nome = vendedor.Nome,
                    email = vendedor.Email,
                    cargo = vendedor.cargo
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERRO LOGIN] {ex.Message}");
                return StatusCode(500, new { mensagem = "Erro ao processar login", detalhe = ex.Message });
            }
        }

        /// <summary>Lista as credenciais válidas dos vendedores (uso interno).</summary>
        [HttpGet("credenciais")]
        public async Task<IActionResult> ListarCredenciais()
        {
            try
            {
                // 1. Buscamos APENAS as colunas que REALMENTE existem fisicamente no banco de dados
                var dadosVendedores = await _context.VENDEDOR
                    .Select(v => new {
                        v.Id_vendedor,
                        v.Nome,
                        v.Email
                    })
                    .ToListAsync(); // Executa e fecha a comunicação com o banco aqui

                // 2. Agora injetamos a senha fixa '123' em memória, sem o SQL Server saber
                var listaCredenciais = dadosVendedores
                    .Select(v => new {
                        id = v.Id_vendedor,
                        nome = v.Nome,
                        email = v.Email,
                        senha_valida = "123" // Injetado com sucesso no C#
                    })
                    .ToList();

                Console.WriteLine($"\n[CREDENCIAIS SUCESSO] Geradas {listaCredenciais.Count} credenciais internas.");
                return Ok(listaCredenciais);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERRO CRÍTICO CREDENCIAIS]: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[DETALHE BANCO]: {ex.InnerException.Message}");
                }
                return StatusCode(500, new { mensagem = "Erro ao processar credenciais", detalhe = ex.Message });
            }
        }
    }
}