using ConsoleApp.Aula5.Entidades;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Aula5.Mapping
{
    public class LivroAutorMapp : IEntityTypeConfiguration<LivroAutor>
    {
        public void Configure(EntityTypeBuilder<LivroAutor> builder)
        {
            builder.HasKey(x => new { x.LivroId, x.AutorId });

            builder.HasOne(x => x.Livro)
                   .WithMany(x => x.Autores)
                   .HasForeignKey(x => x.LivroId);

            builder.HasOne(x => x.Autor)
                   .WithMany(x => x.Livros)
                   .HasForeignKey(x => x.AutorId);
        }
    }
}
