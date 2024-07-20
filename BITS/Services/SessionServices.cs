
using Newtonsoft.Json;
namespace BITS.Services;

public static class SessionServices
{
    //Found an issues where parents object and child object can't have the same attribute
    //Other wise, the child object attribute will be overwrite.
    //Issues: Product Quantity missing when passing from cart controller to order controller
    //Solution: Change System.Text.Json library to Newtonsoft.Json
    public static void Set<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }
    public static T? Get<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonConvert.DeserializeObject<T>(value);
    }
}