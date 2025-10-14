
// Write a program that takes a user input age and prints it to the sceed
// Display an error message if the input is invalid

//Console.Write("Enter your age: ");
//string? input = Console.ReadLine();

//if (int.TryParse(input, out int result)){
//    Console.WriteLine($"Your age is: {result}");
//}
//else{
//    Console.WriteLine("Invalid input. Please enter a valid age.");
//}

try
{
    Console.Write("Enter your age: ");
    int age = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine($"Your age is: {age}");
}
catch (Exception)
{
    Console.WriteLine("Invalid input. Please enter a valid age.");
    //throw;
}
finally
{
    Console.WriteLine("End");
}


