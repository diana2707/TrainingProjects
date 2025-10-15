
Console.WriteLine("---------- Lists ----------");

// Declare a list
List<int> grades = new List<int>();

// Add values to the list
//grades.Add(45);
//Console.WriteLine(grades[0]);

int grade = 0;

do
{
    Console.Write("Enter grade (or -1 to exit): ");
    grade = Convert.ToInt32(Console.ReadLine());
    if (grade != -1)
    {
        grades.Add(grade);
    }
} while (grade != -1);



// Print values of the list - for
Console.WriteLine("Printing grades via for loop");
for (int i = 0; i < grades.Count; i++)
{
    Console.WriteLine($"Grade {i + 1}: {grades[i]}");
}


// Print values in list - foreach
Console.WriteLine("Printing grades via foreach loop");
int count = 1;
foreach (var g in grades)
{
    Console.WriteLine($"Grade {count}: {g}");
    count++;
}
