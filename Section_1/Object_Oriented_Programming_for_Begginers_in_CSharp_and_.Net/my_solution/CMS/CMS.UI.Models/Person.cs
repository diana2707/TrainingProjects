namespace CMS.UI.Models;

public class Person
{
    public Person()
    {
        Console.WriteLine("Person Object Created");

    }

    public Person(string firstName, string lastName)
    {
        Console.WriteLine("Calling Person Constructor with parameters");
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public virtual string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }
}