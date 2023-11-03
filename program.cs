using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Verifica se o caminho do arquivo de entrada foi fornecido
        if (args.Length != 1)
        {
            Console.WriteLine("Usage: Rules <input_file_path>");
            return;
        }

        // Lê o arquivo de entrada
        var inputFilePath = args[0];
        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"File not found: {inputFilePath}");
            return;
        }

        try
        {
            // Cria um CharStream que lê do arquivo
            var input = CharStreams.fromPath(inputFilePath);

            // Instancia o lexer com o input
            var lexer = new RulesLangLexer(input);

            // Cria um token stream a partir do lexer
            var tokens = new CommonTokenStream(lexer);

            // Instancia o parser com o token stream
            var parser = new RulesLangParser(tokens);

            // Inicia a análise na regra 'ruleset' (ajuste conforme sua gramática)
            IParseTree tree = parser.ruleset();

            // Instancia o visitante semântico
            var visitor = new SemanticAnalyzer();

            // Visita a árvore
            visitor.Visit(tree);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
