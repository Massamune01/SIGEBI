using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SIGEBI.Application.Dtos.Configuration.CredencialesDtos
{
    public record CredencialesCreateDto
    {
        [Key]
        public int ClienteId { get; set; }

        [StringLength(80, MinimumLength = 3, ErrorMessage = "Debe de tener mas de 3 caracteres y un maximo de 80")]
        public string? Usuario { get; set; } = string.Empty;

        [PasswordPropertyText]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Debe de tener mas de 3 caracteres")]
        public string? PasswordHash { get; set; } = string.Empty;
    }
}
