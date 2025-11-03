using System.Globalization;
using System.Text.RegularExpressions;

namespace ReadingList.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string ToTitleCaseSafe(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            string[] parts = str.Split(' ');
            string[] processedParts = new string[parts.Length];

            for (int i = 0; i < parts.Length; i++)
            {
                processedParts[i] = ProcessPart(parts[i]);
            }

            return string.Join(' ', processedParts);
        }

        private static string ProcessPart(string part)
        {
            string processedPart = string.Empty;

            // Trim spaces and quotes
            processedPart = part.Trim([' ', '\"']);

            // Check if the string is all uppercase + dots (e.g., "J.K.")
            if (Regex.IsMatch(processedPart, @"^[A-Za-z\.]*$"))
            {
                return processedPart;
            }

            // Convert to title Case
            processedPart = processedPart.ToLower();
            processedPart = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(processedPart);

            return processedPart;
        }
    }
}
