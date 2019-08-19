using System;
using System.Globalization;

namespace SalesAnalisys.Utilities
{
    public static class StringExtensions
    {
        public static int ToInt(this string number) => int.Parse(number);
        public static float ToFloat(this string number) => float.Parse(number, new CultureInfo("en-US"));


    }
}
