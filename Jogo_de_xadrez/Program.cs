using System;
using Jogo_de_xadrez.tabuleiro;
using Jogo_de_xadrez.Xadrez;

namespace Jogo_de_xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new(8, 8);
            try
            {
                tab.ColocarPeca(new Torre(Cor.Preta, tab), new Posicao(0, 0));
                tab.ColocarPeca(new Torre(Cor.Preta, tab), new Posicao(1, 3));
                tab.ColocarPeca(new Rei(Cor.Branca, tab), new Posicao(0, 5));
                Tela.MostrarTabuleiro(tab);
            } catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
