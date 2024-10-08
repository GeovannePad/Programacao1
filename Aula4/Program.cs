using System;

namespace ConsoleApp.Aula4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Aula prática de C# 4");
            Console.WriteLine("--------------------");
            Console.WriteLine("Threads");

            //ThreadPing exercicio1 = new ThreadPing();
            //exercicio1.StartPing();

            Console.WriteLine("--------------------");
            Console.WriteLine("Tasks e Async/Await");

            //PessoaFinder pessoaFinder = new PessoaFinder();
            //pessoaFinder.ExercicioPessoaAsync();

            PraticaIOAsync praticaIOAsync = new PraticaIOAsync();
            _ = praticaIOAsync.Exercicio2Async();
        }
    }
}