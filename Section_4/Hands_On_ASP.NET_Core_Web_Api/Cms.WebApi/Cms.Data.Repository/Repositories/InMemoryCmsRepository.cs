using System;
using static Cms.Data.Repository.Course;

namespace Cms.Data.Repository.Repositories
{
    public class InMemoryCmsRepository : ICmsRepository
    {
        List<Course> courses = null;
        public InMemoryCmsRepository()
        {
            courses = new List<Course>
            {
                new Course()
                {
                    CourseId = 1,
                    CourseName = "Computer Science",
                    CourseDuration = 4,
                    CourseType = COURSE_TYPE.Engineering
                },

                new Course()
                {
                    CourseId = 2,
                    CourseName = "Information Technology",
                    CourseDuration = 4,
                    CourseType = COURSE_TYPE.Engineering
                }
            };
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return courses;
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            return await Task.Run(() => courses.ToList());
        }
    }
}
