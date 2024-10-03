using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp.Aula3
{
    public class PraticaComLinq
    {
        public void Exercicio1()
        {
            // Preparando cenário
            string[] nomes = { "João", "Silva", "Paulo", "Antonio", "Maria", "Joana", "Matheus", "Alan", "Julian", "Tomé" };
            string[] materias = { "Programação I", "Algoritmos II", "Redes", "Cálculo II", "Sistemas Operacionais" };
            Random rand = new Random();
            List<Avaliacao> avaliacoes = new List<Avaliacao>();

            foreach (var nomeEstudante in nomes)
            {
                foreach (var materia in materias)
                {
                    var avaliacao = new Avaliacao()
                    {
                        Nome = materia,
                        NomeAluno = nomeEstudante,
                        Nota = decimal.Parse($"{rand.Next(0, 10)},{rand.Next(0, 10)}")
                    };

                    avaliacoes.Add(avaliacao);
                }
            }

            // Query 1
            var queryEstudantes = from n in nomes
                                  let avaliacoesEstudante = (from a in avaliacoes
                                                             where a.Nome == n
                                                             select a).ToList()
                                  select new Estudante()
                                  {
                                      Avaliacoes = avaliacoesEstudante,
                                      Nome = n,
                                      Media = avaliacoesEstudante.Average(x => x.Nota)
                                  };

            var estudantesList = queryEstudantes.ToList();

            // Query 2
            var estudanteMaiorNota = estudantesList.SelectMany(e => e.Avaliacoes)
                                                    .Where(a => a.Nome == "Programação I")
                                                    .OrderByDescending(x => x.Nota)
                                                    .Select(a => a.NomeAluno)
                                                    .FirstOrDefault();

            // Query 3
            var queryMediaNotas = estudantesList.SelectMany(e => e.Avaliacoes)
                                                .GroupBy(e => e.Nome)
                                                .Select(x => new
                                                {
                                                    Materia = x.Key,
                                                    MediaNota = x.Average(m => m.Nota)
                                                });

            // Query 4
            var queryNotasCalc = estudantesList.SelectMany(e => e.Avaliacoes)
                                                .Where(a => a.Nome == "Cálculo II" && a.NomeAluno.ToLower().Contains("a"))
                                                .OrderByDescending(x => x.Nota);
        }

        public class Estudante
        {
            public string Nome { get; set; }
            public decimal Media { get; set; }
            public List<Avaliacao> Avaliacoes { get; set; }
        }

        public class Avaliacao
        {
            public string Nome { get; set; }
            public string NomeAluno { get; set; }
            public decimal Nota { get; set; }
        }
    }
}
