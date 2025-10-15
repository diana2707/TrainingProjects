
Console.WriteLine("---------- Strings manipulations ----------");

// Initializa with a regular string literal
string s1 = "This is a literal string";
String s2 = "This is the literal string";

// Declare without initialization - possible null exception
string s3;

// Initialize to null
string? s4 = null;

// Initialize an empty string
string s5 = string.Empty;

// Verbatim string literal
string oldPath = "C:\\Users\\Public\\Documents";
string path = @"C:\Users\Public\Documents";

// Use a const string to prevent modification of a string
const string constString = "This is a constant string";

// Escape sequence of characters
string sentence = "She said , \"Hello!\" \r\n Next line";

// Raw string literals (for multiple lines)
string rawString = """
    This is a raw string literal.
    It can span multiple lines.
    It preserves whitespace and special characters like \n and \t.
    """;

// Review concatination and interpolation
s1 = s1 + s2;
s1 += " - Appended text";
string concatString = string.Concat(s1, " - More text");
string interpolatedString = $"{s1} - Interpolated text with value: {42}";
string formattedString = string.Format("{0} - Formatted text with value: {1}", s1, 42);

// String manipulation methods

// Substrings
string subString = s1.Substring(5);
Console.WriteLine($"Substring from index 5: {subString}");

subString = s1.Substring(5, 5);
Console.WriteLine($"Substring from index 5 with length 5: {subString}");

// Null or empty checks
Console.WriteLine($"{nameof(s1)} has a lenght of {s1.Length}");

//s4 is null
if (!string.IsNullOrEmpty(s4))
{
    Console.WriteLine($"{nameof(s4)} has a lenght of {s4.Length}");
}


// Splitting strings
var splitStrings = s1.Split(' ');
for(int i = 0; i < splitStrings.Length; i++)
{
    Console.WriteLine($"Split part {i}: {splitStrings[i]}");
}

// Replace
string replacements = s1.Replace("string", "STRING");

// Convert to string
string salary = 3000.ToString();
int valueSalary = int.Parse(salary);


// Changing formatting
Console.WriteLine($"{nameof(salary)} : {salary:C}");
Console.WriteLine(nameof(salary)+ ": " + valueSalary.ToString("C"));

