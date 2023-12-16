using System.Globalization;

namespace BlazingPokemon.ExtensionMethods
{
    public static class ExtensionMethods
    {
        public static string ToTitleCase(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str);
        }
    }
}
