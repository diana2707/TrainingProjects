namespace Cms.Data.Repository
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public int CourseDuration { get; set; } // Duration in hours
        public COURSE_TYPE CourseType { get; set; }
       
    }

    public enum COURSE_TYPE
    {
        Engineering,
        Medical,
        Management
    }
}
