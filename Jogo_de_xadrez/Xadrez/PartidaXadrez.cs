using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Jogo_de_xadrez.tabuleiro;

namespace Jogo_de_xadrez.Xadrez
{
    public class PartidaXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        public bool Xeque { get; private set; }

        private HashSet<Peca> _pecas;
        private HashSet<Peca> _capturadas;


        public PartidaXadrez()
        {
            Tab = new(8, 8);
            Terminada = false;
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Xeque = false;
            _pecas = new HashSet<Peca>();
            _capturadas = new HashSet<Peca>();

            ColocarPecas();
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca pMovimentada = Tab.RetirarPeca(origem);
            pMovimentada.IncrementarMovimentos();
            Peca pCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(pMovimentada, destino);
            if (pCapturada != null) _capturadas.Add(pCapturada);

            // Jogada especial Roque pequeno
            if (pMovimentada is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = Tab.RetirarPeca(origemTorre);
                torre.IncrementarMovimentos();
                Tab.ColocarPeca(torre, destinoTorre);
            }

            // Jogada especial Roque grande
            if (pMovimentada is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = Tab.RetirarPeca(origemTorre);
                torre.IncrementarMovimentos();
                Tab.ColocarPeca(torre, destinoTorre);
            }

            return pCapturada;
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pCapturada = ExecutarMovimento(origem, destino);

            if (ExisteXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pCapturada);
                throw new TabuleiroException("Você se colocou em xeque");
            }
            if (ExisteXeque(Adversaria(JogadorAtual))) Xeque = true;
            else Xeque = false;
            if (ExisteXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
                return;
            }
            Turno++;
            MudarJogador();
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca capturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarMovimentos();

            if (capturada != null)
            {
                Tab.ColocarPeca(capturada, destino);
                _capturadas.Remove(capturada);
            }

            Tab.ColocarPeca(p, origem);

            // Jogada especial Roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca torre = Tab.RetirarPeca(destinoTorre);
                torre.DecrementarMovimentos();
                Tab.ColocarPeca(torre, origemTorre);
            }

            // Jogada especial Roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca torre = Tab.RetirarPeca(destinoTorre);
                torre.DecrementarMovimentos();
                Tab.ColocarPeca(torre, origemTorre);
            }
        }

        public void ValidarPosicaoOrigem(Posicao pos)
        {
            Peca peca = Tab.Peca(pos);
            if (peca == null) throw new TabuleiroException("Não há peças na posição de origem escolhida");
            if (JogadorAtual != peca.Cor) throw new TabuleiroException("Escolha somente peças suas");
            if (!peca.ExisteMovimentos()) throw new TabuleiroException("A peça de origem escolhida não pode se movimentar");
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).PodeMoverPosicao(destino)) throw new TabuleiroException("Movimento inválido");
        }

        public void MudarJogador()
        {
            if (JogadorAtual == Cor.Branca) JogadorAtual = Cor.Preta;
            else JogadorAtual = Cor.Branca;
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca peca in _capturadas)
            {
                if (peca.Cor == cor) aux.Add(peca);
            }

            return aux;
        }

        public HashSet<Peca> PecasExistentes(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca peca in _pecas)
            {
                if (peca.Cor == cor) aux.Add(peca);
            }

            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca) return Cor.Preta;
            return Cor.Branca;
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca peca in PecasExistentes(cor))
            {
                if (peca is Rei) return peca;
            }
            return null;
        }

        public bool ExisteXeque(Cor cor)
        {
            Peca r = Rei(cor);
            if (r == null) throw new TabuleiroException("Sem rei desta cor no tabuleiro");

            foreach (Peca peca in PecasExistentes(Adversaria(cor)))
            {
                bool[,] mat = peca.MovimentosPossiveis();
                if (mat[r.Posicao.Linha, r.Posicao.Coluna]) return true;
            }
            return false;
        }

        public bool ExisteXequeMate(Cor cor)
        {
            if (!ExisteXeque(cor)) return false;

            foreach (Peca peca in PecasExistentes(cor))
            {
                bool[,] mat = peca.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = peca.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pCapturada = ExecutarMovimento(origem, destino);
                            bool existeXeque = ExisteXeque(cor);
                            DesfazMovimento(origem, destino, pCapturada);
                            if (!existeXeque) return false;
                        }
                    }
                }
            }
            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ConverterPosicao());
            _pecas.Add(peca);
        }
        private void ColocarPecas()
        {
            // Peças brancas
            ColocarNovaPeca('a', 1, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('b', 1, new Cavalo(Cor.Branca, Tab));
            ColocarNovaPeca('c', 1, new Bispo(Cor.Branca, Tab));
            ColocarNovaPeca('d', 1, new Dama(Cor.Branca, Tab));
            ColocarNovaPeca('e', 1, new Rei(Cor.Branca, Tab, this));
            ColocarNovaPeca('f', 1, new Bispo(Cor.Branca, Tab));
            ColocarNovaPeca('g', 1, new Cavalo(Cor.Branca, Tab));
            ColocarNovaPeca('h', 1, new Torre(Cor.Branca, Tab));

            ColocarNovaPeca('a', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('b', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('c', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('d', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('e', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('f', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('g', 2, new Peao(Cor.Branca, Tab));
            ColocarNovaPeca('h', 2, new Peao(Cor.Branca, Tab));

            // Peças pretas
            ColocarNovaPeca('a', 8, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('b', 8, new Cavalo(Cor.Preta, Tab));
            ColocarNovaPeca('c', 8, new Bispo(Cor.Preta, Tab));
            ColocarNovaPeca('d', 8, new Dama(Cor.Preta, Tab));
            ColocarNovaPeca('e', 8, new Rei(Cor.Preta, Tab, this));
            ColocarNovaPeca('f', 8, new Bispo(Cor.Preta, Tab));
            ColocarNovaPeca('g', 8, new Cavalo(Cor.Preta, Tab));
            ColocarNovaPeca('h', 8, new Torre(Cor.Preta, Tab));

            ColocarNovaPeca('a', 7, new Peao(Cor.Preta, Tab));
            ColocarNovaPeca('b', 7, new Peao(Cor.Preta, Tab));
            ColocarNovaPeca('c', 7, new Peao(Cor.Preta, Tab));
            ColocarNovaPeca('d', 7, new Peao(Cor.Preta, Tab));
            ColocarNovaPeca('e', 7, new Peao(Cor.Preta, Tab));
            ColocarNovaPeca('f', 7, new Peao(Cor.Preta, Tab));
            ColocarNovaPeca('g', 7, new Peao(Cor.Preta, Tab));
            ColocarNovaPeca('h', 7, new Peao(Cor.Preta, Tab));
        }
    }
}
