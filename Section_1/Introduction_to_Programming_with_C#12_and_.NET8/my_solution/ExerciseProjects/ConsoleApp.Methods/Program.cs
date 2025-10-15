Console.WriteLine("------------ Methods -------------");

// void methods
void PrintName()
{
    Console.WriteLine("Andreea.");
}

// value returning methods
int GetFiveYearsAgo()
{
    return DateTime.Now.AddYears(- 5).Year;
}

// methods with parameters
void PrintNameWithParams(string name)
{
    Console.WriteLine(name);
}

int GetFiveYearsAgoWithParams(int year)
{
    return DateTime.Now.Year - year;
}

// methods with optional parameters
int GetFutureOrPastYear(int numberOfYers = 0)
{
    var year = DateTime.Now.AddYears(numberOfYers).Year;
    return year;
}


// methods with nullable parameters
void PrintNameWithNullableParams(string? name, int? count)
{
    //if (string.IsNullOrEmpty(name))
    //{
    //    Console.WriteLine("Guest");
    //}

    //if (!count.HasValue)
    //{
    //    count = 1;
    //}

    name ??= "Guest";
    count ??= 1;

    for(int i = 0; i < count; i++)
    {
        Console.WriteLine(name);
    }
}



// Function calls
//PrintName();
//int fiveYearsAgo = GetFiveYearsAgo();
//Console.WriteLine($"Five years ago was {fiveYearsAgo}.");

//Console.WriteLine("Enter your name: ");
//string userName = Console.ReadLine() ?? "Guest";
//PrintNameWithParams(userName);

//Console.WriteLine("Enter a number of years to go back: ");
//string input = Console.ReadLine() ?? "0";
//int years = int.TryParse(input, out int result) ? result : 0;
//int yearsAgo = GetFiveYearsAgoWithParams(years);
//Console.WriteLine($"This was {yearsAgo} years ago.");

//Console.WriteLine("Enter number of years in the future or past: ");
//string input2 = Console.ReadLine() ?? "0";
//int yearsInput = int.TryParse(input2, out int result2) ? result2 : 0;
//var pastYear1 = GetFutureOrPastYear();
//Console.WriteLine($"The year is: {pastYear1}.");

//var pastYear2 = GetFutureOrPastYear(yearsInput);
//Console.WriteLine($"The year is: {pastYear2}.");

PrintNameWithNullableParams(null, null);
PrintNameWithNullableParams("Ana", 3);