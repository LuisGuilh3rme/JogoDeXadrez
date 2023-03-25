using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Jogo_de_xadrez.tabuleiro;
using Jogo_de_xadrez.Xadrez;

namespace Jogo_de_xadrez
{
    public class Tela
    {
        public static void ExibirPartida(PartidaXadrez partida)
        {
            MostrarTabuleiro(partida.Tab);
            ExibirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: {0}", partida.Turno);

            if (partida.Terminada)
            {
                Console.WriteLine("Xeque-Mate!");
                Console.WriteLine("Vencedor: " + partida.JogadorAtual);
                return;
            }

            Console.WriteLine("Aguardado jogada: Peças {0}s", partida.JogadorAtual);
            Console.WriteLine();
            if (partida.Xeque)
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("XEQUE");
                Console.ForegroundColor = aux;
            }
        }

        public static void ExibirPecasCapturadas(PartidaXadrez partida) {
            Console.WriteLine();
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            ExibirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");

            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ExibirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = aux;

            Console.WriteLine();
        }

        public static void ExibirConjunto(HashSet<Peca> pecas)
        {
            Console.Write("[");
            foreach (Peca peca in pecas)
            {
                Console.Write(" {0} ", peca);
            }
            if (pecas.Count == 0) Console.Write("0");
            Console.Write("]");
        }

        public static void MostrarTabuleiro(Tabuleiro tab)
        {
            ConsoleColor aux = Console.ForegroundColor;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(8 - i + " |");
                Console.ForegroundColor = aux;

                for (int j = 0; j < tab.Colunas; j++)
                {
                    ExibirPeca(tab.Peca(i, j));
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("    a  b  c  d  e  f  g  h");
            Console.ForegroundColor = aux;
        }

        public static void MostrarTabuleiro(Tabuleiro tab, bool[,] mat)
        {
            ConsoleColor aux = Console.ForegroundColor;
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoEscuro = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(8 - i + " |");
                Console.ForegroundColor = aux;

                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (mat[i, j]) Console.BackgroundColor = fundoEscuro;
                    ExibirPeca(tab.Peca(i, j));
                    Console.BackgroundColor = fundoOriginal;

                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("    a  b  c  d  e  f  g  h");
            Console.ForegroundColor = aux;
        }

        public static void ExibirPeca(Peca peca)
        {
            if (peca == null) Console.Write(" - ");
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                if (peca.Cor == Cor.Branca)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.Write(" {0} ", peca);
                Console.ForegroundColor = aux;
            }
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string posicao = Console.ReadLine();
            Regex re = new ("[a-h]");

            if (!re.IsMatch(posicao))
            {
                throw new TabuleiroException("Posição inválida");
            }

            int linha = Convert.ToInt16(posicao[1] + " ");
            char coluna = posicao[0];
            return new PosicaoXadrez(coluna, linha);
        }
    }
}
