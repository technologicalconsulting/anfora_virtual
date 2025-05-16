using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sav_api.DTOs;
using sav_api.Features.RecoverPassword.Dtos;
using sav_api.Features.RecoverPassword.Interfaces;
using sav_api.Models;



namespace sav_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRequestResetService _requestReset;
        private readonly IVerifyCodeService _verifyCode;
        private readonly IResetPasswordService _resetPassword;

        public AuthController(
            IRequestResetService requestReset, 
            IVerifyCodeService verifyCode,
            IResetPasswordService resetPassword
            )
        {
            _requestReset = requestReset;
            _verifyCode = verifyCode;
            _resetPassword = resetPassword;

        }

        //POST /api/auth/request-reset
        [HttpPost("request-reset")]
        public async Task<IActionResult> SendResetPasswordCode([FromBody] RequestResetDto resetdto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _requestReset.RequestResetAsync(resetdto.Email);
            return Ok("El correo se envió correctamente");
        }

        //POST /api/auth/verify-code
        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyResetPasswordCode([FromBody] VerifyCodeDto verifydto)
        {
            await _verifyCode.VerifyCodeAsync(verifydto.Code);

            return Ok(new ResponseVerifyCodeDto
            {
                Code = verifydto.Code,
                Message = "El codigo se verificó correctamente",
            });


        }

        //POST /api/auth/reset-password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ConfirmPasswordReset([FromBody] ResetPasswordDto passwordDto)
        {
            await _resetPassword.ResetPasswordAsync(passwordDto.Code, passwordDto.NewPassword);

            return Ok("La contraseña ha sido cambiada correctamente");
        }



    }
}
