using Jogo_de_xadrez.tabuleiro;

namespace Jogo_de_xadrez.Xadrez
{
    public class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        public Posicao ConverterPosicao() => new Posicao(8 - Linha, Coluna - 'a');

        public override string ToString() => $"{Coluna}{Linha}";
    }
}
