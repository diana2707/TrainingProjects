
Console.WriteLine("---------- Lists ----------");

// Declare a list
List<int> grades = new List<int>();
List<string> students = new List<string>();

// Add values to the list
//grades.Add(45);
//Console.WriteLine(grades[0]);

int grade = 0;
int gradeCount = 0;
string name = string.Empty;
int @continue;

do
{
    gradeCount += 1;
    Console.Write("Enter student name: ");
    name = Console.ReadLine();
    students.Add(name);

    Console.Write("Enter grade (or -1 to exit): ");
    grade = Convert.ToInt32(Console.ReadLine());

    Console.WriteLine($"Do you wish to continue? (1 = yes | 2 = no)");
    @continue = Convert.ToInt32(Console.ReadLine());

} while (@continue == 1);

// Print values in list
Console.WriteLine("The grades you have entered are: ");
for (int i = 0; i < gradeCount; i++)
{
    Console.WriteLine($"{students[1]}: {grades[i]}");
}


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
