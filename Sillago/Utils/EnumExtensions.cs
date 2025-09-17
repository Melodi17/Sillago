namespace Sillago.Utils
{
    using System;

    public static class EnumExtensions
    {
        public static bool Is<TEnum>(this TEnum value, TEnum flag)
            where TEnum : Enum
        {
            return (Convert.ToUInt64(value) & Convert.ToUInt64(flag)) == Convert.ToUInt64(flag);
        }
    }
}