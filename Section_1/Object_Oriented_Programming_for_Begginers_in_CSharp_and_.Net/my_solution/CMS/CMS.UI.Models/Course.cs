
public class Course : ICourse
{
    public int courseId;
    public string courseName;

    private List<CourseSubject> subjects = [];
    public Course()
    {

    }

    public int TotalDurationInDays { get; set; }

    public List<CourseSubject> Subjects => subjects;

    public virtual void AddSubject(CourseSubject subject)
    {
        subjects.Add(subject);
    }

    public void AddSubject(List<CourseSubject> subjects)
    {
        this.subjects.AddRange(subjects);
    }

    public virtual void RemoveSubject(CourseSubject subject)
    {
        subjects.Remove(subject);
    }
}