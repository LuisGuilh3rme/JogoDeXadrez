using System;
using Jogo_de_xadrez.tabuleiro;

namespace Jogo_de_xadrez.Xadrez
{
    public class Rei : Peca
    {
        public Rei (Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro) { }

        public override string ToString() => "R";

        private bool PodeMover (Posicao pos)
        {
            Peca p = Tab.Peca(pos.Linha, pos.Coluna);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new(0, 0);

            // Acima:
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }


            // Nordeste:
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //  Direita:
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Sudeste:
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Abaixo:
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Sudoeste:
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Esquerda:
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            // Noroeste:
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            return mat;
        }
    }
}
