using Antlr4.Runtime.Misc;
using System;

public class SemanticAnalyzer : RulesLangBaseVisitor<object>
{
    // Método para visitar chamadas de função e avaliar as funções Upper, Lower e Length
    public override object VisitFunctionCall([NotNull] RulesLangParser.FunctionCallContext context)
    {
        var functionName = context.FUNCTION().GetText();
        var args = context.expression();

        switch (functionName)
        {
            case "Upper":
                if (args.Length == 1)
                {
                    var arg = Visit(args[0]) as string;
                    return arg?.ToUpper();
                }
                throw new ArgumentException("Upper function requires exactly one string argument.");

            case "Lower":
                if (args.Length == 1)
                {
                    var arg = Visit(args[0]) as string;
                    return arg?.ToLower();
                }
                throw new ArgumentException("Lower function requires exactly one string argument.");

            case "Length":
                if (args.Length == 1)
                {
                    var arg = Visit(args[0]) as string;
                    return arg?.Length;
                }
                throw new ArgumentException("Length function requires exactly one string argument.");

            default:
                throw new ArgumentException($"Unknown function '{functionName}'.");
        }
    }

    // Método para visitar literais de string e retornar o valor da string
    public override object VisitStringLiteral([NotNull] RulesLangParser.StringLiteralContext context)
    {
        var text = context.GetText();
        // Remove as aspas duplas do início e do fim
        return text.Substring(1, text.Length - 2);
    }

    // Método para visitar literais numéricos e retornar o valor numérico
    public override object VisitInt([NotNull] RulesLangParser.IntContext context)
    {
        return int.Parse(context.GetText());
    }

    // Método para visitar identificadores e retornar o valor do identificador
    public override object VisitId([NotNull] RulesLangParser.IdContext context)
    {
        return context.GetText();
    }

    // Adicionarei mais métodos de visita para outros tipos de expressões conforme necessário
}
