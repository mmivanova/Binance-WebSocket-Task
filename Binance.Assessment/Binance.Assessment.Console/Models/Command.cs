namespace Binance.Assessment.Console.Models;

public abstract class Command
{
    public string Method { get; }
    public string Symbol { get; }

    protected Command(string? input)
    {
        var elements = input!.Split(' ');
        if (elements.Length != 2 && elements.Length != 4 && elements.Length != 5)
        {
            throw new ArgumentException("Wrong number of arguments in the command!");
        }

        Method = ValidateAndSetMethod(elements);
        Symbol = ValidateAndSetSymbol(elements);
    }

    private static string ValidateAndSetMethod(IReadOnlyList<string> elements)
    {
        if (DomainModel.Constants.CommandMethods.Contains(elements[0]))
        {
            throw new ArgumentException($"Wrong method name in the command! Should be one of the following {DomainModel.Constants.CommandMethods}");
        }

        return elements[0];
    }

    private static string ValidateAndSetSymbol(IReadOnlyList<string> elements)
    {
        if (DomainModel.Constants.Tickers.Contains(elements[0]))
        {
            throw new ArgumentException($"Wrong symbol in the command! Should be one of the following {DomainModel.Constants.Tickers}");
        }

        return elements[1];
    }
}