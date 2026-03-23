using System.Security.Cryptography;

namespace JogoDosDados.ConsoleApp;

static class Jogador
{
    public static int posicao = 0;
    private const int limiteLinhaChegada = 30;
    private const int bonusAvancoExtra = 3;
    private const int penalidadeRecuo = 2;

    public static void ExecutarRodada()
    {
        do
        {
            ExibirCabecalho();

            Console.Write("Pressione ENTER para lançar um dado...");
            Console.ReadLine();

            int resultadoJogador = RandomNumberGenerator.GetInt32(1, 7);

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"O número sorteado foi: {resultadoJogador}");
            Console.WriteLine("-------------------------------------------");

            posicao += resultadoJogador;

            Console.WriteLine($"Você está na posição: {posicao} de {limiteLinhaChegada}");

            if (posicao == 5 || posicao == 10 || posicao == 15 || posicao == 25)
            {
                Console.WriteLine($"\nEVENTO: Avanço de {bonusAvancoExtra} casas!");
                posicao += bonusAvancoExtra;

                Console.WriteLine($"\nVocê está na posição: {posicao} de {limiteLinhaChegada}");
            }

            else if (posicao == 7 || posicao == 13 || posicao == 20)
            {
                Console.WriteLine($"\nEVENTO: Recuo de {penalidadeRecuo} casas");
                posicao -= penalidadeRecuo;

                Console.WriteLine($"\nVocê está na posição: {posicao} de {limiteLinhaChegada}");
            }

            if (posicao >= limiteLinhaChegada)
            {
                Console.WriteLine("\nParabéns! Você alcançou a linha de chegada.");

                Console.Write("\nPressione ENTER para continuar...");
                Console.ReadLine();

                break;
            }

            if (resultadoJogador == 6)
            {
                Console.WriteLine($"\nEVENTO: Rodada Extra!");
                Console.WriteLine("-------------------------------------------");

                Console.Write("\nPressione ENTER para jogar novamente...");
                Console.ReadLine();

                continue;
            }
            else
            {
                Console.Write("\nPressione ENTER para continuar...");
                Console.ReadLine();

                break;
            }
        } while (true);
    }

    public static bool Venceu()
    {
        return posicao >= limiteLinhaChegada;
    }

    private static void ExibirCabecalho()
    {
        Console.Clear();
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("Jogo dos Dados");
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine($"Rodada do Jogador");
        Console.WriteLine("-------------------------------------------");
    }
}