# RULES: Universal Logic Engine Systems Compiler

Este projeto é dedicado ao desenvolvimento do compilador para o motor de regras "RULES: Universal Logic Engine Systems". O compilador é construído usando ANTLR para análise léxica e sintática, e C# para a implementação do analisador semântico e execução das regras.

## Objetivos

- Desenvolver um compilador eficiente e seguro para a linguagem de regras.
- Implementar a sintaxe e semântica da linguagem de regras, baseada em "DataWindow Expression Functions" do PowerBuilder.
- Garantir a integridade e a segurança na execução das regras e queries.

## Estrutura do Projeto

O projeto é dividido nas seguintes partes principais:

- `RulesLang.g4`: Arquivo de gramática ANTLR que define a sintaxe da linguagem de regras.
- `SemanticAnalyzer.cs`: Classe em C# para análise semântica das regras.
- `Program.cs`: Classe principal em C# que utiliza o analisador para interpretar e executar as regras.
- `Tests.cs`: Arquivo de testes unitários para garantir a corretude do compilador.
- `input.txt`: Arquivo de exemplo com regras para testar o compilador.

## Pré-requisitos

Para compilar e executar o projeto, você precisará:

- [.NET SDK](https://dotnet.microsoft.com/download)
- Ferramenta ANTLR ou plugin ANTLR para sua IDE

## Configuração e Execução

1. Clone o repositório: git clone https://github.com/arbgjr/RULES
2. Navegue até o diretório do projeto: cd RulesCompiler
3. Gere os analisadores com ANTLR a partir do arquivo `RulesLang.g4`.
5. Compile o projeto: dotnet build
6. Execute o programa: dotnet run


## Testes

Para executar os testes unitários:

1. No diretório do projeto, execute: dotnet test
