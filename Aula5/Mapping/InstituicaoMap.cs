using ConsoleApp.Aula5.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Aula5.Mapping
{
    public class InstituicaoMap : IEntityTypeConfiguration<Instituicao>
    {
        public void Configure(EntityTypeBuilder<Instituicao> builder)
        {
            builder.ToTable("Instituicao");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedNever();

            builder.Property(x => x.Nome)
                   .HasMaxLength(100);

            builder.Property(x => x.Tipo)
                   .HasConversion(toDb => toDb.GetHashCode(),
                                  fromDb => (Instituicao.TipoInstituicao)fromDb);

            builder.HasMany(x => x.Cursos)
                   .WithOne(x => x.Instituicao);
        }
    }
}
