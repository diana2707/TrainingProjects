
// Prompt for input
Console.WriteLine("Please enter Student's grade: ");
int grade = Convert.ToInt32(Console.ReadLine()); // variable with global scope

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

// Switch statement
Console.WriteLine();
Console.WriteLine("---------- Switch statements ----------");
Console.WriteLine();

// Accept an integer as the day of the week and print the appropriate day

Console.WriteLine("Please enter a number between 1 and 7 to get the corresponding day of the week: ");
int dayOfWeek = Convert.ToInt32(Console.ReadLine());

switch (dayOfWeek)
{
    case 1:
        Console.WriteLine("Monday");
        break;
    case 2:
        Console.WriteLine("Tuesday");
        break;
    case 3:
        Console.WriteLine("Wednesday");
        break;
    case 4:
        Console.WriteLine("Thursday");
        break;
    case 5:
        Console.WriteLine("Friday");
        break;
    case 6:
        Console.WriteLine("Saturday");
        break;
    case 7:
        Console.WriteLine("Sunday");
        break;
    default:
        Console.WriteLine("Enter a valid day of the week.");
        break;
}

Console.WriteLine();
Console.WriteLine("---------- End Switch statements ----------");
Console.WriteLine();
