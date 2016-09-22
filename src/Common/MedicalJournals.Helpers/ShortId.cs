using System;

namespace MedicalJournals.Helpers
{
    /// <summary>
    /// Code from here originally
    /// http://kvz.io/blog/2009/06/10/create-short-ids-with-php-like-youtube-or-tinyurl/
    /// </summary>
    public static class ShortId
    {
        public static string Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string Encode(long id)
        {
            string result = string.Empty;
            for (var i = (int)Math.Floor(Math.Log(id) / Math.Log(Alphabet.Length)); i >= 0; i--)
            {
                result += Alphabet.Substring((int)(Math.Floor(id / BcPow(Alphabet.Length, i)) % Alphabet.Length), 1);
            }
            return ReverseString(result);
        }

        public static long Decode(string id)
        {
            var str = ReverseString(id);
            long result = 0;
            int end = str.Length - 1;
            for (int i = 0; i <= end; i++)
            {
                result = result + (long)(Alphabet.IndexOf(str.Substring(i, 1), StringComparison.Ordinal) * BcPow(Alphabet.Length, end - i));
            }
            return result;
        }

        private static double BcPow(double a, double b)
        {
            return Math.Floor(Math.Pow(a, b));
        }

        private static string ReverseString(string s)
        {
            var arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
