using System;

namespace Cms.Data.Repository.Repositories
{
    public interface ICmsRepository
    {
        public IEnumerable<Course> GetAllCourses();
        public Task<IEnumerable<Course>> GetAllCoursesAsync();
    }
}
