namespace CMS.UI.Models;

public class Student : Person, IStudent
{
    public string firstName = default;
    public string lastName = string.Empty;

    public int studentId = 100;

    public readonly int MaxEnrolledCourses = 3;

    public static int MaxBooksAllowed = 6;
    public Student()
    {
        Console.WriteLine("Student Object Created by first constructor");
        int TotalCourses = 5;
        MaxEnrolledCourses = TotalCourses;
    }

    public Student(int id, string firstName, string lastName)
        : base(firstName, lastName)
    {
        Console.WriteLine("Student Object Created by second constructor");
        this.studentId = id;
        this.firstName = firstName;
        this.lastName = lastName;
    }

    public List<string> Hobbies { get; set; }

    public int GetId()
    {
        return studentId;
    }

     ~Student()
    {
        // Destructor
    }
}
