using System.Runtime.Serialization;

namespace Binance.Assessment.DomainModel;

public enum DataIntervalsInMilliseconds
{
    [EnumMember(Value = "1m")] _1m = 1,
    [EnumMember(Value = "5m")] _5m = 5,
    [EnumMember(Value = "30m")] _30m = 30,
    [EnumMember(Value = "1d")] _1d = 1440,
    [EnumMember(Value = "1w")] _1w = 10080
}