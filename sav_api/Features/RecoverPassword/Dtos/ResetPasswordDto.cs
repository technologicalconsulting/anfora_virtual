using System.ComponentModel.DataAnnotations;

namespace sav_api.DTOs
{
    public class ResetPasswordDto
    {
        public string Code { get; set; } = string.Empty;


        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        public string NewPassword { get; set; } = string.Empty;

    }
}
