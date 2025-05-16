using Microsoft.EntityFrameworkCore;
using sav_api.Features.RecoverPassword.Interfaces;
using sav_api.Features.RecoverPassword.Services.Models;
using sav_api.Models;

namespace sav_api.Features.RecoverPassword.Services
{
    public class ResetPasswordService: IResetPasswordService
    {
        private readonly AppDbContext _context;


        public  ResetPasswordService(AppDbContext context)
        {
            _context = context;
        }

        public async Task ResetPasswordAsync(string code, string newPassword)
        {
            var codeValid = await _context.CodigoVerificacions.FirstOrDefaultAsync(e =>
                e.Codigo == code
            );

            if (codeValid == null)
            {
                throw new BadHttpRequestException("Código no válido");
            }
            if (codeValid.FechaExpiracion <= DateTime.Now)
            {
                throw new BadHttpRequestException("Codigo expirado ");
            }
            if (codeValid.Usado == true)
            {
                throw new BadHttpRequestException("Codigo ya utilizado ");
            }

            var user = await _context.Usuarios.FirstOrDefaultAsync(e => 
                e.Id == codeValid.UsuarioId
            );

            if (user == null)
            {
                throw new BadHttpRequestException("Usuario no encontrado.");
            }

            user.ClaveHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            codeValid.Usado = true;

            await _context.SaveChangesAsync();

            var auditoria = new Auditorium
            {
                UsuarioId = user.Id,
                Accion = "Cambio de contraseña",
                TablaAfectada = "Usuario",
                RegistroId = user.Id,
                FechaOperacion = DateTime.Now,
                Descripcion = "Cambio de contraseña exitoso"
            };

            await _context.Auditoria.AddAsync(auditoria);
            await _context.SaveChangesAsync();
        }
    }
}