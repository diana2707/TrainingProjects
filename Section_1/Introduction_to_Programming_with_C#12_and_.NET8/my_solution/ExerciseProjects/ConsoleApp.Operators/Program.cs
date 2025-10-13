
int num1 = 0;
int num2 = 0;

Console.WriteLine("Please enter he first number: ");
num1 = Convert.ToInt32(Console.ReadLine());

Console.WriteLine("Please enter the second number: ");
num2 = Convert.ToInt32(Console.ReadLine());

// ----- Mathematical Operators -----

Console.WriteLine();
Console.WriteLine("---------- Mathematical Operators: ----------");
Console.WriteLine();

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

Console.WriteLine();
Console.WriteLine("---------- End Mathematical Operators: ----------");
Console.WriteLine();

// ----- Logical Operators -----

Console.WriteLine();
Console.WriteLine("---------- Logical Operators: ----------");
Console.WriteLine();

bool isGreaterThan = num1 > num2;
bool isLessThan = num1 < num2;
bool isEqualTo = num1 == num2;
bool isGeaterThenOrEqualTo = num1 >= num2;
bool isLessThanOrEqualTo = num1 <= num2;
bool isNotEqualTo = num1 != num2;

Console.WriteLine($"{num1} is greater than {num2}: {isGreaterThan}");
Console.WriteLine($"{num1} is less than {num2}: {isLessThan}");
Console.WriteLine($"{num1} is equal to {num2}: {isEqualTo}");
Console.WriteLine($"{num1} is greater than or equal to {num2}: {isGeaterThenOrEqualTo}");
Console.WriteLine($"{num1} is less than or equal to {num2}: {isLessThanOrEqualTo}");
Console.WriteLine($"{num1} is not equal to {num2}: {isNotEqualTo}");

Console.WriteLine();
Console.WriteLine("---------- End Logical Operators: ----------");
Console.WriteLine();

// ----- Assignment Operators -----

Console.WriteLine();
Console.WriteLine("---------- Assignment Operators: ----------");
Console.WriteLine();

Console.WriteLine($"Enter a random value: ");
int randomVal = Convert.ToInt32(Console.ReadLine());

num1 += randomVal; // num1 = num1 + 5
Console.WriteLine($"Num 1 after += {randomVal}: {num1}");

num1 -= randomVal; // num1 = num1 - 3
Console.WriteLine($"Num 1 after -= {randomVal}: {num1}");

num1 /= randomVal; // num1 = num1 / 2
Console.WriteLine($"Num 1 after /= {randomVal}: {num1}");

num1 %= randomVal; // num1 = num1 % 2
Console.WriteLine($"Num 1 after %= {randomVal}: {num1}");

num1 *= randomVal; // num1 = num1 * 10
Console.WriteLine($"Num 1 after *= {randomVal}: {num1}");

Console.WriteLine();
Console.WriteLine("---------- End Assignment Operators: ----------");
Console.WriteLine();

