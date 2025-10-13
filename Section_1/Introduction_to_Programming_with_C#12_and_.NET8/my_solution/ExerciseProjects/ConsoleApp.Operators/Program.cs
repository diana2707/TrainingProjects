
int num1 = 0;
int num2 = 0;

Console.WriteLine("Please enter he first number: ");
num1 = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Please enter the second number: ");
num2 = Convert.ToInt32(Console.ReadLine());

// Add numbers
int sum = num1 + num2;

// Multiply numbers
int product = num1 * num2;

// Divide numbers
int quotient = num1 / num2;

// Subtract numbers
int difference = num1 - num2;

// Modulus
int remainder = num1 % num2;

Console.WriteLine($"The sum of {num1} and {num2} is: {sum}");
Console.WriteLine($"The product of {num1} and {num2} is: {product}");
Console.WriteLine($"The quotient of {num1} and {num2} is: {quotient}");
Console.WriteLine($"The difference of {num1} and {num2} is: {difference}");
Console.WriteLine($"The remainder of {num1} divided by {num2} is: {remainder}");