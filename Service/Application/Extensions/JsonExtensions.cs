using Newtonsoft.Json;

namespace DotNet.EventSourcing.Service.Application
{
    public static class JsonExtensions
    {
        internal static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
