## DESAFIO TÉNICO HEPTA - OTÁVIO GOMES 2024
## Descrição do Problema
O desafio consiste em criar um código .Net que calcula o Consumo de Energia Elétrica (CEE)que é um dos pontos de 
avaliação de diagnóstico de um submarino.
Para isso, é preciso receber dados de entrada em formato binário, avaliar esses dados de forma que geram duas taxas: 
uma taxa Gama que consistem em considerar o bit mais significante de cada número binário fornecido e uma taxa Epsilon
que consiste em considerar o bit Menos significante desses mesmos números.
Após calcular essas duas taxas, precisamos multiplicá-las entre si para gerar o CEE e por fim converter em formato 
decimal para fornecer ao usuário final

## Tecnologias envolvidas
Utilizamos C#.Net Core SDK7 como linguagem de backend para gerar uma API Restfullpadrao que fornece 
um endpoint /Diagnostico e esse método recebe um texto com todos os números binários separados por vírgula.
Além disso, utilizamos padrão MVC aproveitando um código existente em que já temos os pacotes Nugets instalados, UX/UI 
já definidas, bem como EntitiFrameworkCore, dependências de bancos e dados entre outros, mesmo que não sendo utilizados 
nesse desafio. 
Fizemos dessa forma visando o reaproveitamento do código em projetos futuros e para disponibilizar três formas de testes:
	a. Testes unitários utilizando xUnit.
	b. Uma UX Razor que está dentro de Views/Home em que podemos rodar a aplicação e informar os números binários à 
	   partir de um site gerador de números binários aleatórios, fornecendo uma mensagem de resultado para o usuário 
	   final.
	c. Uma interface Swegger em que disponibiliza o endpoint e possibilita testes.

## Algoritmo
O algoritmo utilizado foi:
-----
Algoritmo Diagnostico
<br/>
1.1. Declare TaxaGama10, TaxaEpsilon10 e ConsumoEnergiaEletrica como decimal 
<br/>
1.2. Dividir vDados em um array vDadosDiagnostico utilizando a vírgula como delimitador
<br/>
1.3. Verificar se há dados no relatório de diagnóstico 
<br/>
2.1. Se vDadosDiagnostico.Length for maior que 0, continuar 
<br/>
2.2. Caso contrário, retornar -1 em formato JSON e encerrar
<br/>
2.3. Inicializar variáveis 
<br/>
3.1. Determinar o comprimento dos bits (sizeDadosDiagnostico) a partir do primeiro elemento de vDadosDiagnostico 
<br/>
3.2. Inicializar os arrays zeros e uns com o tamanho de sizeDadosDiagnostico 
<br/>
3.3. Inicializar as strings TaxaGamma2 e TaxaEpsilon2 como vazias
<br/>
3.4. Calcular a frequência de 0s e 1s em cada posição 
<br/>
4.1. 
Para cada número binário em vDadosDiagnostico: 
<br/>
4.1.1. Para cada bit na posição i do número binário: 
<br/>
4.1.1.1. Se o bit for '0', incrementar zeros[i] 
<br/>
4.1.1.2. Caso contrário, incrementar uns[i] 
<br/>
5.0. Formar os valores das taxas em binário 
<br/>
5.1. Para cada posição i de 0 a sizeDadosDiagnostico - 1: 
<br/>
5.1.1. Se uns[i] for maior que zeros[i]: 
<br/>
5.1.1.1. Acrescentar '1' a TaxaGamma2 e '0' a TaxaEpsilon2 5.1.2. Caso contrário: 
<br/>
5.1.2.1. Acrescentar '0' a TaxaGamma2 e '1' a TaxaEpsilon2
<br/>
6.0. Converter as taxas de binário para decimal 
<br/>
6.1. Converter TaxaGamma2 de binário para decimal e armazenar em TaxaGama10 
<br/>
6.2. Converter TaxaEpsilon2 de binário para decimal e armazenar em TaxaEpsilon10
<br/>
6.3. Calcular o consumo de energia 
<br/>
7.1. Multiplicar TaxaGama10 por TaxaEpsilon10 e armazenar o resultado em ConsumoEnergiaEletrica
<br/>
8.1. Retornar ConsumoEnergiaEletrica em formato JSON
<br/>
9.0. Encerrar
<br/>

## Padrão de Projeto Utilizado
.Net core MVC. Estou habituado a trabalhar com esse padrão e vi que seria o mais indicado para a situação.

## Testes Unitários
Verificar o projeto DesafioTecnicoHeptaTests

## Resolução do Desafio
Verificar o projeto Painel

## Imagens do Projeto
Observações:
<br/>
Conforme instruído no texto do desafio, o campo para informar os dados binários trazem os números binários passados no próprio 
texto do desafio e, adicionamos a possibilidade de buscar novos números binários em um site externo, simulando novos relatórios
de diagnóstico.
<br/>
Normalmente escrevemos manualmente o entendimento dos assuntos que vamos trabalhar e essas anotações são as últimas imagens.
</br>
<img src="https://github.com/otagomes/hepta2024/blob/main/hepta01.png" />
<img src="https://github.com/otagomes/hepta2024/blob/main/hepta02.png" />
<img src="https://github.com/otagomes/hepta2024/blob/main/hepta03.png" />
<img src="https://github.com/otagomes/hepta2024/blob/main/hepta04.png" />
<img src="https://github.com/otagomes/hepta2024/blob/main/hepta05.png" />
<img src="https://github.com/otagomes/hepta2024/blob/main/hepta06.png" />
<img src="https://github.com/otagomes/hepta2024/blob/main/hepta07.png" />
<img src="https://github.com/otagomes/hepta2024/blob/main/hepta08.jpg" />
<img src="https://github.com/otagomes/hepta2024/blob/main/hepta09.jpg" />
