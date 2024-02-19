using ClassLibrary.Data.Base;
using ClassLibrary.Data.Entities;
using ClassLibrary.Service.CourseService;
using ClassLibrary.Service.StudentService;
using ClassLibrary.Service.UnitOfWork;
using CollegeWebApis.Model.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeWebApis.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet(Name = "GetAllStudent")]
        public async Task<ActionResult<IEnumerable<Student>>> GetAll()
        {
            var result = await _unitOfWork.StudentRepository.GetAllAsync();
            if (result == null)
            {
                return NotFound("Student Not Found");
            }
            return Ok(result);
        }

        [HttpGet(Name = "GetStudnetById")]
        public async Task<ActionResult<Student>> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound("Id is not Found");
            }
            var result = await _unitOfWork.StudentRepository.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound("No Studnet is Persent Correspond to " + id);
            }
            return Ok(result);
        }

        [HttpPost(Name = "AddStudentDto")]
        public async Task<ActionResult> Add(AddStudentDto student)
        {
            if (!ModelState.IsValid)
            {
                return NotFound("Student contaning empty fields");
            }
            var newStudent = new Student()
            {
                Id = Guid.NewGuid(),
                Name = student.Name,
                Address = student.Address,
                GmailId = student.GmailId,
                PhoneNumber = student.PhoneNumber,
                CourseRefId = student.CourseRefId,
                //Course= _unitOfWork.CourseRepository.GetByIdAsync(student.CourseRefId).Result,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _unitOfWork.StudentRepository.AddAsync(newStudent);
            return Ok("Successfully Added Student");
        }

        [HttpDelete(Name = "DeleteStudnet")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound("Id is Not Found");
            }
            var student = await _unitOfWork.StudentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound("There is no student persent against " + id + "student id");
            }

            await _unitOfWork.StudentRepository.DeleteAsync(student);
            return Ok("Successfully Delete Course");
        }

        [HttpPut(Name = "UpdateStudent")]
        public async Task<ActionResult> Update(UpdateStudentDto student)
        {
            if (ModelState.IsValid)
            {
                var existingStudnet = await _unitOfWork.StudentRepository.GetByIdAsync(student.Id);
                if (existingStudnet == null)
                {
                    return NotFound("No course has been found related to this Course Id");
                }
                else
                {
                    existingStudnet.Name = student.Name;
                    existingStudnet.PhoneNumber = student.PhoneNumber;
                    existingStudnet.Address= student.Address;
                    existingStudnet.GmailId= student.GmailId;
                    existingStudnet.CourseRefId= student.CourseRefId;
                    existingStudnet.UpdatedAt = DateTime.Now;
                }

                await _unitOfWork.StudentRepository.UpdateAsync(existingStudnet);
                return Ok("Course Updated Successfully");

            }
            return BadRequest("Course Details are Invalid");
        }


    }
}
