// See https://aka.ms/new-console-template for more information

// Different data types
/*
    text - string
    integer - int
    decimal number - double, float, decimal
    logical - bool
 */

string pet = "cat";

Console.WriteLine(pet);
Console.WriteLine($"I have a {pet}."); // string interpolation
Console.WriteLine("My " + pet + " is orange."); // string concatenation
Console.WriteLine("The {0} eats Wiskas.", pet); // composite formatting

int age = 3;
int averageLifespan = 15;
Console.WriteLine($"My {pet} is {age} years old.");
Console.WriteLine($"My {pet} will probably live for another {averageLifespan - age} years.");

bool isHungry = true;
Console.WriteLine($"Is my {pet} hungry? {isHungry}");

