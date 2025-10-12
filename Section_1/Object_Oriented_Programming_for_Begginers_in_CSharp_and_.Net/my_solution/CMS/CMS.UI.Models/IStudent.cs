public interface IStudent
{
    public string FirstName { get;  set; }
    public string LastName { get; set; }
    
    public string GetFullName()
    {
        return $"{FirstName} {LastName}";
    }

    public static string WhoAmI()
    {
        return "Student";
    }
}