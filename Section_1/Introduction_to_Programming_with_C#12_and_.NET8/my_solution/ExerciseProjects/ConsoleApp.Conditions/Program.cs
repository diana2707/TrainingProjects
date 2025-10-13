
// Prompt for input
Console.WriteLine("Please enter Student's grade: ");
int grade = Convert.ToInt32(Console.ReadLine());

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
