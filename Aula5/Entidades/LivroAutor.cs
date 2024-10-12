using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Aula5.Entidades
{
    public class LivroAutor
    {
        public int AutorId { get; set; }
        public int LivroId { get; set; }
        public string Tipo { get; set; }
        public Autor Autor { get; set; }
        public Livro Livro { get; set; }
    }
}
