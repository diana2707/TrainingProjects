namespace CMS.UI.Models;

public class ElectronicsCourse : Course, ICourse
{
	public new void AddSubject(CourseSubject subject)
	{
		Console.WriteLine("Adding subject in Electronics Course by method hiding");
	}
	
	public override void RemoveSubject(CourseSubject subject)
	{
		Console.WriteLine("Removing subject in Electronics Course by method overriding");
	}
	
}