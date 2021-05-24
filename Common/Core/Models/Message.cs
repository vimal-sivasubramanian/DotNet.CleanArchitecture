using System.Collections.Generic;

namespace DotNet.EventSourcing.Core.Models
{
    public class Message<TKey, TValue>
    {
        public Dictionary<string, byte[]> Headers { get; set; }

        public TKey Key { get; set; }

        public TValue Value { get; set; }
    }
}
