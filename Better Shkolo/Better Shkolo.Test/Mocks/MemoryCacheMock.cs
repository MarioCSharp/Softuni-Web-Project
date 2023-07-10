using Microsoft.Extensions.Caching.Memory;

namespace Better_Shkolo.Test.Mocks
{
    public static class MemoryCacheMock
    {
        public static Mock<IMemoryCache> Instance { 
            get
            {
                var mockCache = new Mock<IMemoryCache>();
                var mockCacheEntry = new Mock<ICacheEntry>();

                string? keyPayload = null;
                mockCache
                    .Setup(mc => mc.CreateEntry(It.IsAny<object>()))
                    .Callback((object k) => keyPayload = (string)k)
                    .Returns(mockCacheEntry.Object);

                object? valuePayload = null;
                mockCacheEntry
                    .SetupSet(mce => mce.Value = It.IsAny<object>())
                    .Callback<object>(v => valuePayload = v);

                TimeSpan? expirationPayload = null;
                mockCacheEntry
                    .SetupSet(mce => mce.AbsoluteExpirationRelativeToNow = It.IsAny<TimeSpan?>())
                    .Callback<TimeSpan?>(dto => expirationPayload = dto);

                return mockCache;
            }
        }
    }
}
