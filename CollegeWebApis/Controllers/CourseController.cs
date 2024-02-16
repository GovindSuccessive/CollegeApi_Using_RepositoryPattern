using ClassLibrary.Context;
using ClassLibrary.Data.Base;
using ClassLibrary.Data.Entities;
using ClassLibrary.Service.CourseService;
using ClassLibrary.Service.StudentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CollegeWebApis.Model.Dto;

namespace CollegeWebApis.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }


        [HttpGet(Name ="GetAllCourse")]
        public async Task<ActionResult<IEnumerable<Course>>> GetAll()
        {
            var result = await _courseRepository.GetAllAsync();
            if (result == null)
            {
                return NotFound("Course Not Found");
            }
            return Ok(result);
        }

        [HttpGet(Name = "GetCourseById")]
        public async Task<ActionResult<Course>> GetById(Guid id)
        {
            if(id==Guid.Empty)
            {
                return NotFound("Id is not Found");
            }
            var result = await _courseRepository.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound("No Course is Persent Correspond to " + id);
            }
            return Ok(result);
        }

        [HttpPost(Name ="AddStudentDto")]
        public async Task<ActionResult>Add(AddCourseDto course)
        {
            if(course == null)
            {
                return NotFound("Course contaning empty fields");
            }
            var newCourse = new Course()
            {
                Id = Guid.NewGuid(),
                Name = course.Name,
                Description = course.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt= DateTime.Now
            };
            await _courseRepository.AddAsync(newCourse);
            return Ok("Successfully Added Course");
        }

        [HttpDelete(Name = "DeleteCourse")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound("Id is Not Found");
            }
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound("There is no course persent against " + id + "course id");
            }

            await _courseRepository.DeleteAsync(course);
            return Ok("Successfully Delete Course");
        }

        [HttpPut(Name ="UpdateCourse")]

        public async Task<ActionResult> Update(UpdateCourseDto course)
        {
            if(ModelState.IsValid)
            {
                var existingCourse = await _courseRepository.GetByIdAsync(course.Id);
                if(existingCourse == null)
                {
                    return NotFound("No course has been found related to this Course Id");
                }
                else
                {
                    existingCourse.Name = course.Name;
                    existingCourse.Description = course.Description; 
                    existingCourse.UpdatedAt = DateTime.Now;
                }

                await _courseRepository.UpdateAsync(existingCourse);
                return Ok("Course Updated Successfully");
                
            }
            return BadRequest("Course Details are Invalid");
        }

    }
}
