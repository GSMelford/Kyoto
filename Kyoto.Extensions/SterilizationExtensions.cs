using Newtonsoft.Json;

namespace Kyoto.Extensions;

public static class SterilizationExtensions
{
    public static T ToObject<T>(this string value)
    {
        return JsonConvert.DeserializeObject<T>(value)!;
    }
    
    public static string ToJson(this object value)
    {
        return JsonConvert.SerializeObject(value);
    }
}