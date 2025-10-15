
Console.WriteLine("---------- Arrays ----------");

// Declare fixed size array
int[] grades = new int[5];


// Add values to fixed size array
//grades[0] = 45;
//grades[1] = 25;
//grades[2] = 38;
//grades[3] = 45;
//grades[4] = 54;

for (int i = 0; i < grades.Length; i++)
{
    Console.Write($"Enter grade for student {i + 1}: ");
    grades[i] = Convert.ToInt32(Console.ReadLine());
}

// Print values of fixed size array
Console.WriteLine("Grades:");
for (int i = 0; i < grades.Length; i++)
{
    Console.WriteLine($"Student {i + 1}: {grades[i]}");
}


// Declare variable sized array
string[] names = new string[] { "Alice", "Bob", "Charlie" };

// Add value to variable sized array
for (int i = 0; i < grades.Length; i++)
{
    Console.Write($"Enter student name {i + 1}: ");
    names[i] = Console.ReadLine();
}

// Print values of variable sized array
Console.WriteLine("Names:");
for (int i = 0; i < grades.Length; i++)
{
    Console.WriteLine($"Student {i + 1}: {names[i]}");
}