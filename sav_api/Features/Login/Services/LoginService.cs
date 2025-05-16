using BCrypt.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using sav_api.Features.Login.Dtos;
using sav_api.Features.Login.Interfaces;
using sav_api.Models;
using System.Security.Cryptography.X509Certificates;

namespace sav_api.Features.Login.Services
{
    public class LoginService: ILoginService
    {
        private readonly AppDbContext _context;

        public LoginService(AppDbContext context)
        {
            _context = context;
        }
        public async Task LoginAsync(LoginDto loginDto)
        {
            var uservalid = await _context.Usuarios.FirstOrDefaultAsync(e => e.Email == loginDto.Email);

            if (uservalid == null)
            {
                throw new UnauthorizedAccessException("Correo o contraseña incorrectos.");
            }

            bool passwordMatch = BCrypt.Net.BCrypt.Verify(loginDto.Password, uservalid.ClaveHash);

            if (!passwordMatch)
            {
                throw new UnauthorizedAccessException("Contraseña incorrecta");
            }


        }

        
        
    }
}
