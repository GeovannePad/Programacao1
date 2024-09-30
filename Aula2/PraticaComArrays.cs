using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Aula2
{
    public class PraticaComArrays
    {
        public void Exercicio1()
        {
            int[] arrayIntLinear = new int[100000];

            // Popular as posições com seu valor de índice linearmente
            for (int i = 0; i < 100000; i++)
            {
                arrayIntLinear[i] = i;
            }

            int[,] matriz = new int[100, 100];
            var rand = new Random();

            for (int i = 0; i < 100; i++)
            {
                for (int k = 0; k < 100; k++)
                {
                    matriz[i, k] = rand.Next(1, 300000);
                }
            }

            int countMatch = 0;
            for (int i = 0; i < matriz.GetUpperBound(0); i++)
            {
                for (int k = 0; k  < matriz.GetUpperBound(1); k ++)
                {
                    var valorMatriz = (int)matriz.GetValue(i, k);

                    for (int x = 0; x < arrayIntLinear.Length; x++)
                    {
                        if (arrayIntLinear[x] == valorMatriz)
                        {
                            countMatch++;
                        }
                    }
                }
            }

            Console.WriteLine($"Houveram {countMatch} entre os valores randômicos da matriz e do array linear!");
        }

        public void Exercicio2()
        {
            Console.WriteLine("*******************************************************");

            // Declarando arrays de base para o exercício
            char[] alfabetoArray = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] arrayCharAleatorio = new char[100];
            char[] arrayFinal = new char[100];
            Random rand = new Random();

            // Populando o array aleatório
            for (int i = 0; i < 100; i++)
            {
                arrayCharAleatorio[i] = alfabetoArray[rand.Next(0, alfabetoArray.Length)];
            }

            int posInicial = 0;
            int posFinal = 0;

            Console.WriteLine("Informa um valor inicial para recortar do array original:");
            posInicial = int.Parse(Console.ReadLine());
            Console.WriteLine("Informa um valor final (maior que valor anterior) para recortar do array original:");
            posFinal = int.Parse(Console.ReadLine());

            for (int i = posInicial; i < posFinal; i++)
            {
                arrayFinal[i] = arrayCharAleatorio[i];
            }

            var stringFinal = new string(arrayFinal);

            Console.WriteLine("A string final ficou assim:");
            Console.WriteLine(stringFinal);
        }
    }
}
