using SoapWebService;
using System;

namespace ConsoleApp.Aula6
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Aula 6! Consumindo Web Services e APIs");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Consumindo via SOAP");
            Console.WriteLine("--------------------------------------------------------");

            //Inicializa o client
            var soapClient = new WebServiceSoapSoapClient(WebServiceSoapSoapClient.EndpointConfiguration.WebServiceSoapSoap);

            var retornoFibonnaci = await soapClient.CalculaFibonnaciAsync(10);

            Console.WriteLine("Resultado Fibonnaci:");
            foreach (int numero in retornoFibonnaci.Body.CalculaFibonnaciResult)
            {
                Console.Write($"{numero},");
            }

            var retornoAutores = await soapClient.GetRetornoComplexoAsync("Intersaberes, O'Relly, Meaning, B2you");

            Console.WriteLine("");
            Console.WriteLine("Resultado Autores:");
            foreach (var autor in retornoAutores.Body.GetRetornoComplexoResult)
            {
                Console.WriteLine($"{autor.Nome} - {autor.Editora} - {autor.DataNascimento}");
            }

            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Consumindo via SOAP");
            Console.WriteLine("--------------------------------------------------------");

            // Consumindo Web Api pelo Console
            var httpClient = new HttpClient();
            int numeroFibonacci = 10;
            HttpResponseMessage responseFibonnaci = await httpClient.GetAsync($"http://localhost:5270/fibonnaci?numero={numeroFibonacci}");

            if (responseFibonnaci.IsSuccessStatusCode)
            {
                string contentString = await responseFibonnaci.Content.ReadAsStringAsync();
                var sequenciaFibonnaci = System.Text.Json.JsonSerializer.Deserialize<int[]>(contentString);

                foreach (int numero in sequenciaFibonnaci)
                {
                    Console.Write($"{numero},");
                }
            }
        }
    }
}