using System;

namespace CryptoFolio.Extensions
{
    public static class StringExtensions
    {
        public static String ToFirstLetterUpperCase(this String s)
        {
            if (s == null)
                return null;

            if (s.Length > 1)
                return char.ToUpper(s[0]) + s.Substring(1);

            return s.ToUpper();
        }
    }
}
