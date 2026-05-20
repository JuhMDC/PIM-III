namespace LuxxMotorsAPI.Models
{
    // DOC: Modelo usado para receber requisições de login de vendedor
    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
}
