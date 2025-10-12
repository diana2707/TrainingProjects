namespace CMS.UI.Models;

public class CSCourse : Course, ICourse
{
    public sealed override void AddSubject(CourseSubject subject)
    {
        Console.WriteLine("Calling CSCourse.AddSubject(CourseSubject)");
    }
}