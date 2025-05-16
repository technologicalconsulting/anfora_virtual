using System.ComponentModel.DataAnnotations;

namespace sav_api.Features.RecoverPassword.Dtos
{
    public class RequestResetDto
    {
        [Required(ErrorMessage = "El campo no puede estar vacío")]
        [EmailAddress(ErrorMessage = "Debe ser un correo válido.")]
        public string Email { get; set; } = string.Empty;
    }
}
