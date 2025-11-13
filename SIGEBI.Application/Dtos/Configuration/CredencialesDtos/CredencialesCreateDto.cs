using System.ComponentModel.DataAnnotations;

namespace SIGEBI.Application.Dtos.Configuration.CredencialesDtos
{
    public record CredencialesCreateDto
    {
        [Key]
        public int ClienteId { get; set; }
        public string Usuario { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; 
    }
}
