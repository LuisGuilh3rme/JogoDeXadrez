using System;
using Jogo_de_xadrez.tabuleiro;

namespace Jogo_de_xadrez
{
    public class Tela
    {
        public static void MostrarTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                for (int j = 0; j < tab.Colunas; j++)
                {
                    Peca peca = tab.Peca(i, j);
                    if (peca == null) Console.Write("- ");
                    else Console.Write("{0} ", peca);
                }
                Console.WriteLine();
            }
        }
    }
}
