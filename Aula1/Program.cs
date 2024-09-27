using Aula1;
using System;

namespace ConsoleApp.Aula1
{
    class Program
    {
        char c1 = 'c';

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World - Aula Prática 1!");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("Tipos primitivos");

            int int1 = 10;
            Console.WriteLine($"{int1}");

            bool bool1 = true;
            bool bool2 = false;

            long long1 = 341241241245362457;
            decimal decimal1 = 123.234M;
            float float1 = 356.167f;

            Console.WriteLine("-----------------------------");
            Console.WriteLine("Tipos reference types");

            object obj1 = new object();
            var obj2 = new object();

            var obj3 = obj2;

            Console.WriteLine("Comparando objs");
            Console.WriteLine($"{obj1.GetType().Name} | {obj2.GetType().Name} | {obj3.GetType().Name}");
            Console.WriteLine($"{obj2 == obj3}");

            Console.WriteLine("-----------------------------");
            Console.WriteLine("Strings");

            var s1 = new string('a', 5);
            string s2 = new string(new char[5] { 'a', 'a', 'a', 'a', 'a' });
            Console.WriteLine($"{s1 == s2}");

            string s3 = string.Concat((new char[5] { 'a', 'a', 'a', 'a', 'a' }).AsEnumerable());

            var s4 = $"{s3} string 3";
            Console.WriteLine($"{s4}");

            Console.WriteLine("-----------------------------");
            Console.WriteLine("Classes");

            ClasseCompareString compare1 = new ClasseCompareString()
            {
                testeComparacao = "123"
            };
            var compare2 = new ClasseCompareString()
            {
                testeComparacao = "123"
            };

            Console.WriteLine($"Comparando com == e com equals");
            Console.WriteLine($"{compare1 == compare2}");
            Console.WriteLine($"{compare1.testeComparacao == compare2.testeComparacao}");
            Console.WriteLine($"{compare1.testeComparacao.Equals(compare2.testeComparacao)}");

            ClasseComplexa classeComplexa = new ClasseComplexa();
            classeComplexa.PropInterface = "123";

            IImprimirValores interfaceX = classeComplexa;

            Console.WriteLine($"{interfaceX.PropInterface}");

            // Divisor

            IDivisor divisorCalculadora = new DivisorCalc();

            Console.WriteLine("Digite 'S' para SAIR a qualquer momento");
            string valorLido = string.Empty;
            int a;
            int b;
            string valorSaida = "S";
            do
            {
                // Valor A
                Console.WriteLine("Digite o valor A:");

                valorLido = Console.ReadLine();
                if (valorSaida.Equals(valorLido))
                {
                    break;
                }

                try
                {
                    a = int.Parse(valorLido);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Erro ao converter o valor lido para int. Valor: {valorLido}");
                    continue;
                }

                // Valor B
                Console.WriteLine("Digite o valor B:");

                valorLido = Console.ReadLine();
                if (valorSaida.Equals(valorLido))
                {
                    break;
                }

                try
                {
                    b = int.Parse(valorLido);
                }
                catch(Exception)
                {
                    Console.WriteLine($"Erro ao converter o valor lido para int. Valor: {valorLido}");
                    continue;
                }

                // Resultado
                try
                {
                    var resultado = divisorCalculadora.Dividir(a, b);
                    Console.WriteLine("Resultado:");
                    Console.WriteLine(resultado);
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("Divisão por zero detectada. Reiniciando processo");
                }
            }
            while (valorSaida.Equals(valorLido) == false);

            Console.WriteLine(divisorCalculadora.Dividir(100, 10));
        }
    }
}