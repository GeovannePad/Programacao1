using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebApplicationSoap
{
    /// <summary>
    /// Descrição resumida de WebServiceSoap
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceSoap : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Olá, Mundo";
        }

        [WebMethod]
        public List<int> CalculaFibonnaci(int numero)
        {

            var sequencia = FibonacciSerie(numero);

            return sequencia.ToList();
        }

        [WebMethod]
        public Autor[] GetRetornoComplexo(string editoras)
        {
            var autores = GeraAutores(6, editoras.Split(','));

            return autores.ToArray();
        }

        public IEnumerable<Autor> GeraAutores(int quantidade, string[] editoras)
        {
            List<Autor> autores = new List<Autor>();
            var nomes = new string[] { "ALAN", "JOSE", "MARIA", "LUCAS", "JOÃO", "MARCELO", "SIMONE", "JOSIANE", "PATRICK", "LAUREN", "GABRIELA", "NICOLE", "ISABELA", "MATHEUS", "THIAGO", "GABRIEL", "PAULO", "EDUARDO" };
            Random randomizer = new Random();

            for (int i = 0; i < quantidade; i++)
            {
                autores.Add(new Autor()
                {
                    Codigo = i,
                    Editora = editoras[randomizer.Next(0, editoras.Length)],
                    Nome = nomes[randomizer.Next(0, nomes.Length)],
                    DataNascimento = new DateTime(randomizer.Next(1960, 2002), randomizer.Next(1, 13), randomizer.Next(1, 30))
                });
            }

            return autores;
        }

        public class Autor
        {
            public int Codigo { get; set; }
            public string Nome { get; set; }
            public string Editora { get; set; }
            public DateTime DataNascimento { get; set; }
        }


        public IEnumerable<int> FibonacciSerie(int n)
        {
            List<int> series = new List<int>();
            int a = 0, b = 1, resultado = 0;

            if (n == 0)
            {
                series.Add(a);
                return series;
            }
            if (n == 1)
            {
                series.Add(b);
                return series;
            }
            for (int i = 2; i <= n; i++)
            {
                resultado = a + b;
                a = b;
                b = resultado;

                series.Add(resultado);
            }

            return series;
        }
    }
}
