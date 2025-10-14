
Console.WriteLine("---------- Welcome to the sample calculator! ----------");


int operation = 0;

do
{
    Console.WriteLine("Please select an operation (-1 to exit):");
    Console.WriteLine("Addition (1)");
    Console.WriteLine("Subtraction (2)");
    Console.WriteLine("Multiplication (3)");
    Console.WriteLine("Division (4)");
    Console.WriteLine("Accumulator (5)");

    operation = Convert.ToInt32(Console.ReadLine());
    
    if (operation == -1)
    {
        break;
    }

    Console.Write("Please enter the first number: ");
    int num1 = Convert.ToInt32(Console.ReadLine());

    Console.Write("Please enter the second number: ");
    int num2 = Convert.ToInt32(Console.ReadLine());

    double result = 0.0;

    switch (operation)
    {
        case 1:
            result = num1 + num2;
            break;
        case 2:
            result = num1 - num2;
            break;
        case 3:
            result = num1 * num2;
            break;
        case 4:
            if (num2 != 0)
            {
                result = (double)num1 / num2; // Cast to double for accurate division
            }
            else
            {
                Console.WriteLine("Error: Division by zero is not allowed.");
            }
            break;
        case 5:
            for (int i = num1; i < num2; i++)
            {
                result += i;
            }
            break;
        default:
            Console.WriteLine("Invalid operation selected.");
            break;
    }

    Console.WriteLine($"The result is: {result}");
}
while (operation != -1);

