using Authentication.Domain.Entities;
using Authentication.Infrastrunture.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reservar.Common.Domain.Exceptions;

namespace Authentication.Infrastrunture.Inicialize
{
    public class Start
    {
        private readonly ILogger<Start> _logger;
        private AppDbContext _context { get; set; }
        public Start(DbContext context)
        {
            _context = (AppDbContext)context;
        }

        public async Task Seed()
        {
            try
            {
                //_logger.LogInformation("Start handling Seed.");
                if (!_context.Users.Any()) await SeedUsers();

            }
            catch (AppException ex)
            {
                //_logger.LogError(ex, $"StackTrace: {ex.StackTrace}");
                //_logger.LogError(ex, $"An unhandled exception has occurred: {ex.Message}");
                //throw ex;
            }
            finally
            {
                //_logger.LogInformation("Finished handling Seed.");
            }
        }

        private async Task SeedUsers()
        {
            var users = new List<User>
            {
                new User
                {
                    Email = "fabianramirez0072@gmail.com",
                    Name = "Fabian ",
                    LastName = "Ramirez",
                    Password = "Password"
                },
                new User
                {
                    Email = "ramirezedward535@gmail.com",
                    Name = "Fabian ",
                    LastName = "Ramirez",
                    Password = "Password"
                },
            };

            await _context.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }
    }
}
