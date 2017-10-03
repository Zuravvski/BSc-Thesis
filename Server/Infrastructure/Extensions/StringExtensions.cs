namespace Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static bool Empty(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static string ReduceFront(this string value, int length, ref string retVal)
        {
            if (value.Length < length)
            {
                retVal = string.Empty;
                return value;
            }

            retVal = value.Substring(0, length);
            return value.Remove(0, length);
        }
    }
}
