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
    public class AlunoCursoMap : IEntityTypeConfiguration<CursoAluno>
    {
        public void Configure(EntityTypeBuilder<CursoAluno> builder)
        {
            builder.HasKey(x => new { x.AlunoId, x.CursoId });

            builder.HasOne(x => x.Aluno)
                   .WithMany(x => x.Cursos)
                   .HasForeignKey(x => x.AlunoId);

            builder.Property(x => x.DataInscricao);
            builder.Property(x => x.PeriodoAtual);
        }
    }
}
