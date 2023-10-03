namespace Binance.Assessment.Console.Models;

/// <summary>
/// Abstract class with base functionality of the command object
/// </summary>
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

    /// <summary>
    /// Validates that the first argument of the command is with the correct value of '24h' or 'sma'
    /// </summary>
    /// <param name="elements">the user command arguments</param>
    /// <returns>The method name</returns>
    /// <exception cref="ArgumentException"></exception>
    private static string ValidateAndSetMethod(IReadOnlyList<string> elements)
    {
        if (DomainModel.Constants.CommandMethods.Contains(elements[0]))
        {
            throw new ArgumentException($"Wrong method name in the command! Should be one of the following {DomainModel.Constants.CommandMethods}");
        }

        return elements[0];
    }

    /// <summary>
    /// Validates that the second argument of the command is the correctly written symbol name
    /// </summary>
    /// <param name="elements">the user command arguments</param>
    /// <returns>The symbol</returns>
    /// <exception cref="ArgumentException"></exception>
    private static string ValidateAndSetSymbol(IReadOnlyList<string> elements)
    {
        if (DomainModel.Constants.Tickers.Contains(elements[0]))
        {
            throw new ArgumentException($"Wrong symbol in the command! Should be one of the following {DomainModel.Constants.Tickers}");
        }

        return elements[1];
    }
}