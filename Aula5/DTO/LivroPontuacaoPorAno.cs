using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Aula5.DTO
{
    public class LivroPontuacaoPorAno
    {
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int AnoPublicacao { get; set; }
        public int Pontuacao { get; set; }
        public decimal Media { get; set; }
    }
}
