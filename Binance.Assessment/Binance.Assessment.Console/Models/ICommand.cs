namespace Binance.Assessment.Console.Models;

/// <summary>
/// Common interface between the two kinds of commands for easier management and manipulation in the code
/// </summary>
public interface ICommand
{
    public string Method { get; }
    public string Symbol { get; }
}