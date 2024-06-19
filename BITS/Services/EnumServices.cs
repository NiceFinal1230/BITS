using System.ComponentModel;
using System.Reflection;

namespace BITS.Services;

public class EnumServices
{
    public static string GetDescription(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

        if (attributes != null && attributes.Length > 0)
        {
            return attributes.First().Description;
        }

        return value.ToString();
    }
}
