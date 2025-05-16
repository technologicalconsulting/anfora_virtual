using Microsoft.EntityFrameworkCore;
using sav_api.Features.RecoverPassword.Interfaces;
using sav_api.Features.RecoverPassword.Services.Models;
using sav_api.Models;

namespace sav_api.Features.RecoverPassword.Services
{
    public class VerifyCodeService : IVerifyCodeService
    {

        private readonly AppDbContext _context;

        public VerifyCodeService(AppDbContext context)
        {
            _context = context;
        }


        public async Task VerifyCodeAsync(string code)
        {

            var codeValid = await _context.CodigoVerificacions.FirstOrDefaultAsync(e =>
                e.Codigo == code
            );


            if (codeValid == null)
            {
                throw new BadHttpRequestException("Codigo incorrecto ");
            } 
            if (codeValid.FechaExpiracion <= DateTime.Now)
            {
                throw new BadHttpRequestException("Codigo expirado ");
            }
            if (codeValid.Usado == true )
            {
                throw new BadHttpRequestException("Codigo ya utilizado ");
            }


            var auditoria = new Auditorium
            {
                UsuarioId = codeValid.UsuarioId,
                Accion = "verificacion codigo",
                TablaAfectada = "CodigoVerificacion",
                RegistroId = codeValid.Id,
                FechaOperacion = DateTime.Now,
                Descripcion = "Se valido el código exitosamente para recuperación de contraseña."
            };

            await _context.Auditoria.AddAsync(auditoria);
            await _context.SaveChangesAsync();
        }
    }
}