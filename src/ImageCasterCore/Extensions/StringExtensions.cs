namespace ImageCasterCore.Extensions
{
    public static class StringExtensions
    {
        public static string Truncate(this string o, int maxChars)
        {
            return (o.Length > maxChars) ? o.Substring(0, maxChars) : o;
        }
    }
}
