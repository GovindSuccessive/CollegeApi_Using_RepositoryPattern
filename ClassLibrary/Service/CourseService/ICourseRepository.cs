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

        Task<IEnumerable<Course>> GetCoursesPaged(int page, int pageSize);

        Task<IEnumerable<Course>> GetCoursesByPagesNextPrev(bool nextPage, int pageSize,string searchInput,string sortingInput);
    }
}
