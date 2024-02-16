using ClassLibrary.Context;
using ClassLibrary.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Service.CourseService
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CollegeDbContext _collegeDbContext;


        public CourseRepository(CollegeDbContext collegeDbContext)
        {
            _collegeDbContext = collegeDbContext;
        }
        public async Task AddAsync(Course course)
        {
            await _collegeDbContext.Courses.AddAsync(course);
            await _collegeDbContext.SaveChangesAsync(); 
        }

        public async Task  DeleteAsync(Course course)
        {
            _collegeDbContext.Courses.Remove(course);
           await  _collegeDbContext.SaveChangesAsync();
        }

   

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            var courses = await _collegeDbContext.Courses.ToListAsync();
            return courses;
        }

        public async Task<Course> GetByIdAsync(Guid id)
        {
            var course = await _collegeDbContext.Courses.FirstOrDefaultAsync(x => x.Id == id);
            if(course == null)
            {
                return null;
            }
            return course;
        }

        public async Task UpdateAsync(Course course)
        {
             _collegeDbContext.Courses.Update(course);
            await _collegeDbContext.SaveChangesAsync();
        }
    }
}
