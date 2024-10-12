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
    public class ReviewMapp : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(x => x.ReviewId);

            builder.Property(x => x.NomeRevisor)
                   .HasMaxLength(100);

            builder.Property(x => x.QtdEstrelas);

            builder.Property(x => x.Comentario)
                   .HasMaxLength(300);

            builder.HasOne(x => x.Livro)
                   .WithMany(x => x.Reviews)
                   .HasForeignKey(x => x.LivroId);
        }
    }
}
