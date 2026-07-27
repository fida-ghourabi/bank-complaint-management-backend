using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankComplaintManagement.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {


        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }



        public DbSet<User> Users { get; set; }


        public DbSet<Client> Clients { get; set; }


        public DbSet<Agent> Agents { get; set; }


        public DbSet<Admin> Admins { get; set; }



        public DbSet<Complaint> Complaints { get; set; }



        public DbSet<Message> Messages { get; set; }



        public DbSet<Attachment> Attachments { get; set; }



        public DbSet<Notification> Notifications { get; set; }



        public DbSet<BankAccount> BankAccounts { get; set; }



        public DbSet<BankCard> BankCards { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }


        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                  typeof(ApplicationDbContext).Assembly);
        }
    }

         

    
}
