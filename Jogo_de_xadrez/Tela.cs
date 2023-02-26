﻿using System;
using Jogo_de_xadrez.tabuleiro;

namespace Jogo_de_xadrez
{
    public class Tela
    {
        public static void MostrarTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    Peca peca = tab.Peca(i, j);
                    if (peca == null) Console.Write("- ");
                    else ExibirPeca(peca);
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ExibirPeca(Peca peca)
        {

            ConsoleColor aux = Console.ForegroundColor;
            if (peca.Cor == Cor.Branca)
            {                
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue ; 
            }
            Console.Write(peca + " ");
            Console.ForegroundColor = aux;
        }
    }
}
