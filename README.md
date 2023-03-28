# Jogo de xadrez

O clássico Jogo de xadrez na versão de linhas de comando.

## Requisitos

O programa necessita do .NET 6.0 pra funcionar. [Baixe aqui](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

Use os comandos abaixo no prompt de comandos para rodar a aplicação:

```sh
dotnet run
```

## Guia de Jogo

Todos os jogadores possuem 16 peças: 1 Rei, 1 Rainha, 2 Bispos, 2 Cavalos, 2 torres e 8 peões. O jogador inicial é sempre o de peças brancas.

![Tabuleiro de xadrez](https://user-images.githubusercontent.com/89887370/228103393-dab27cb7-17a5-45ef-8ddd-d38469c00cf8.PNG)


Cada peça possuí seu próprio estilo de movimentação: 

- O rei pode se mover uma casa em qualquer direção;

- Os bispos podem se mover em qualquer casa na diagonal;

- As torres se movem em qualquer direção em linha reta;

- Os peões somente podem se mover uma casa para frente, exceto na primeira jogada, onde podem mover duas casas a frente, e para capturar peças adversárias, que devem ser feitas nas diagonais a frente da peça;

- A rainha é a peça mais forte, podendo se mover para qualquer casa na diagonal ou em linha reta.

O objetivo do xadrez é realizar xeque-mate ao rei adversário, para isso, o rei inimigo deve estar em uma posição para ser capturado e não haver nenhuma maneira de remove-lo dessa posição.

## Jogadas especiais

Existem algumas jogadas que podem ser realizadas durante o jogo:

- Na primeira jogada de qualquer peão, ele pode se movimentar duas casas em vez de somente uma.

- Quando o peão está em sua primeira jogada, e realiza duas movimentações para frente, se houver um peão adversário ao seu lado, ele pode captura-lo fazendo uma jogada na diagonal no quadrado anterior em que o peão está posicionado.

- A jogada roque pequeno consiste na troca de posições entre o rei e a torre ao seu lado, o rei anda duas posições a direita, e a torre duas posições a esquerda, só pode ocorrer se as posições para troca estiverem livres.

- A jogada roque gradne consiste na troca de posições entre o rei e a torre ao lado da dama, o rei anda duas posições a esquerda, e a torre três posições a direita, só pode ocorrer se as posições para troca estiverem livres.
