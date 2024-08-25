using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinAPIMusicProject.Data;
using MinAPIMusicProject.DTOs;
using MinAPIMusicProject.Models;
using System.Security.Cryptography;
using System.Text;

namespace MinAPIMusicProject.Endpoints
{
    public static class UserEndpoints
    {
        public static void AddUserEndpoints(this IEndpointRouteBuilder app)
        {
            var endpoint = app.MapGroup("/api/user");

            endpoint.MapPost("/register", async (
                MusicContext context,
                [FromBody] RegisterUserDTO registerDTO) =>
            {
                if (await context.Users.AnyAsync(u => u.Login == registerDTO.Login))
                {
                    return Results.Conflict("User already exists");
                }

                var user = new User
                {
                    Name = registerDTO.Name,
                    Login = registerDTO.Login,
                    Password = HashPassword(registerDTO.Password),
                    Playlists = new List<Playlist>()
                };

                context.Users.Add(user);
                await context.SaveChangesAsync();

                return Results.Created($"/api/users/{user.Id}", user);
            })
            .WithName("Register User")
            .WithDescription("Register a new user");


            endpoint.MapPost("/login", async (
                MusicContext context,
                [FromBody] LoginUserDTO loginDTO) =>
            {
                var user = await context.Users
                    .FirstOrDefaultAsync(u => u.Login == loginDTO.Login);

                if (user == null || !VerifyPassword(loginDTO.Password, user.Password))
                {
                    return Results.Unauthorized();
                }

                return Results.Ok(user);
            })
            .WithName("Login User")
            .WithDescription("Authenticate a user");
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        } 

        private static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            var hashedInput = HashPassword(inputPassword);
            return hashedPassword == hashedInput;
        }
    }
}
