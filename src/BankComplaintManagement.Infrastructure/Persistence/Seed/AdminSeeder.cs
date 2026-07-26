using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Domain.Enums;
using BankComplaintManagement.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Infrastructure.Persistence.Seed
{
    public class AdminSeeder
    {

        private readonly ApplicationDbContext _context;


        private readonly IPasswordService _passwordService;


        private readonly AdminSettings _settings;



        public AdminSeeder(
            ApplicationDbContext context,
            IPasswordService passwordService,
            IOptions<AdminSettings> settings)
        {

            _context = context;

            _passwordService = passwordService;

            _settings = settings.Value;

        }





        public async Task SeedAsync()
        {


            bool exists =
                await _context.Users
                .AnyAsync(u =>
                    u.Role == UserRole.Admin);



            if (exists)
            {
                return;
            }





            var admin =
                new Admin(
                    _settings.FirstName,
                    _settings.LastName,
                    _settings.Email,
                    _passwordService.HashPassword(
                        _settings.Password)
                );





            await _context.Users
                .AddAsync(admin);



            await _context.SaveChangesAsync();





            Console.WriteLine(
                "Admin account created");

        }

    }
}