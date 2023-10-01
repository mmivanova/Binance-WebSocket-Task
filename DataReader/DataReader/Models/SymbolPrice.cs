namespace DataReader.Models;

public class SymbolPrice
{
    public int Id { get; set; }
    public int SymbolId { get; set; }
    public long Time { get; set; }
    public float Price { get; set; }
    public int ConsecutiveCounts { get; set; }

    public SymbolPrice(RawPriceObject rawPrice)
    {
        SymbolId = (int)rawPrice.Symbol;
        Price = rawPrice.Kline.ClosePrice;
        Time = rawPrice.Kline.CloseTimeInMillisecondsEpoch;
        ConsecutiveCounts = 1;
    }
}