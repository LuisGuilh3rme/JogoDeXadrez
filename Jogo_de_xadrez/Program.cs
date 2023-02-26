using System;
using Jogo_de_xadrez.tabuleiro;

namespace Jogo_de_xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tela.MostrarTabuleiro(new Tabuleiro(8, 8));
        }
    }
}
