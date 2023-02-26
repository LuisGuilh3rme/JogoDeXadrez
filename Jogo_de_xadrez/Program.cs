using System;
using Jogo_de_xadrez.tabuleiro;
using Jogo_de_xadrez.Xadrez;

namespace Jogo_de_xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez pos = new('c', 7);
            Console.WriteLine(pos.ConverterPosicao());
        }
    }
}
