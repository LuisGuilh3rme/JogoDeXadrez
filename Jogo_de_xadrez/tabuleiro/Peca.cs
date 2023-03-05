using System;
namespace Jogo_de_xadrez.tabuleiro
{
    public abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QuantidadeMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca (Cor cor, Tabuleiro tabuleiro)
        {
            Posicao = null;
            Cor = cor;
            Tab = tabuleiro;
            QuantidadeMovimentos = 0;
        }

        public void IncrementarMovimentos()
        {
            QuantidadeMovimentos++;
        }

        public void DecrementarMovimentos()
        {
            QuantidadeMovimentos--;
        }

        public bool ExisteMovimentos()
        {
            bool[,] mat = MovimentosPossiveis();
            for (int i = 0; i < Tab.Linhas; i++)
            {
                for (int j = 0; j < Tab.Colunas; j++)
                {
                    if (mat[i, j]) return true;
                }
            }
            return false;
        }

        public bool PodeMoverPosicao(Posicao pos)
        {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}
