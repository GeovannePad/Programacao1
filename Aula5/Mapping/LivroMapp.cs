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
    public class LivroMapp : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(x => x.LivroId);

            builder.Property(x => x.Titulo)
                   .HasMaxLength(100);

            builder.Property(x => x.Descricao)
                   .HasMaxLength(300);

            builder.Property(x => x.PublicadoEm);
        }
    }
}
