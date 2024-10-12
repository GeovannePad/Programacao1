using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Aula5.Entidades
{
    public class Autor
    {
        public int AutorId { get; set; }
        public string Nome { get; set; }
        public string WebUrl { get; set; }
        public List<LivroAutor> Livros { get; set; }
    }
}
