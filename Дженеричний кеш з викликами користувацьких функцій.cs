using System;
using System.Collections.Generic;

public class FunctionCache<TKey, TResult>
{
    private Dictionary<TKey, CacheItem> cache = new Dictionary<TKey, CacheItem>();
    private TimeSpan cacheDuration;

    public FunctionCache(TimeSpan cacheDuration)
    {
        this.cacheDuration = cacheDuration;
    }

    public TResult GetOrAdd(TKey key, Func<TKey, TResult> func)
    {
        if (cache.TryGetValue(key, out var cacheItem) && !cacheItem.HasExpired())
        {
            return cacheItem.Value;
        }

        TResult result = func(key);
        cache[key] = new CacheItem(result);
        return result;
    }

    private class CacheItem
    {
        public TResult Value { get; }
        private DateTime expiration;

        public CacheItem(TResult value)
        {
            Value = value;
            expiration = DateTime.Now.Add(cacheDuration);
        }

        public bool HasExpired()
        {
            return DateTime.Now >= expiration;
        }
    }
}

class Program
{
    static void Main()
    {
        var cache = new FunctionCache<string, int>(TimeSpan.FromSeconds(10));

        Func<string, int> expensiveFunction = key =>
        {
            Console.WriteLine("Calculating result for key: " + key);
            return key.Length;
        };

        Console.WriteLine(cache.GetOrAdd("Hello", expensiveFunction)); // Calculate and cache
        Console.WriteLine(cache.GetOrAdd("World", expensiveFunction)); // Calculate and cache

        // Wait for cache to expire
        System.Threading.Thread.Sleep(11000);

        Console.WriteLine(cache.GetOrAdd("Hello", expensiveFunction)); // Calculate and cache again
    }
}
