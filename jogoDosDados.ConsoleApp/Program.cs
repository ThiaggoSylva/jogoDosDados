using System.Security.Cryptography;

namespace jogoDosDados.ConsoleApp;

class Program
{
    const int limiteLinhaChegada = 30;
    const int bonusAvancoExtra = 3;
    const int penalidadeRecuo = 2;

    static void Main(string[] args)
    {
        while (true)
        {
            ExecutarJogo();

            Console.Write("Deseja continuar? (s/N): ");
            string? opcaoContinuar = Console.ReadLine()?.ToUpper();

            if (opcaoContinuar != "S")
                break;
        }
    }

    static void ExecutarJogo()
    {
        int posicaoJogador = 0;
        int posicaoComputador = 0;
        bool jogoEstaEmAndamento = true;

        while (jogoEstaEmAndamento)
        {
            RodadaJogador(ref posicaoJogador);

            if (VerificarVitoria(posicaoJogador, "Jogador"))
                break;

            RodadaComputador(ref posicaoComputador);

            if (VerificarVitoria(posicaoComputador, "Computador"))
                break;
        }
    }

    static void RodadaJogador(ref int posicaoJogador)
    {
        Console.Clear();
        ExibirCabecalho("Rodada do Jogador");

        Console.Write("Pressione ENTER para lançar um dado...");
        Console.ReadLine();

        int resultado = LancarDado();

        Console.WriteLine("-------------------------------------------");
        Console.WriteLine($"O número sorteado foi: {resultado}");
        Console.WriteLine("-------------------------------------------");

        posicaoJogador += resultado;

        posicaoJogador = VerificarEventos(posicaoJogador, "Você");

        Console.WriteLine($"\nVocê está na posição: {posicaoJogador} de {limiteLinhaChegada}");

        Console.Write("\nPressione ENTER para continuar...");
        Console.ReadLine();
    }

    static void RodadaComputador(ref int posicaoComputador)
    {
        Console.Clear();
        ExibirCabecalho("Rodada do Computador");

        int resultado = LancarDado();

        Console.WriteLine($"O número sorteado foi: {resultado}");
        Console.WriteLine("-------------------------------------------");

        posicaoComputador += resultado;

        posicaoComputador = VerificarEventos(posicaoComputador, "O computador");

        Console.WriteLine($"\nO computador está na posição: {posicaoComputador} de {limiteLinhaChegada}");

        Console.Write("\nPressione ENTER para continuar...");
        Console.ReadLine();
    }

    static int LancarDado()
    {
        return RandomNumberGenerator.GetInt32(1, 7);
    }

    static int VerificarEventos(int posicao, string jogador)
    {
        if (posicao == 5 || posicao == 10 || posicao == 15 || posicao == 25)
        {
            Console.WriteLine($"\nEVENTO: Avanço de {bonusAvancoExtra} casas!");
            posicao += bonusAvancoExtra;
        }
        else if (posicao == 7 || posicao == 13 || posicao == 20)
        {
            Console.WriteLine($"\nEVENTO: Recuo de {penalidadeRecuo} casas!");
            posicao -= penalidadeRecuo;
        }

        return posicao;
    }

    static bool VerificarVitoria(int posicao, string jogador)
    {
        if (posicao >= limiteLinhaChegada)
        {
            Console.WriteLine($"\n{jogador} alcançou a linha de chegada!");

            Console.Write("\nPressione ENTER para continuar...");
            Console.ReadLine();

            return true;
        }

        return false;
    }

    static void ExibirCabecalho(string titulo)
    {
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine("Jogo dos Dados");
        Console.WriteLine("-------------------------------------------");
        Console.WriteLine(titulo);
        Console.WriteLine("-------------------------------------------");
    }
}