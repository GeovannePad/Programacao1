using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Aula5.Entidades
{
    public class Curso
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public int Periodos { get; set; }
        public int TempoMesesPeriodo { get; set; }

        public List<CursoAluno> Alunos { get; set; }
        public Instituicao Instituicao { get; set; }
    }
}
