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

        private HashSet<Peca> _pecas;
        private HashSet<Peca> _capturadas;

        public PartidaXadrez()
        {
            Tab = new(8, 8);
            Terminada = false;
            Turno = 1;
            JogadorAtual = Cor.Branca;
            _pecas = new HashSet<Peca>();
            _capturadas = new HashSet<Peca>();

            ColocarPecas();
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca pMovimentada = Tab.RetirarPeca(origem);
            pMovimentada.IncrementarMovimentos();
            Peca pCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(pMovimentada, destino);
            if (pCapturada != null) _capturadas.Add(pCapturada);
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutarMovimento(origem, destino);
            Turno++;
            MudarJogador();
        }

        public void ValidarPosicaoOrigem (Posicao pos)
        {
            Peca peca = Tab.Peca(pos);
            if (peca == null) throw new TabuleiroException("Não há peças na posição de origem escolhida");
            if (JogadorAtual != peca.Cor) throw new TabuleiroException("Escolha somente peças suas");
            if (!peca.ExisteMovimentos()) throw new TabuleiroException("A peça de origem escolhida não pode se movimentar");
        }

        public void ValidarPosicaoDestino (Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).PodeMoverPosicao(destino)) throw new TabuleiroException("Movimento inválido");
        }

        public void MudarJogador()
        {
            if (JogadorAtual == Cor.Branca) JogadorAtual = Cor.Preta;
            else JogadorAtual = Cor.Branca;
        }

        public HashSet<Peca> PecasCapturadas (Cor cor)
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

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ConverterPosicao());
            _pecas.Add(peca);
        }
        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('c', 2, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('d', 2, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('e', 2, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('e', 1, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('d', 1, new Rei(Cor.Branca, Tab));

            ColocarNovaPeca('c', 7, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('c', 8, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('d', 7, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('e', 7, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('e', 8, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('d', 8, new Rei(Cor.Preta, Tab));
        }
    }
}
