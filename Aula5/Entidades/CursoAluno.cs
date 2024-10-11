using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Aula5.Entidades
{
    public class CursoAluno
    {
        public Curso Curso { get; set; }
        public int CursoId { get; set; }
        public Aluno Aluno { get; set; }
        public Guid AlunoId { get; set; }

        public DateTime DataInscricao { get; set; }
        public int PeriodoAtual { get; set; }
    }
}
