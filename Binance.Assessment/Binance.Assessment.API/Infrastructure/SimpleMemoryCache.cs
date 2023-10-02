using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Binance.Assessment.API.Infrastructure;

public class SimpleMemoryCache : MemoryCache
{
    public SimpleMemoryCache(IOptions<MemoryCacheOptions> optionsAccessor) : base(optionsAccessor)
    {
    }

    public SimpleMemoryCache(IOptions<MemoryCacheOptions> optionsAccessor, ILoggerFactory loggerFactory) : base(optionsAccessor, loggerFactory)
    {
    }
}