using System;
using Jogo_de_xadrez.tabuleiro;

namespace Jogo_de_xadrez.Xadrez
{
    public class Cavalo : Peca
    {
        public Cavalo(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro) { }

        public override string ToString() => "C";

        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.Peca(pos.Linha, pos.Coluna);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new(0, 0);

            // Posições acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos)) mat[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos)) mat[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos)) mat[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos)) mat[pos.Linha, pos.Coluna] = true;

            // Posições abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos)) mat[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos)) mat[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos)) mat[pos.Linha, pos.Coluna] = true;

            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tab.VerificarPosicao(pos) && PodeMover(pos)) mat[pos.Linha, pos.Coluna] = true;

            return mat;
        }
    }
}
