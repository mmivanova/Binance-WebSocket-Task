namespace Binance.Assessment.Console.Models;

public interface ICommand
{
    public string Method { get; }
    public string Symbol { get; }
}