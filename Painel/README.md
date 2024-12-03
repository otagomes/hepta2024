## Descri��o do Problema
O desafio consiste em criar um c�digo .Net que calcula o Consumo de Energia El�trica (CEE)que � um dos pontos de 
avalia��o de diagn�stico de um submarino.
Para isso, � preciso receber dados de entrada em formato bin�rio, avaliar esses dados de forma que geram duas taxas: 
uma taxa Gama que consistem em considerar o bit mais significante de cada n�mero bin�rio fornecido e uma taxa Epsilon
que consiste em considerar o bit Menos significante desses mesmos n�meros.
Ap�s calcular essas duas taxas, precisamos multiplic�-las entre si para gerar o CEE e por fim converter em formato 
decimal para fornecer ao usu�rio final

## Tecnologias envolvidas
Utilizamos C#.Net Core SDK7 como linguagem de backend para gerar uma API Restfullpadrao que fornece 
um endpoint /Diagnostico e esse m�todo recebe um texto com todos os n�meros bin�rios separados por v�rgula.
Al�m disso, utilizamos padr�o MVC aproveitando um c�digo existente em que j� temos os pacotes Nugets instalados, UX/UI 
j� definidas, bem como EntitiFrameworkCore, depend�ncias de bancos e dados entre outros, mesmo que n�o sendo utilizados 
nesse desafio. 
Fizemos dessa forma visando o reaproveitamento do c�digo em projetos futuros e para disponibilizar tr�s formas de testes:
	a. Testes unit�rios utilizando xUnit.
	b. Uma UX Razor que est� dentro de Views/Home em que podemos rodar a aplica��o e informar os n�meros bin�rios � 
	   partir de um site gerador de n�meros bin�rios aleat�rios, fornecendo uma mensagem de resultado para o usu�rio 
	   final.
	c. Uma interface Swegger em que disponibiliza o endpoint e possibilita testes.

## Algoritmo
O algoritmo utilizado foi:
-----
Algoritmo Diagnostico
1.1. Declare TaxaGama10, TaxaEpsilon10 e ConsumoEnergiaEletrica como decimal 
1.2. Dividir vDados em um array vDadosDiagnostico utilizando a v�rgula como delimitador
1.3. Verificar se h� dados no relat�rio de diagn�stico 
2.1. Se vDadosDiagnostico.Length for maior que 0, continuar 
2.2. Caso contr�rio, retornar -1 em formato JSON e encerrar
2.3. Inicializar vari�veis 
3.1. Determinar o comprimento dos bits (sizeDadosDiagnostico) a partir do primeiro elemento de vDadosDiagnostico 
3.2. Inicializar os arrays zeros e uns com o tamanho de sizeDadosDiagnostico 
3.3. Inicializar as strings TaxaGamma2 e TaxaEpsilon2 como vazias
3.4. Calcular a frequ�ncia de 0s e 1s em cada posi��o 
4.1. Para cada n�mero bin�rio em vDadosDiagnostico: 
4.1.1. Para cada bit na posi��o i do n�mero bin�rio: 
4.1.1.1. Se o bit for '0', incrementar zeros[i] 
4.1.1.2. Caso contr�rio, incrementar uns[i] 
5.0. Formar os valores das taxas em bin�rio 
5.1. Para cada posi��o i de 0 a sizeDadosDiagnostico - 1: 
5.1.1. Se uns[i] for maior que zeros[i]: 
5.1.1.1. Acrescentar '1' a TaxaGamma2 e '0' a TaxaEpsilon2 5.1.2. Caso contr�rio: 
5.1.2.1. Acrescentar '0' a TaxaGamma2 e '1' a TaxaEpsilon2
6.0. Converter as taxas de bin�rio para decimal 
6.1. Converter TaxaGamma2 de bin�rio para decimal e armazenar em TaxaGama10 
6.2. Converter TaxaEpsilon2 de bin�rio para decimal e armazenar em TaxaEpsilon10
6.3. Calcular o consumo de energia 
7.1. Multiplicar TaxaGama10 por TaxaEpsilon10 e armazenar o resultado em ConsumoEnergiaEletrica
8.1. Retornar ConsumoEnergiaEletrica em formato JSON
9.0. Encerrar

## Padr�o de Projeto Utilizado
.Net core MVC. Estou habituado a trabalhar com esse padr�o e vi que seria o mais indicado para a situa��o.

## Testes Unit�rios
Verificar o projeto DesafioTecnicoHeptaTests

## Resolu��o do Desafio
Verificar o projeto Painel
