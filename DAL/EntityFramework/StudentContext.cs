using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.EntityFramework
{
    internal sealed class StudentContext : DbContext
    {
        internal DbSet<Student> Students { get; set; }
        
        internal StudentContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Подключение к удаленному серверу базы данных на Azure
            optionsBuilder.UseSqlServer(
                @"Server=tcp:suding-server.database.windows.net,1433;Initial Catalog=reposDB;Persist Security Info=False;User ID=vladimir;Password=793b3243!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}