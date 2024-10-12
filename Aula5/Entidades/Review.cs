using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Aula5.Entidades
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string NomeRevisor { get; set; }
        public int QtdEstrelas { get; set; }
        public string Comentario { get; set; }
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
    }
}
