using Microsoft.AspNetCore.Mvc;
using Cms.Data.Repository.Repositories;
using Cms.Data.Repository;
using Cms.WebApi.DTOs;

namespace Cms.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICmsRepository _cmsRepository;
        public CoursesController(ICmsRepository cmsRepository)
        {
            _cmsRepository = cmsRepository;
        }

        //[HttpGet]
        //public IEnumerable<Course> GetCourses()
        //{
        //    return _cmsRepository.GetAllCourses();
        //}

        //[HttpGet]
        //public IEnumerable<CourseDto> GetCourses()
        //{
        //    try
        //    {
        //        var courses = _cmsRepository.GetAllCourses();
        //        return courses.Select(MapCourseToCourseDto);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception (not shown here for brevity)
        //        // Return an appropriate error response
        //        throw new Exception("An error occurred while retrieving courses.", ex);
        //    }

        //}

        //[HttpGet]
        //public IActionResult GetCourses()
        //{
        //    try
        //    {
        //        var courses = _cmsRepository.GetAllCourses();
        //        var result = courses.Select(MapCourseToCourseDto);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }

        //}

        //[HttpGet]
        //public ActionResult<IEnumerable<CourseDto>> GetCourses()
        //{
        //    try
        //    {
        //        var courses = _cmsRepository.GetAllCourses();
        //        var result = courses.Select(MapCourseToCourseDto);
        //        return result.ToList(); // Implicitly returns 200 OK with the list; convert to suport ActionResult<T>
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }

        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesAsync()
        {
            try
            {
                var courses = await _cmsRepository.GetAllCoursesAsync();
                var result = courses.Select(MapCourseToCourseDto);
                return result.ToList(); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        // Custom mapper functions
        private CourseDto MapCourseToCourseDto(Course course)
        {
            return new CourseDto
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                CourseDuration = course.CourseDuration,
                CourseType = course.CourseType
            };
        }
    }
}
