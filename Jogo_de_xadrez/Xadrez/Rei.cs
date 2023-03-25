using System;
using Jogo_de_xadrez.tabuleiro;

namespace Jogo_de_xadrez.Xadrez
{
    public class Rei : Peca
    {
        private PartidaXadrez _partida;
        public Rei(Cor cor, Tabuleiro tabuleiro, PartidaXadrez partida) : base(cor, tabuleiro)
        {
            _partida = partida;
        }

        public override string ToString() => "R";

        private bool PodeMover(Posicao pos)
        {
            Peca p = Tab.Peca(pos.Linha, pos.Coluna);
            return p == null || p.Cor != Cor;
        }

        private bool TesteTorreRoque(Posicao pos)
        {
            Peca p = Tab.Peca(pos);
            return (p != null && p is Torre && p.Cor == Cor && p.QuantidadeMovimentos == 0);
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

            // Jogada especial Roque
            if (QuantidadeMovimentos == 0 && !_partida.Xeque)
            {
                // Roque pequeno
                Posicao posTorre1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);

                if (TesteTorreRoque(posTorre1))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);

                    if (Tab.Peca(p1) == null && Tab.Peca(p2) == null) mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                }

                // Roque grande
                Posicao posTorre2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);

                if (TesteTorreRoque(posTorre2))
                {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);

                    if (Tab.Peca(p1) == null && Tab.Peca(p2) == null && Tab.Peca(p3) == null) mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                }
            }

            return mat;
        }
    }
}
