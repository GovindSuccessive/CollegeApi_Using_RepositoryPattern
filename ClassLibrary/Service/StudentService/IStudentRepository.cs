using ClassLibrary.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ClassLibrary.Service.StudentService
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllAsync(); // Getting All Students
        Task<Student> GetByIdAsync(Guid id);

        Task AddAsync(Student student);

        Task UpdateAsync(Student student);

        Task DeleteAsync(Student student);
       
    }
}
