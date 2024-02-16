using ClassLibrary.Data.Base;
using ClassLibrary.Data.Entities;
using ClassLibrary.Service.StudentService;
using CollegeWebApis.Model.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CollegeWebApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet(Name = "GetAllStudent")]
        public async Task<ActionResult<IEnumerable<Student>>> GetAll()
        {
            var result = await _studentRepository.GetAllAsync();
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
            var result = await _studentRepository.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound("No Studnet is Persent Correspond to " + id);
            }
            return Ok(result);
        }

        [HttpPost(Name = "AddStudentDto")]
        public async Task<ActionResult> Add(AddStudentDto student)
        {
            if (student == null)
            {
                return NotFound("Student contaning empty fields");
            }
            var newStudent = new Student()
            {
                Id = Guid.NewGuid(),
                Name = student.Name,
                Address= student.Address,
                GmailId= student.GmailId,
                PhoneNumber= student.PhoneNumber,
                CourseRefId= student.CourseRefId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            await _studentRepository.AddAsync(newStudent);
            return Ok("Successfully Added Student");
        }

        [HttpDelete(Name = "DeleteStudnet")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound("Id is Not Found");
            }
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound("There is no student persent against " + id + "student id");
            }

            await _studentRepository.DeleteAsync(student);
            return Ok("Successfully Delete Course");
        }

        [HttpPut(Name = "UpdateStudent")]
        public async Task<ActionResult> Update(UpdateStudentDto student)
        {
            if (ModelState.IsValid)
            {
                var existingStudnet = await _studentRepository.GetByIdAsync(student.Id);
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

                await _studentRepository.UpdateAsync(existingStudnet);
                return Ok("Course Updated Successfully");

            }
            return BadRequest("Course Details are Invalid");
        }


    }
}
