using System;

namespace ConsoleApp.Aula3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Aula prática de C# 3");
            Console.WriteLine("--------------------");
            Console.WriteLine("Lambda Functions");

            PraticaComLambdas praticaComLambdas = new PraticaComLambdas();
            praticaComLambdas.Exercicio1();
        }
    }
}