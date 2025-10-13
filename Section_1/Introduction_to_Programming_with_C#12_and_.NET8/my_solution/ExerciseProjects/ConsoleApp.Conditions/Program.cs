
// Prompt for input
Console.WriteLine("Please enter Student's grade: ");
int grade = Convert.ToInt32(Console.ReadLine());

// Simple If-else condition
Console.WriteLine();
Console.WriteLine("---------- Simple If-Else Condition ----------");
Console.WriteLine();

// Decide to print pass or fail based on input
if (grade > 50)
{
    Console.WriteLine("You have passed the exam.");
}
else
{
    Console.WriteLine("You have failed the exam.");
}

Console.WriteLine("End of program.");

Console.WriteLine();
Console.WriteLine("---------- End Simple If-Else Condition ----------");
Console.WriteLine();

// Complex If-else if condition
Console.WriteLine();
Console.WriteLine("---------- Complex If-Else if Condition ----------");
Console.WriteLine();

// Decide to print letter based on grade value
if (grade < 0 || grade > 100)
{
    Console.WriteLine("Invalid grade entered.");
}
else if (grade < 50)
{
    Console.WriteLine("You have received an F grade.");
}
else if(grade >= 50 && grade <= 64)
{
    Console.WriteLine("You have received a C- grade.");
}
else if (grade >= 65 && grade <= 74)
{
    Console.WriteLine("You have received a C grade.");
}
else if (grade >= 85 && grade <= 100)
{
    Console.WriteLine("You have received a A grade.");
}

Console.WriteLine();
Console.WriteLine("---------- End COmplex If-Else if Condition ----------");
Console.WriteLine();

// Ternary operator
Console.WriteLine();
Console.WriteLine("---------- Ternary operator ----------");
Console.WriteLine();

string passStatus = grade < 50 ? "Fail" : "Pass";
Console.WriteLine($"You have {passStatus}ed the exam.");

Console.WriteLine();
Console.WriteLine("---------- End Ternary operator ----------");
Console.WriteLine();
