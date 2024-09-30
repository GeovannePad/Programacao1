using System;

namespace ConsoleApp.Aula2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Aula Prática de C# 2!");
            Console.WriteLine("---------------------");
            Console.WriteLine("Arrays");

            PraticaComArrays praticaComArrays = new PraticaComArrays();
            praticaComArrays.Exercicio1();
            praticaComArrays.Exercicio2();

            Console.WriteLine("---------------------");
            Console.WriteLine("Generics");

            PraticaComFilasBoxing praticaComFilasBoxing = new PraticaComFilasBoxing();
            praticaComFilasBoxing.ExercicioBoxingFilas();

            Console.WriteLine("---------------------");
            Console.WriteLine("Listas, IEnumerables e Dicionários");

            PraticaManipulandoCollections praticaManipulandoCollections = new PraticaManipulandoCollections();
            praticaManipulandoCollections.Exercicio1();

            Console.WriteLine("---------------------");
            Console.WriteLine("Métodos de extensão");

            PraticaExtensionMethods praticaExtensionMethods = new PraticaExtensionMethods();
            praticaExtensionMethods.Exercicio1();
        }
    }
}