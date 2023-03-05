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
                try
                {

                    Console.Clear();
                    Tela.MostrarTabuleiro(partida.Tab);

                    Console.WriteLine();
                    Console.WriteLine("Turno: {0}", partida.Turno);
                    Console.WriteLine("Aguardado jogada: Peças {0}s", partida.JogadorAtual);
                    Console.WriteLine();

                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ConverterPosicao();
                    partida.ValidarPosicaoOrigem(origem);


                    bool[,] posicoesPossiveis = partida.Tab.Peca(origem).MovimentosPossiveis();
                    Console.Clear();
                    Tela.MostrarTabuleiro(partida.Tab, posicoesPossiveis);

                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ConverterPosicao();
                    partida.ValidarPosicaoDestino(origem, destino);

                    partida.RealizaJogada(origem, destino);
                } catch(TabuleiroException e)
                {
                    Console.WriteLine();
                    Console.WriteLine("Erro: {0}", e.Message);
                    Console.Write("Aperte qualquer botão para continuar");
                    Console.ReadKey();
                }

            }

        }
    }
}
