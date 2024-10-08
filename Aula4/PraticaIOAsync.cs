using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Aula4
{
    public class PraticaIOAsync
    {
        public class Diretorio
        {
            public string Nome { get; set; }
            public decimal TamanhoEmMB { get; set; }

            public List<Arquivo> Arquivos { get; set; }
        }

        public class Arquivo
        {
            public string Nome { get; set; }
            public decimal TamanhoEmMB { get; set; }
        }

        public async Task Exercicio2Async()
        {
            Console.WriteLine("Informa uma lista de diretórios para contarmos a quantidade de arquivos...");
            Console.WriteLine("Digite 'S' para finalizar o input:");

            string diretorio = string.Empty;
            string comandoStop = "S";
            var listaDiretorios = new List<string>();

            do
            {
                try
                {
                    diretorio = Console.ReadLine();

                    if (!Directory.Exists(diretorio))
                    {
                        Console.WriteLine("-> Diretório não encontrado! Verifique o input novamente!");
                        continue;
                    }

                    listaDiretorios.Add(diretorio);
                }
                catch (Exception)
                {
                    Console.WriteLine($"Erro ao processar o input de diretórios. Tente novamente");
                }
            } while (!comandoStop.Equals(diretorio));

            var diretorioCalculado = new List<Diretorio>();
            var listaTasks = new List<Task>();

            foreach (var dir in listaDiretorios)
            {
                var taskDir = new Task(async () =>
                {
                    var diretorioCalculado = new Diretorio
                    {
                        Nome = dir
                    };

                    var arquivos = await findDirFilesAsync(dir);

                    diretorioCalculado.Arquivos = arquivos;
                    diretorioCalculado.TamanhoEmMB = arquivos.Sum(a => a.TamanhoEmMB);
                });

                listaTasks.Add(taskDir);
            }

            await Task.WhenAll(listaTasks);

            Console.WriteLine("=========================");
            Console.WriteLine("Processo finalizado!");
            Console.WriteLine("=========================");
        }

        public async Task<List<Arquivo>> findDirFilesAsync(string path)
        {
            var files = await Task.Run(() =>
            {
                var filesDir = Directory.GetFiles(path, "*", new EnumerationOptions() { RecurseSubdirectories = true });
                return filesDir;
            });

            var arquivos = new List<Arquivo>(files.Count());

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                arquivos.Add(new Arquivo
                {
                    Nome = fileInfo.Name,
                    TamanhoEmMB = ToSize(fileInfo.Length, SizeUnits.MB)
                });
            }

            return arquivos;
        }

        public enum SizeUnits
        {
            Byte, KB, MB, GB, TB, PB, EB, ZB, YB
        }

        public decimal ToSize(long value, SizeUnits unit)
        {
            return Math.Round(Convert.ToDecimal((value / (double)Math.Pow(1024, (long)unit))), 2);
        }
    }
}
