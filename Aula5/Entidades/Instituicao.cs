using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Aula5.Entidades
{
    public class Instituicao
    {
        public enum TipoInstituicao
        {
            EAD,
            EAD_Presencial,
            Presencial
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public TipoInstituicao Tipo { get; set; }

        public List<Curso> Cursos { get; set; }
    }
}
