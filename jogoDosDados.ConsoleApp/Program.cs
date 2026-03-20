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

            Console.Write("\nDeseja continuar? (s/N): ");
            string? opcao = Console.ReadLine()?.ToUpper();

            if (opcao != "S")
                break;
        }
    }

    static void ExecutarJogo()
    {
        int posicaoJogador = 0;
        int posicaoComputador = 0;

        while (true)
        {
            bool rodadaExtra;

            // Rodada do jogador
            do
            {
                rodadaExtra = ExecutarRodada("Jogador", ref posicaoJogador, true);

                if (VerificarVitoria(posicaoJogador, "Jogador"))
                    return;

            } while (rodadaExtra);

            // Rodada do computador
            do
            {
                rodadaExtra = ExecutarRodada("Computador", ref posicaoComputador, false);

                if (VerificarVitoria(posicaoComputador, "Computador"))
                    return;

            } while (rodadaExtra);
        }
    }

    static bool ExecutarRodada(string nome, ref int posicao, bool aguardarEntrada)
    {
        Console.Clear();
        ExibirCabecalho($"Rodada do {nome}");

        if (aguardarEntrada)
        {
            Console.Write("Pressione ENTER para lançar o dado...");
            Console.ReadLine();
        }

        int resultado = LancarDado();

        Console.WriteLine("-------------------------------------------");
        Console.WriteLine($"O número sorteado foi: {resultado}");
        Console.WriteLine("-------------------------------------------");

        posicao += resultado;

        posicao = VerificarEventos(posicao);

        Console.WriteLine($"\n{nome} está na posição: {posicao} de {limiteLinhaChegada}");

        if (resultado == 6)
        {
            Console.WriteLine($"\n🎉 {nome} tirou 6 e ganhou uma rodada extra!");
        }

        Console.Write("\nPressione ENTER para continuar...");
        Console.ReadLine();

        return resultado == 6;
    }

    static int LancarDado()
    {
        return RandomNumberGenerator.GetInt32(1, 7);
    }

    static int VerificarEventos(int posicao)
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
            Console.WriteLine($"\n🏆 {jogador} alcançou a linha de chegada!");

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