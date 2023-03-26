using System;
using Jogo_de_xadrez.tabuleiro;

namespace Jogo_de_xadrez.Xadrez
{
    public class Peao : Peca
    {
        private PartidaXadrez _partida;
        public Peao(Cor cor, Tabuleiro tabuleiro, PartidaXadrez partida) : base(cor, tabuleiro)
        {
            _partida = partida;
        }

        public override string ToString() => "P";

        private bool ExisteInimigo(Posicao pos)
        {
            Peca p = Tab.Peca(pos.Linha, pos.Coluna);
            return p != null && p.Cor != Cor;
        }

        private bool Livre(Posicao pos) => Tab.Peca(pos) == null;

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new(0, 0);

            if (Cor == Cor.Branca)
            {
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.VerificarPosicao(pos) && Livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;

                    pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                    if (Tab.VerificarPosicao(pos) && Livre(pos) && QuantidadeMovimentos == 0) mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tab.VerificarPosicao(pos) && ExisteInimigo(pos)) mat[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tab.VerificarPosicao(pos) && ExisteInimigo(pos)) mat[pos.Linha, pos.Coluna] = true;

                // Jogada en passant
                if (Posicao.Linha == 3)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tab.VerificarPosicao(esquerda) && ExisteInimigo(esquerda) && Tab.Peca(esquerda) == _partida.VulneravelEnPassant) 
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;

                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tab.VerificarPosicao(direita) && ExisteInimigo(direita) && Tab.Peca(direita) == _partida.VulneravelEnPassant) 
                        mat[direita.Linha - 1, direita.Coluna] = true;
                }
            }

            else
            {
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.VerificarPosicao(pos) && Livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;


                    pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                    if (Tab.VerificarPosicao(pos) && Livre(pos) && QuantidadeMovimentos == 0) mat[pos.Linha, pos.Coluna] = true;
                }

                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tab.VerificarPosicao(pos) && ExisteInimigo(pos)) mat[pos.Linha, pos.Coluna] = true;

                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tab.VerificarPosicao(pos) && ExisteInimigo(pos)) mat[pos.Linha, pos.Coluna] = true;

                // Jogada en passant
                if (Posicao.Linha == 4)
                {
                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tab.VerificarPosicao(esquerda) && ExisteInimigo(esquerda) && Tab.Peca(esquerda) == _partida.VulneravelEnPassant) 
                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;

                    Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tab.VerificarPosicao(direita) && ExisteInimigo(direita) && Tab.Peca(direita) == _partida.VulneravelEnPassant) 
                        mat[direita.Linha + 1, direita.Coluna] = true;
                }
            }

            return mat;
        }
    }
}
