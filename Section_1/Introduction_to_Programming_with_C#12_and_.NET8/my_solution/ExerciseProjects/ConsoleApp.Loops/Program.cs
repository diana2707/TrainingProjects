
// Simple for loop
Console.WriteLine();
Console.WriteLine("---------- Simple For Loop ----------");
Console.WriteLine();

// Print Hello World 10 times
for (int i = 0; i < 10; i++)
{
    Console.WriteLine($"Hello world! - {i}");
}

Console.WriteLine("End of loop.");


// Ask user how many times to print Hello World and print it
//Console.Write("How many times do you want to print Hello World? ");
//int times = Convert.ToInt32(Console.ReadLine());
//for (int i = 0; i < times; i++)
//{
//    Console.WriteLine($"Hello world! - {i}");
//}

Console.WriteLine();
Console.WriteLine("---------- Simple For Loop End ----------");
Console.WriteLine();

// While loop
Console.WriteLine();
Console.WriteLine("---------- While Loop ----------");
Console.WriteLine();

int counter = 0;
while (counter < 10)
{
    Console.WriteLine($"Hello world! - {counter}");
    counter += 2;
}

// Ask user for a number and find the total for each number that is entered
// Print final sum when the user enters -1 to exit

Console.WriteLine();
Console.WriteLine("Enter numbers to sum them up. Enter -1 to exit.");
int num = 0;
int sum = 0;

while (num != -1)
{
    num = Convert.ToInt32(Console.ReadLine());
    sum = num == -1 ? sum : sum + num;
}

Console.WriteLine($"The total sum is: {sum}");

Console.WriteLine();
Console.WriteLine("---------- While  Loop End ----------");
Console.WriteLine();


