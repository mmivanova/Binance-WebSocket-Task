namespace Binance.Assessment.DomainModel;

public class SymbolPrice
{
    public int Id { get; set; }
    public int SymbolId { get; set; }
    public DateTime CloseTime { get; set; }
    public float ClosePrice { get; set; }
    public int ConsecutiveCounts { get; set; }
}