using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Aula5.DTO
{
    public class LivroBaixaPontuacao
    {
        public int LivroId { get; set; }
        public string Titulo { get; set; }
        public int QtdPontuacao { get; set; }
        public int ReviewId { get; set; }
        public string NomeRevisor { get; set; }
        public int QtdEstrelas { get; set; }
        public string Comentario { get; set; }
    }
}
