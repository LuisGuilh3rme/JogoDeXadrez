using System;
namespace Jogo_de_xadrez.tabuleiro
{
    public class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QuantidadeMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca (Posicao posicao, Cor cor, Tabuleiro tabuleiro)
        {
            Posicao = posicao;
            Cor = cor;
            Tab = tabuleiro;
            QuantidadeMovimentos = 0;
        }
    }
}
