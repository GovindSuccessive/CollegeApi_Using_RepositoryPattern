using ClassLibrary.Context;
using ClassLibrary.Data.Base;
using ClassLibrary.Data.Entities;
using ClassLibrary.Service.CourseService;
using ClassLibrary.Service.StudentService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CollegeWebApis.Model.Dto;
using ClassLibrary.Service.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CollegeWebApis.Controllers
{
    [Authorize]
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet(Name ="GetAllCourse")]
        public async Task<ActionResult<IEnumerable<Course>>> GetAll()
        {
            var result = await _unitOfWork.CourseRepository.GetAllAsync();
            if (result == null)
            {
                return NotFound("Course Not Found");
            }
            return Ok(result);
        }

        [HttpGet(Name = "GetCourseById")]
        public async Task<ActionResult<Course>> GetById(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _unitOfWork.CourseRepository.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound("No Course is Persent Correspond to " + id);
            }
            return Ok(result);
        }

        [HttpPost(Name ="AddCourseDto")]
        public async Task<ActionResult>Add(AddCourseDto course)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newCourse = new Course()
            {
                Id = Guid.NewGuid(),
                Name = course.Name,
                Description = course.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt= DateTime.Now
            };
            await _unitOfWork.CourseRepository.AddAsync(newCourse);
            return Ok("Successfully Added Course");
        }

        [HttpDelete(Name = "DeleteCourse")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var course = await _unitOfWork.CourseRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound("There is no course persent against " + id + "course id");
            }

            await _unitOfWork.CourseRepository.DeleteAsync(course);
            return Ok("Successfully Delete Course");
        }

        [HttpPut(Name ="UpdateCourse")]

        public async Task<ActionResult> Update(UpdateCourseDto course)
        {
            if(ModelState.IsValid)
            {
                var existingCourse = await _unitOfWork.CourseRepository.GetByIdAsync(course.Id);
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

                await _unitOfWork.CourseRepository.UpdateAsync(existingCourse);
                return Ok("Course Updated Successfully");
                
            }
            return BadRequest(ModelState);
        }

        
        [HttpGet(Name = "GetCourseByPages")]

        public  async Task<ActionResult<IEnumerable<Course>>> GetCoursesPaged(int page, int pageSize)
        {
            // Implementation for paginated retrieval
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await   _unitOfWork.CourseRepository.GetCoursesPaged(page, pageSize));
        }

        [HttpGet(Name = "GetCourseByPagesNextPrev")]

        public async Task<ActionResult<IEnumerable<Course>>> GetCourseByPagesNextPrev(bool nextPage, int pageSize, string? searchInput, string? sortingInput)
        {
            // Implementation for paginated retrieval
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(await _unitOfWork.CourseRepository.GetCoursesByPagesNextPrev(nextPage, pageSize, searchInput, sortingInput));
        }

    }
}
