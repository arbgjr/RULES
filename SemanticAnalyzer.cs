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

    // Método para visitar expressões aritméticas
    public override object VisitArithmeticExpression([NotNull] RulesLangParser.ArithmeticExpressionContext context)
    {
        var left = Visit(context.term(0));
        for (int i = 1; i <= context.term().Length / 2; i++)
        {
            var right = Visit(context.term(i));
            var op = context.GetChild(i * 2 - 1).GetText();
    
            switch (op)
            {
                case "+":
                    left = (int)left + (int)right;
                    break;
                case "-":
                    left = (int)left - (int)right;
                    break;
                // Adicione casos para '*', '/' conforme necessário
            }
        }
        return left;
    }
    
    // Método para visitar termos (parte da expressão aritmética)
    public override object VisitTerm([NotNull] RulesLangParser.TermContext context)
    {
        // Visita o primeiro fator (obrigatório)
        var result = Visit(context.factor(0));
    
        // Se houver mais fatores, aplica as operações '*', '/'
        for (int i = 1; i < context.factor().Length; i++)
        {
            var right = Visit(context.factor(i));
            var op = context.GetChild((i - 1) * 2 + 1).GetText(); // Obtém o operador
    
            switch (op)
            {
                case "*":
                    result = (int)result * (int)right;
                    break;
                case "/":
                    if ((int)right == 0)
                    {
                        throw new DivideByZeroException("Cannot divide by zero.");
                    }
                    result = (int)result / (int)right;
                    break;
                default:
                    throw new ArgumentException($"Unexpected operator '{op}'.");
            }
        }
    
        return result;
    }

    
    // Método para visitar fatores (parte da expressão aritmética)
    public override object VisitFactor([NotNull] RulesLangParser.FactorContext context)
    {
        // Se o fator é um número inteiro, retorne o valor
        if (context.INT() != null)
        {
            return VisitInt(context.INT());
        }
        // Se o fator é um identificador, retorne o valor
        else if (context.ID() != null)
        {
            return VisitId(context.ID());
        }
        // Se o fator é uma expressão entre parênteses, visite a expressão
        else if (context.expression() != null)
        {
            return Visit(context.expression());
        }
        else
        {
            throw new ArgumentException("Invalid factor.");
        }
    }
    
    // Adicionarei mais métodos de visita para outros tipos de expressões conforme necessário
}
