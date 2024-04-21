using Microsoft.Extensions.Caching.Memory;
using RestaurantOrderAPI.Domain.Entities;
using System.Timers;

namespace RestaurantOrderAPI.Application.Caching
{
    public class Cache<T>
    {
        private static readonly Lazy<Cache<T>> lazy =
            new Lazy<Cache<T>>(() => new Cache<T>());
        private readonly System.Timers.Timer timer;
        public static Cache<T> Instance 
        { get { return lazy.Value; } }
        private Dictionary<Guid, List<T>> dictionary;
        private Cache()
        {
            dictionary = new Dictionary<Guid, List<T>>();
            timer = new System.Timers.Timer(600000);
            timer.Elapsed += ClearCache;
            timer.AutoReset = true;
            timer.Enabled = true;
        }
        public void ClearCache
            (Object source, ElapsedEventArgs e)
        {
            dictionary.Clear();
        }
        public ICollection<Guid> GetKeys()
        {
            return dictionary.Keys;
        }
        public void RemoveKey
            (Guid key)
        {
            dictionary.Remove(key);
        }
        public List<T>? Get(Guid key)
        {
            if (dictionary.ContainsKey(key))
                return dictionary[key];
            else
                return null;
        }
        public void Add(Guid key, T value, Predicate<T> predicate)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.TryAdd(key, new List<T>());
                dictionary[key].Add(value);
            } else
            {
                if (dictionary[key].Find(predicate) == null
                    || dictionary[key].Count == 0)
                {
                    dictionary[key].Add(value);
                } else
                {
                    var index = dictionary[key].FindIndex
                        (v => v.Equals(value));
                    dictionary[key][index] = value;
                }
            }
        }
        public bool RemoveValue(Guid key, T? value)
        {
            if (dictionary.ContainsKey(key))
            {
                var remove = dictionary[key].FirstOrDefault
                    (v => v.Equals(value));
                return dictionary[key].Remove(remove);
            } else
            {
                return false;
            }       
        }
    }
}
