using System.ComponentModel.DataAnnotations;

namespace sav_api.Features.Login.Dtos
{
    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public string Password { get; set; } = string.Empty;

    }
}
