using Azure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Database
{
    public class SQLSContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<EntryGroup> EntryGroups { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=PasswordDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var General = new EntryGroup {Id = 1, Name = "General" };
            var Internet = new EntryGroup { Id= 2,Name = "Internet" };
            var Banking = new EntryGroup {Id = 3,Name = "Banking" };
            
            modelBuilder.Entity<EntryGroup>().HasData(
                General,
                Internet,
                Banking) ;
            modelBuilder.Entity<Entry>().HasData(
                new Entry { Id = 1, Username = "UserG", Password = "Password", },
                new Entry { Id = 2, Username = "UserI", Password = "Password", },
                new Entry { Id = 3, Username = "UserB", Password = "Password", }
                );
        }
        
    }
}
    