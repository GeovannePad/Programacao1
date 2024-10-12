using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Aula5.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp.Aula5.Mapping
{
    public class AutorMapp : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.HasKey(x => x.AutorId);

            builder.Property(x => x.Nome)
                   .HasMaxLength(100);

            builder.Property(x => x.WebUrl)
                   .HasMaxLength(500);
        }
    }
}
