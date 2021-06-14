using Newtonsoft.Json;

namespace DotNet.CleanArchitecture.Core
{
    public static class JsonExtensions
    {
        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public static T To<T>(this string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
