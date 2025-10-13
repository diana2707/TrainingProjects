
Console.WriteLine("---------- Welcome to the sample calculator! ----------");

Console.Write("Please enter the first number: ");
int num1 = Convert.ToInt32(Console.ReadLine());

Console.Write("Please enter the second number: ");
int num2 = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Please select an operation:");
Console.WriteLine("Addition (A)");
Console.WriteLine("Subtraction (S)");
Console.WriteLine("Multiplication (M)");
Console.WriteLine("Division (D)");
char operation = Convert.ToChar(Console.ReadLine().ToUpper());

double result = 0.0;

switch (operation)
{
       case 'A':
        result = num1 + num2;
        break;
    case 'S':
        result = num1 - num2;
        break;
    case 'M':
        result = num1 * num2;
        break;
    case 'D':
        if (num2 != 0)
        {
            result = (double)num1 / num2; // Cast to double for accurate division
        }
        else
        {
            Console.WriteLine("Error: Division by zero is not allowed.");
        }
        break;
    default:
        Console.WriteLine("Invalid operation selected.");
        break;
}

Console.WriteLine($"The result is: {result}");
