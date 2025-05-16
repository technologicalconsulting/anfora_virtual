using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS;
using Microsoft.EntityFrameworkCore;
using sav_api.Features.RecoverPassword.Interfaces;
using sav_api.Models;
using System.Security.Cryptography;

namespace sav_api.Features.RecoverPassword.Services
{

    public class RequestResetService : IRequestResetService
    {
        private readonly AppDbContext _context;
        private readonly IResendEmailService _emailservice;

        public RequestResetService(AppDbContext context, IResendEmailService emailService)
        {
            _context = context;
            _emailservice = emailService;
        }

        public async Task RequestResetAsync(string email)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(e => e.Email == email);

            if (user == null)
            {
                throw new Exception("El correo no está registrado");
            }

            if (user.Estado == false)
            {
                throw new Exception("Solo usuarios activos pueden iniciar el proceso de recuperación de contraseña");
            }

            var newcode = new CodigoVerificacion
            {
                UsuarioId = user.Id,
                Codigo = GenerateCode(),
                FechaCreacion = DateTime.Now,
                FechaExpiracion = DateTime.Now.AddMinutes(10),
                Usado = false
            };

            await _context.CodigoVerificacions.AddAsync(newcode);
            await _context.SaveChangesAsync();


            //var subject = "Código de Verificación";
            //var body = $"Tu código de verificación es: <strong>{newcode.Codigo}</strong><br>  " +
            //    "<span style=\"color: red;\"> Este código expira en 10 minutos. </span>";

            //await _emailservice.SendEmailAsync(user.Email, subject, body);

            var auditoria = new Auditorium
            {
                UsuarioId = user.Id,
                Accion = "Solicitud de codigo",
                TablaAfectada = "CodigoVerificacion",
                RegistroId = user.Id,
                FechaOperacion = DateTime.Now,
                Descripcion = $"Se envió un código de verificación al correo {user.Email} para recuperación de contraseña."
            };

            await _context.Auditoria.AddAsync(auditoria);
            await _context.SaveChangesAsync();

        }
        public string GenerateCode()
        {
            var bytes = RandomNumberGenerator.GetBytes(4);
            var number = BitConverter.ToUInt32(bytes, 0) % 900000 + 100000;
            return number.ToString();
        }
    }
}