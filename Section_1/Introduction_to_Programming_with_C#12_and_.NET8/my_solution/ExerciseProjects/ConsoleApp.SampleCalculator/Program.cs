
Console.WriteLine("---------- Welcome to the sample calculator! ----------");


int operation = 0;

while (operation != -1)
{
    try {
        PrintMenu();

        operation = Convert.ToInt32(Console.ReadLine());
      
        if (operation == -1)
        {
            break;
        }

        int num1 = 0;
        int num2 = 0;

        Console.Write("Please enter the first number: ");
        num1 = Convert.ToInt32(Console.ReadLine());

        Console.Write("Please enter the second number: ");
        num2 = Convert.ToInt32(Console.ReadLine());
        
        double result = 0.0;

        switch (operation)
        {
            case 1:
                result = AddNumbers(num1, num2);
                break;
            case 2:
                result = SubtractNumbers(num1, num2);
                break;
            case 3:
                result = MultiplyNumbers(num1, num2);
                break;
            case 4:
                result = DivideNumbers(num1, num2);
                break;
            case 5:
                result = Fibonacci(num1, num2);
                break;
            default:
                throw new Exception("Invalid operation selected.");
        }

        Console.WriteLine($"The result is: {result}");
    }
    catch (DivideByZeroException)
    {
        Console.WriteLine("Error: Division by zero is not allowed.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        
    }
    finally
    {
        Console.WriteLine("Press any key to try again");
        Console.ReadKey();
        Console.Clear();
    }
    
}

int DivideNumbers(int num1, int num2)
{
    return num1 / num2;
}

int MultiplyNumbers(int num1, int num2)
{
    return num1 * num2;
}

int SubtractNumbers(int num1, int num2)
{
    return num1 - num2;
}

int AddNumbers(int num1, int num2)
{
    return num1 + num2;
}

int Fibonacci(int num1, int num2)
{
    var result = 0;
    for (int i = num1; i < num2; i++)
    {
        result += i;
    }

    return result;
}

void PrintMenu()
{
    Console.Clear();
    Console.WriteLine("Please select an operation (-1 to exit):");
    Console.WriteLine("Addition (1)");
    Console.WriteLine("Subtraction (2)");
    Console.WriteLine("Multiplication (3)");
    Console.WriteLine("Division (4)");
    Console.WriteLine("Accumulator (5)");
}