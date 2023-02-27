using System;
namespace Jogo_de_xadrez.tabuleiro
{
    public class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] _pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            _pecas = new Peca[linhas, colunas];
        }

        public Peca Peca(int linha, int coluna)
        {
            return _pecas[linha, coluna];
        }

        public Peca Peca(Posicao pos)
        {
            return _pecas[pos.Linha, pos.Coluna];
        }

        public void ColocarPeca (Peca p, Posicao pos)
        {
            if (ExistePeca(pos)) throw new TabuleiroException("Há peças se sobrepondo");
            _pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }

        public Peca RetirarPeca (Posicao pos)
        {
            Peca aux = Peca(pos);
            if (aux == null) return null;
            aux.Posicao = null;
            _pecas[pos.Linha, pos.Coluna] = null;
            return aux;
        }

        public bool ExistePeca (Posicao pos)
        {
            ValidarPosicao(pos);
            return Peca(pos) != null;
        }

        public bool VerificarPosicao(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linhas) return false;
            if (pos.Coluna < 0 || pos.Coluna >= Colunas) return false;
            return true;
        }

        public void ValidarPosicao (Posicao pos)
        {
            if (!VerificarPosicao(pos))
            {
                throw new TabuleiroException("Há peças com posições inválidas");
            }
        }
    }
}
