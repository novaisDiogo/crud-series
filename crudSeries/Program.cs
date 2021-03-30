using System;
using crudSeries.classes;
using crudSeries.Enum;

namespace crudSeries
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static FilmeRepositorio filmeRepositorio = new FilmeRepositorio();
        static void Main(string[] args)
        {
            string opcao = OpcaoFilmeOuSerie();

            while (opcao.ToUpper() != "X")
            {

                if (opcao == "1")
                {
                    string opcaoUsuarioFilme = ObterOpcaoUsuarioFilme();

                    switch (opcaoUsuarioFilme)
                    {
                        case "1":
                            ListarFilmes();
                            break;
                        case "2":
                            InserirFilme();
                            break;
                        case "3":
                            AtualizarFilme();
                            break;
                        case "4":
                            ExcluirFilme();
                            break;
                        case "5":
                            VisualizarFilme();
                            break;
                        case "C":
                            Console.Clear();
                            break;
                        case "V":
                            opcao = OpcaoFilmeOuSerie();
                            break;
                        default:
                            Console.WriteLine("A opção digitada não existe! insira as opções listadas!");
                            break;
                    }
                }
                else if (opcao == "2")
                {
                    string opcaoUsuario = ObterOpcaoUsuarioSerie();

                    switch (opcaoUsuario)
                    {
                        case "1":
                            ListarSeries();
                            break;
                        case "2":
                            InserirSerie();
                            break;
                        case "3":
                            AtualizarSerie();
                            break;
                        case "4":
                            ExcluirSerie();
                            break;
                        case "5":
                            VisualizarSerie();
                            break;
                        case "C":
                            Console.Clear();
                            break;
                        case "V":
                            opcao = OpcaoFilmeOuSerie();
                            break;
                        default:
                            Console.WriteLine("A opção digitada não existe! insira as opções listadas!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("A opção digitada não existe! insira as opções listadas!");
                    opcao = OpcaoFilmeOuSerie();
                }

            }

            Console.WriteLine("Obrigado por utilizar nossos serviços.");
            Console.ReadLine();
        }

        #region Filme
        private static void ListarFilmes()
        {
            var filmes = filmeRepositorio.Lista();

            if(filmes.Count == 0)
            {
                Console.WriteLine("Nenhum filme cadastrado.");
                return;
            }
            else
            {
                foreach(var filme in filmes)
                {
                    var excluido = filme.RetornaExcluido();

                    Console.WriteLine("#ID {0}: - {1} {2}", filme.RetornaId(), filme.RetornaTitulo(), (excluido ? "*Excluído*" : ""));
                }
            }
        }

        private static void InserirFilme()
        {
            Console.WriteLine("Inserir novo filme");

            foreach(int e in System.Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", e, System.Enum.GetName(typeof(Genero), e));
            }

            try
            {
                Console.Write("Digite o gênero entre as opções acima: ");
                int entradaGenero = int.Parse(Console.ReadLine());

                Console.Write("Digite o Título do filme: ");
                string entradaTitulo = Console.ReadLine();

                Console.Write("Digite o Ano de Início do filme: ");
                int entradaAno = int.Parse(Console.ReadLine());

                Console.Write("Digite a Descrição do filme: ");
                string entradaDescricao = Console.ReadLine();

                Filme filme = new Filme(id: filmeRepositorio.ProximoId(),
                    genero: (Genero)entradaGenero, titulo: entradaTitulo, descricao: entradaDescricao,
                    ano: entradaAno);

                filmeRepositorio.Insere(filme);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Erro, Formato digitado não valido, digite um numero inteiro para genero e ano!");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("Erro, Formato digitado não valido, digite um numero inteiro para genero e ano!");
                Console.WriteLine("Erro: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex);
            }

        }

        private static void AtualizarFilme()
        {
            try
            {
                Console.Write("Digite o id do filme: ");
                int indiceFilme = int.Parse(Console.ReadLine());

                foreach (int e in System.Enum.GetValues(typeof(Genero)))
                {
                    Console.WriteLine("{0}-{1}", e, System.Enum.GetName(typeof(Genero), e));
                }

                Console.Write("Digite o gênero entre as opções acima: ");
                int entradaGenero = int.Parse(Console.ReadLine());

                Console.Write("Digite o Título do filme: ");
                string entradaTitulo = Console.ReadLine();

                Console.Write("Digite o Ano de Início do filme: ");
                int entradaAno = int.Parse(Console.ReadLine());

                Console.Write("Digite a Descrição do filme: ");
                string entradaDescricao = Console.ReadLine();

                Filme filme = new Filme(id: filmeRepositorio.ProximoId(),
                    genero: (Genero)entradaGenero, titulo: entradaTitulo, descricao: entradaDescricao,
                    ano: entradaAno);

                filmeRepositorio.Atualiza(indiceFilme, filme);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Erro, Formato digitado não valido, digite um numero inteiro para id, genero e ano!");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("Erro, Formato digitado não valido, digite um numero inteiro para id, genero e ano!");
                Console.WriteLine("Erro: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex);
            }
        }

        private static void ExcluirFilme()
        {
            try
            {
                Console.Write("Digite o id do filme: ");
                int indiceFilme = int.Parse(Console.ReadLine());

                Console.WriteLine("Tem certeza que deseja excluir? [0] NÃO [1] SIM");
                int excluir = int.Parse(Console.ReadLine());

                if (excluir == 1)
                {
                    filmeRepositorio.Exclui(indiceFilme);
                }
                else
                {
                    return;
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Erro, Formato digitado não valido, as opções devem ser um numero inteiro!");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("Erro, Formato digitado não valido, as opções devem ser um numero inteiro!");
                Console.WriteLine("Erro: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex);
            }
        }

        private static void VisualizarFilme()
        {
            try
            {
                Console.Write("Digite o id do filme: ");
                int indiceFilme = int.Parse(Console.ReadLine());

                var filme = filmeRepositorio.RetornaPorId(indiceFilme);

                Console.WriteLine(filme);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Erro, Formato digitado não valido, id deve ser um numero inteiro!");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("Erro, Formato digitado não valido, id deve ser um numero inteiro!");
                Console.WriteLine("Erro: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex);
            }
        }
        #endregion

        #region Serie
        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            foreach (int i in System.Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, System.Enum.GetName(typeof(Genero), i));
            }

            try
            {
                Console.Write("Digite o gênero entre as opções acima: ");
                int entradaGenero = int.Parse(Console.ReadLine());

                Console.Write("Digite o Título da Série: ");
                string entradaTitulo = Console.ReadLine();

                Console.Write("Digite o Ano de Início da Série: ");
                int entradaAno = int.Parse(Console.ReadLine());

                Console.Write("Digite a Descrição da Série: ");
                string entradaDescricao = Console.ReadLine();

                Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                            genero: (Genero)entradaGenero,
                                            titulo: entradaTitulo,
                                            ano: entradaAno,
                                            descricao: entradaDescricao);

                repositorio.Insere(novaSerie);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Erro, Formato digitado não valido, as opções genero e ano devem ser um numero inteiro!");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("Erro, Formato digitado não valido, as opções genero e ano devem ser um numero inteiro!");
                Console.WriteLine("Erro: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex);
            }
        }

        private static void AtualizarSerie()
        {
            try
            {
                Console.Write("Digite o id da série: ");
                int indiceSerie = int.Parse(Console.ReadLine());

                foreach (int i in System.Enum.GetValues(typeof(Genero)))
                {
                    Console.WriteLine("{0}-{1}", i, System.Enum.GetName(typeof(Genero), i));
                }
                Console.Write("Digite o gênero entre as opções acima: ");
                int entradaGenero = int.Parse(Console.ReadLine());

                Console.Write("Digite o Título da Série: ");
                string entradaTitulo = Console.ReadLine();

                Console.Write("Digite o Ano de Início da Série: ");
                int entradaAno = int.Parse(Console.ReadLine());

                Console.Write("Digite a Descrição da Série: ");
                string entradaDescricao = Console.ReadLine();

                Serie atualizaSerie = new Serie(id: indiceSerie,
                                            genero: (Genero)entradaGenero,
                                            titulo: entradaTitulo,
                                            ano: entradaAno,
                                            descricao: entradaDescricao);

                repositorio.Atualiza(indiceSerie, atualizaSerie);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Erro, Formato digitado não valido, id, genero e ano devem ser um numero inteiro!");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("Erro, Formato digitado não valido, id, genero e ano devem ser um numero inteiro!");
                Console.WriteLine("Erro: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex);
            }
        }

        private static void ExcluirSerie()
        {
            try
            {
                Console.Write("Digite o id da série: ");
                int indiceSerie = int.Parse(Console.ReadLine());

                Console.WriteLine("Tem certeza que deseja excluir? [0] NÃO [1] SIM");
                int excluir = int.Parse(Console.ReadLine());

                if (excluir == 1)
                {
                    repositorio.Exclui(indiceSerie);
                }
                else
                {
                    return;
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Erro, Formato digitado não valido, as opções devem ser um numero inteiro!");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("Erro, Formato digitado não valido, as opções devem ser um numero inteiro!");
                Console.WriteLine("Erro: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex);
            }
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada.");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();

                Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*" : ""));
            }
        }

        private static void VisualizarSerie()
        {
            try
            {
                Console.Write("Digite o id da série: ");
                int indiceSerie = int.Parse(Console.ReadLine());

                var serie = repositorio.RetornaPorId(indiceSerie);

                Console.WriteLine(serie);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Erro, Formato digitado não valido, id deve ser numero inteiro!");
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("Erro, Formato digitado não valido, id deve ser numero inteiro!");
                Console.WriteLine("Erro: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro: " + ex);
            }
        }
        #endregion

        #region Opções
        private static string ObterOpcaoUsuarioSerie()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Séries!!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar séries");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Atualizar série");
            Console.WriteLine("4- Excluir série");
            Console.WriteLine("5- Visualizar série");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("V- Voltar");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

        private static string ObterOpcaoUsuarioFilme()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Filmes!!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Listar filmes");
            Console.WriteLine("2- Inserir novo filme");
            Console.WriteLine("3- Atualizar filme");
            Console.WriteLine("4- Excluir filme");
            Console.WriteLine("5- Visualizar filme");
            Console.WriteLine("C- Limpar Tela");
            Console.WriteLine("V- Voltar");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }

        private static string OpcaoFilmeOuSerie()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Séries e Filmes a seu dispor!!!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1- Filmes");
            Console.WriteLine("2- Series");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
        #endregion
    }
}
