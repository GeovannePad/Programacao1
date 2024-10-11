using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Aula5
{
    public class ApplicationContext : DbContext
    {
        private const string stringDeConexao = @"...";

        public DbSet<Aluno> Alunos { get; set; }
        public DtSet<Curso> Cursos { get; set; }
        public DbSet<Instituicao> instituicoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 25));

            optionsBuilder.UseMySql(stringDeConexao, serverVersion)
                          .EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
