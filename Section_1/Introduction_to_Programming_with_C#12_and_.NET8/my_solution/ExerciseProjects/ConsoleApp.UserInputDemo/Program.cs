// Declare variables
string name = string.Empty;
int age = 0;
int retirementAge = 65;

// Prompt user for input
Console.Write("Enter your name: ");
name = Console.ReadLine();

Console.Write("Enter your age: ");
age = Convert.ToInt32(Console.ReadLine());

// Process the data
int yearsToRetirement = retirementAge - age;

// Display the results
Console.WriteLine($"Full name: {name}");
Console.WriteLine($"Age: {age}");
Console.WriteLine($"Working years remaining: {yearsToRetirement}");

