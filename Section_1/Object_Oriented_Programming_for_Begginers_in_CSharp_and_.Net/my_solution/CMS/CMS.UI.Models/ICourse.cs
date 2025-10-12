public interface ICourse
{
    public static int DefaultElectives = 8;

    public int TotalDurationInDays { get; set; }
    public List<CourseSubject> Subjects { get; }
    public int TotalSubjects => Subjects.Count;
    

    public void AddSubject(CourseSubject subject);
    public void AddSubject(List<CourseSubject> subjects);
    public void RemoveSubject(CourseSubject subject);

    public int GetTotalElectives()
    {
        return 0;
    }

    public static void ShowDetails()
    {
        Console.WriteLine("Course details");
    }
    
}