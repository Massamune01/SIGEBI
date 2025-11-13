namespace SIGEBI.Persistence.Models.Configuration.Credenciales
{
    public record CredencialesGetModel
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
