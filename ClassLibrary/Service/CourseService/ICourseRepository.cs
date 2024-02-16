using ClassLibrary.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ClassLibrary.Service.CourseService
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync(); // Getting All Students
        Task<Course> GetByIdAsync(Guid id);

        Task AddAsync(Course course);

        Task UpdateAsync(Course course);

        Task DeleteAsync(Course course);
    }
}
