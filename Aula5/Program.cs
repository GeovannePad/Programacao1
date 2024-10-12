using ConsoleApp.Aula5.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp.Aula5
{
    class Program
    {
        public enum TipoAutor
        {
            Principal,
            Coautor
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello! Aula 5 - Interações com banco de dados");


            Console.WriteLine("Inicializando o DbContext:");
            //Inicializa nosso ApplicationContext (herda de DbContext)
            ApplicationContext applicationContext = new ApplicationContext();

            Console.WriteLine("Verificando conexão com o banco...");
            //testa conexão com o banco
            var canConnect = await applicationContext.Database.CanConnectAsync();
            if (!canConnect)
            {
                //não foi possível conectar no BD, revise sua string de conexão
                Console.WriteLine("SUA CONEXÃO NÃO PODE SER ESTABELECIDA! Verifique sua string de conexão!");
                return;
            }
            Console.WriteLine("Conexão estabelecida com sucesso!");
            Console.WriteLine("------------------------------------------");

            Console.WriteLine("Verificando se o database inicial existe...");

            //Verifica se o database existe, se não existir, cria
            await applicationContext.Database.EnsureCreatedAsync();

            Console.WriteLine("------------------------------------------");


            Console.WriteLine("Inserindo autores....");
            //O método “CriaAutores” retorna uma lista com 2 autores
            await CriaAutoresAsync(applicationContext);

            Console.WriteLine("Atualizando url autor 2....");
            await AtualizaUrlAsync(applicationContext);

            Console.WriteLine("Deletando autor de exemplo....");
            await DeletaAutorExemplo(applicationContext);

            Console.WriteLine("Exemplo de consulta1....");
            await ConsultaExemplo1(applicationContext);

            Console.WriteLine("--------------------");
            Console.WriteLine("Carregando dados para as consultas mais complexas....");
            await CarregaDadosAsync(applicationContext);
            Console.WriteLine("--------------------");

            Console.WriteLine("Querys complexas com Entity:");
            await QueryComplexas1(applicationContext);


            Console.WriteLine("--------------------");
            Console.WriteLine("Crud Dapper");
            await ExecutaOperacoesCrudDapper();

            Console.WriteLine("--------------------");
            Console.WriteLine("Querys Dapper:");
            await ExecutaQuerysComplexasDapper();
        }

        private static async Task CriaAutoresAsync(ApplicationContext dbContext)
        {
            //verifica se os autores iniciais existem
            if (await dbContext.Autores.AnyAsync(a => a.Nome == "JON SMITH"))
            {
                //se sim, não cria novamente
                return;
            }

            Autor autor1 = new Autor
            {
                Nome = "JON SMITH"
            };

            Autor autor2 = new Autor
            {
                Nome = "JOSEPH ALBAHARI"
            };

            var autores = new List<Autor>(2) { autor1, autor2 };
            foreach (var autor in autores)
            {
                //Adiciona no contexto do DbSet<Autor>
                dbContext.Autores.Add(autor);
            }

            //Salva as alterações
            await dbContext.SaveChangesAsync();
        }

        private static async Task AtualizaUrlAsync(ApplicationContext dbContext)
        {
            //busca o objeto pela sua PK
            //como nossa PK é uma int, estamos passando o valor da PK do autor 2 (JOSEPH ALBAHARI)
            //observe como estamos acessando o DbSet Autores (DbSet<Autor>)
            var autorAlbahari = await dbContext.Autores.FindAsync(2);

            //atualiza um valor de um propriedade
            autorAlbahari.WebUrl = "https://www.oreilly.com/learning-paths/learning-path-clean/8204091500000000001/";

            //salva no banco de dados as mudanças
            await dbContext.SaveChangesAsync();

            Console.WriteLine("Url atualizada....");
        }

        private static async Task DeletaAutorExemplo(ApplicationContext dbContext)
        {
            //Cria um autor de exemplo para deletar
            dbContext.Autores.Add(new Autor() { Nome = "autorParaDeletar", WebUrl = "google.com.br" });

            //salva no banco de dados o novo autor
            await dbContext.SaveChangesAsync();


            //busca o novo autor pelo seu nome
            var autorParaDeletar = await dbContext.Autores.Where(a => a.Nome == "autorParaDeletar")
                                               .FirstOrDefaultAsync();
            if (autorParaDeletar != null)
            {
                Console.WriteLine($"Autor para deletar encontrado com o id: {autorParaDeletar.AutorId}");
            }

            //marca o objeto para deleção pelo EF
            dbContext.Autores.Remove(autorParaDeletar);

            //salva no banco de dados as alteraões, neste caso,
            //o novo autor com o nome autorParaDeletar será deletado
            await dbContext.SaveChangesAsync();

            Console.WriteLine("Autor deletado");
        }

        private static async Task ConsultaExemplo1(ApplicationContext dbContext)
        {
            List<Autor> autoresComUrl = await dbContext.Autores.Where(a => a.WebUrl != null).ToListAsync();

            Console.WriteLine("Count autoresComUrl:" + autoresComUrl.Count);

            List<string> nomes = await dbContext.Autores.Select(a => a.Nome)
                                                        .ToListAsync();

            Console.WriteLine("Nomes retornados: " + nomes.Count);
            foreach (var nome in nomes)
            {
                Console.Write($"{nome},");
            }
        }

        private static async Task CarregaDadosAsync(ApplicationContext dbContext)
        {
            //valida se já rodou
            if (await dbContext.Autores.AnyAsync(a => a.Nome == "ALAN ARAYA"))
            {
                return;
            }

            var autor = new Autor() { Nome = "ALAN ARAYA", WebUrl = "https://github.com/alan-araya" };

            dbContext.Autores.Add(autor);

            //adiciona novos livros
            var livros = new List<Livro>()
            {
                new Livro(){ Titulo = "C# Rocks", Descricao = "C# é a melhor linguagem", PublicadoEm = new DateTime(2019,06,01)},
                new Livro(){ Titulo = "Java Fundamentos", Descricao = "Fundamentos de programação em Java", PublicadoEm = new DateTime(2005,05,05)},
                new Livro(){ Titulo = "Asp.Net Core Zero-To-Hero", Descricao = "Asp.Net zero to hero lhe transforma em um programador web excelente", PublicadoEm = new DateTime(2018,01,01)},
                new Livro(){ Titulo = "O Futuro da Quantica", Descricao = "Discussao metafisica de Fisica Quantica", PublicadoEm = new DateTime(2016,06,30)},
                new Livro(){ Titulo = "Linguagens de programacao", Descricao = "Um olhar holistico para linguagens de programacao", PublicadoEm = new DateTime(2016,06,01)},
                new Livro(){ Titulo = "Windows Forms morreu! Salve o WPF", Descricao = "Visão sobre o novo conceito de dev para desktops", PublicadoEm = new DateTime(2017,05,01)},
                new Livro(){ Titulo = "Xamarin Rocks!", Descricao = "C# para mobile, melhor impossível!", PublicadoEm = new DateTime(2019,06,01)},
                new Livro(){ Titulo = "Xamarin Fundamentos", Descricao = "Fundamentos de Xamarin", PublicadoEm = new DateTime(2017,05,01)},
                new Livro(){ Titulo = "Xamarin Zero-To-Hero", Descricao = "Zero to hero series", PublicadoEm = new DateTime(2018,05,01)},
                new Livro(){ Titulo = "Xamarin e MUAI components", Descricao = "Xamarin MUAI - Multi-platform App UI", PublicadoEm = new DateTime(2019,06,01)},
                new Livro(){ Titulo = "Xamarin navigations", Descricao = "Dominando a navegação em Apps Xamarin", PublicadoEm = new DateTime(2020,05,01)},
                new Livro(){ Titulo = "Asp.Net APIs", Descricao = "dominando as APIs em .NET", PublicadoEm = new DateTime(2021,06,01)},
                new Livro(){ Titulo = "Asp.Net Microservices", Descricao = "Microservices o futuro!", PublicadoEm = new DateTime(2021,06,01)},
                new Livro(){ Titulo = "Asp.Net com Kubernetes", Descricao = "K8s + AspNet", PublicadoEm = new DateTime(2018,06,01)},
                new Livro(){ Titulo = "Kotlin Fundamentos", Descricao = "Fundamentos de programação em Kotlin", PublicadoEm = new DateTime(2016,05,01)},
                new Livro(){ Titulo = "C# Fundamentos", Descricao = "Fundamentos de programação em C#", PublicadoEm = new DateTime(2016,05,01)},
           };

            foreach (var livro in livros)
            {
                dbContext.Livros.Add(livro);
            }

            await dbContext.SaveChangesAsync();

            //Adiciona reviews
            livros = await dbContext.Livros.ToListAsync();

            await CarregaDadosReviewsAsync(dbContext, livros);

            //Adicona o vinculo com os autores
            var autores = await dbContext.Autores.ToListAsync();// busca todos os autores da base (select * from Autores)

            await VinculaLivroNosAutores(dbContext, livros, autores);
        }

        private static async Task CarregaDadosReviewsAsync(ApplicationContext dbContext, List<Livro> livros)
        {
            var qtdMaxReviews = 100;
            var randomazier = new Random();
            var nomes = new string[] { "ALAN", "JOSE", "MARIA", "LUCAS", "JOÃO", "MARCELO", "SIMONE", "JOSIANE", "PATRICK", "LAUREN", "GABRIELA", "NICOLE", "ISABELA", "MATHEUS", "THIAGO", "GABRIEL", "PAULO", "EDUARDO" };
            var estrelasComment = new Dictionary<int, string>();
            estrelasComment.Add(1, "Achei fraco!");
            estrelasComment.Add(2, "Mais ou menos");
            estrelasComment.Add(3, "Livro bom, mas poderia melhorar!");
            estrelasComment.Add(4, "Livro muito bom!");
            estrelasComment.Add(5, "Livro Topzera!");

            foreach (var livro in livros)
            {
                livro.Reviews = new List<Review>();

                var qtdReviews = randomazier.Next(5, qtdMaxReviews);
                for (int i = 0; i < qtdReviews; i++)
                {
                    var nota = randomazier.Next(1, 5);
                    var novaReview = new Review()
                    {
                        NomeRevisor = nomes[randomazier.Next(0, nomes.Length - 1)],
                        QtdEstrelas = nota,
                        Comentario = estrelasComment[nota],
                        Livro = livro,
                        LivroId = livro.LivroId
                    };

                    livro.Reviews.Add(novaReview);
                }
            }

            await dbContext.SaveChangesAsync();
        }

        private static async Task VinculaLivroNosAutores(ApplicationContext dbContext, List<Livro> livros, List<Autor> autores)
        {
            var randomazier = new Random();
            int qtdMaxAutor = 4;
            var enumValues = Enum.GetValues(typeof(TipoAutor));

            foreach (var livro in livros)
            {
                livro.Autores = new List<LivroAutor>();
                var qtdAutor = randomazier.Next(1, qtdMaxAutor);

                //adiciona os autores no livro
                for (int i = 0; i < qtdAutor; i++)
                {
                    //seleciona um autor 
                    var autor = autores[i];

                    livro.Autores.Add(new LivroAutor()
                    {
                        Autor = autor,
                        AutorId = autor.AutorId,
                        Livro = livro,
                        LivroId = livro.LivroId,
                        Tipo = qtdAutor == 1 ? TipoAutor.Principal.ToString() : enumValues.GetValue(randomazier.Next(1, enumValues.Length - 1)).ToString()
                    });
                }

                //verifica se teve pelo menos um autor principal, do contrario altera o primeiro para principal
                //para sempre mantermos pelo menos 1 autor como principal
                if (!livro.Autores.Any(a => a.Tipo == TipoAutor.Principal.ToString()))
                {
                    livro.Autores[0].Tipo = TipoAutor.Principal.ToString();
                }
            }

            await dbContext.SaveChangesAsync();
        }

        private static async Task QueryComplexas1(ApplicationContext dbContext)
        {
            Console.WriteLine($"-------------------------------------------------------------------------------------");

            //Busca todos os Autores baseado nas reviews de seus livros
            //do autor mais bem avaliado para o menor
            var autoresAvaliacao = dbContext.Autores
                                            .Include(a => a.Livros)
                                            .Select(a => new
                                            {
                                                autor = a,
                                                qtdPontuacao = a.Livros.Sum(autorLivro => autorLivro.Livro.Reviews.Sum(r => r.QtdEstrelas))
                                            })
                                            .OrderByDescending(x => x.qtdPontuacao);

            var queryautoresAvalicao = autoresAvaliacao.ToQueryString();

            Console.WriteLine($"Query autoriasAvaliacao");
            foreach (var elemento in await autoresAvaliacao.ToListAsync())
            {
                Console.WriteLine($"Autor: {elemento.autor.Nome} | Qtd Pontuacao: {elemento.qtdPontuacao}");
            }

            Console.WriteLine($"-------------------------------------------------------------------------------------");

            //Busca os livros com as melhores reviews (de 3 a 5) agrupados pode ano
            var melhoresLivrosPorAno = dbContext.Livros.AsNoTracking()
                                                .Include(l => l.Reviews.Where(r => r.QtdEstrelas >= 3));

            var querymelhoresLivrosPorAno = melhoresLivrosPorAno.ToQueryString();


            var livros = await melhoresLivrosPorAno.ToListAsync();
            var livrosAgrupadosPorAno = livros.GroupBy(l => l.PublicadoEm.Year)
                                              .OrderBy(g => g.Key);

            Console.WriteLine($"Query queryMelhoresLivrosPorAno:");
            foreach (var elementoAgrupado in livrosAgrupadosPorAno)
            {
                Console.WriteLine($"Ano: {elementoAgrupado.Key}");
                foreach (var livro in elementoAgrupado.OrderByDescending(l => l.Reviews.Sum(r => r.QtdEstrelas)))
                {
                    Console.WriteLine($"    Média: {Math.Round(livro.Reviews.Average(r => r.QtdEstrelas), 2)} | Pontuacao: {livro.Reviews.Sum(r => r.QtdEstrelas)} | Livro: {livro.Titulo} - {livro.Descricao}");
                }

            }

            Console.WriteLine($"-------------------------------------------------------------------------------------");

            //Autores com a maior quantidade de livros
            var autoresQtdLivros = dbContext.Autores.AsNoTracking()
                                            .Include(a => a.Livros)
                                            .Select(a => new
                                            {
                                                Publicacoes = a.Livros.Count,
                                                PublicacoesComoAutorPrincial = a.Livros.Where(l => l.Tipo == TipoAutor.Principal.ToString()).Count(),
                                                PublicacoesComoCoautor = a.Livros.Where(l => l.Tipo == TipoAutor.Coautor.ToString()).Count(),
                                                Nome = a.Nome
                                            })
                                            .OrderByDescending(x => x.Publicacoes);

            var queryautoresQtdLivros = autoresQtdLivros.ToQueryString();

            foreach (var item in await autoresQtdLivros.ToListAsync())
            {
                Console.WriteLine($"Autor: {item.Nome} | QtdPublicacoes: {item.Publicacoes} | Como principal: {item.PublicacoesComoAutorPrincial} | Como CoAutor: {item.PublicacoesComoCoautor}");
            }

            Console.WriteLine($"-------------------------------------------------------------------------------------");

            //Top 3 livros menos avaliados e seus comentários de avalição
            var livrosPoucoAvaliados = dbContext.Livros
                                                .Include(l => l.Reviews)
                                                .Select(l => new
                                                {
                                                    Livro = l.Titulo,
                                                    QtdPontuacao = l.Reviews.Sum(r => r.QtdEstrelas),
                                                    ReviewsBaixa = l.Reviews.Where(r => r.QtdEstrelas < 3)
                                                })
                                                .OrderBy(x => x.QtdPontuacao)
                                                .Take(3);


            var querylivrosPoucoAvaliados = livrosPoucoAvaliados.ToQueryString();

            foreach (var item in await livrosPoucoAvaliados.ToListAsync())
            {
                Console.WriteLine($"Livro: {item.Livro} | Pontuação: {item.QtdPontuacao}");
                Console.WriteLine($"    Reviews:");
                foreach (var review in item.ReviewsBaixa)
                {
                    Console.WriteLine($"    -Comentário:{review.Comentario} - Revisor: {review.NomeRevisor}");
                }
            }
        }


        private static async Task ExecutaOperacoesCrudDapper()
        {
            var dapperConext = new DapperContext();
            var autores = await dapperConext.ListaTodosAutores();

            var autor1 = autores.FirstOrDefault();
            autor1.WebUrl = "microsoft.com";

            await dapperConext.UpdateAutorAsync(autor1);
        }

        private static async Task ExecutaQuerysComplexasDapper()
        {
            var dapperContext = new DapperContext();


            Console.WriteLine("Query 4 Dapper:");

            var listaLivrosBaixaPontuacao = await dapperContext.ListaTop3LivrosPoucoAvaliadosEReviews();

            var livros = listaLivrosBaixaPontuacao.GroupBy(l => new { l.LivroId, l.Titulo, l.QtdPontuacao });

            foreach (var item in livros)
            {
                Console.WriteLine($"Livro: {item.Key.Titulo} | Pontuação: {item.Key.QtdPontuacao}");
                Console.WriteLine($"    Reviews:");

                foreach (var review in item)
                {
                    Console.WriteLine($"    -Comentário:{review.Comentario} - Revisor: {review.NomeRevisor}");
                }
            }

            //---------------------------------------------------------------------------------------------

            Console.WriteLine("Query 2 Dapper:");

            var livrosPontuacaoDto = await dapperContext.ListaLivrosPorAnoEPontuacao();
            var livrosAgrupadosPorAno = livrosPontuacaoDto.GroupBy(l => l.AnoPublicacao)
                                                          .OrderBy(g => g.Key);

            foreach (var elementoAgrupado in livrosAgrupadosPorAno)
            {
                Console.WriteLine($"Ano: {elementoAgrupado.Key}");
                foreach (var livro in elementoAgrupado.OrderByDescending(l => l.Pontuacao))
                {
                    Console.WriteLine($"    Média: {Math.Round(livro.Media, 2)} | Pontuacao: {livro.Pontuacao} | Livro: {livro.Titulo} - {livro.Descricao}");
                }
            }
        }
    }
}