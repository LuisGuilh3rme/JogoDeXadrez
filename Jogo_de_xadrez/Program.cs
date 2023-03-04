using System;
using Jogo_de_xadrez.tabuleiro;
using Jogo_de_xadrez.Xadrez;

namespace Jogo_de_xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            PartidaXadrez partida = new();
            while (!partida.Terminada)
            {
                Console.Clear();
                Tela.MostrarTabuleiro(partida.Tab);

                Console.Write("Origem: ");
                Posicao origem = Tela.LerPosicaoXadrez().ConverterPosicao();

                bool[,] posicoesPossiveis = partida.Tab.Peca(origem).MovimentosPossiveis();
                Console.Clear();
                Tela.MostrarTabuleiro(partida.Tab, posicoesPossiveis);

                Console.Write("Destino: ");
                Posicao destino = Tela.LerPosicaoXadrez().ConverterPosicao();

                partida.ExecutarMovimento(origem, destino);
            }

        }
    }
}
