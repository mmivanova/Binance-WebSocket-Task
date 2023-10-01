using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Binance.Assessment.DomainModel;

[JsonConverter(typeof(StringEnumConverter))]
public enum DataIntervalsInMinutes
{
    [EnumMember(Value = "1m")] _1m = 1,
    [EnumMember(Value = "5m")] _5m = 5,
    [EnumMember(Value = "30m")] _30m = 30,
    [EnumMember(Value = "1d")] _1d = 1440,
    [EnumMember(Value = "1w")] _1w = 10080
}