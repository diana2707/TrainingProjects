// Declare variables
using System.Globalization;

string? firstName = string.Empty;
string? lastName = string.Empty;
int age = 0;
DateOnly birthDate = new DateOnly();
const int retirementAge = 65;
decimal salary = 0.0m;
char gender = char.MinValue;
bool working = false;

// Prompt user for input
Console.Write("Enter your first name: ");
firstName = Console.ReadLine();

Console.Write("Enter your last name: ");
lastName = Console.ReadLine();

Console.Write("Enter your date of birth (dd/mm/yyyy): ");
birthDate = DateOnly.ParseExact(Console.ReadLine(), "dd/mm/yyyy", CultureInfo.InvariantCulture);
age = DateTime.Now.Year - birthDate.Year;

Console.Write("Enter your salary: ");
salary = Convert.ToDecimal(Console.ReadLine()); // convert to decimal

Console.Write("Enter your gender (M of F): ");
gender = Convert.ToChar(Console.ReadLine());

Console.Write("Enter your working status (true or false): ");
working = Convert.ToBoolean(Console.ReadLine());

// Process the data
int yearsToRetirement = retirementAge - age;
var estimatedRetirementDate = DateTime.Now.AddYears(yearsToRetirement);

// Display the results
Console.WriteLine($"Full name: {firstName} {lastName}");
Console.WriteLine($"Age: {age}");
Console.WriteLine($"Your salary: {salary}");
Console.WriteLine($"Your gender: {gender}");
Console.WriteLine($"Your working status: {working}");
Console.WriteLine($"Working years remaining: {yearsToRetirement}");
Console.WriteLine($"Estimated retirement date: {estimatedRetirementDate.Year}");

