using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CWX_MegaMod.BotMonitor.Helpers
{
    internal static class EnumExtension
    {
        public static string Description<T>(this T src) where T : Enum
        {
            FieldInfo fi = src.GetType().GetField(src.ToString());
            DescriptionAttribute[] attributes = fi.GetCustomAttributes<DescriptionAttribute>(false) as DescriptionAttribute[];
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return "";
        }

        public static bool IsValid<T>(this T src) where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Contains(src);
        }
    }
}