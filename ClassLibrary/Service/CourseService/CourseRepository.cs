using ClassLibrary.Context;
using ClassLibrary.Data.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Service.CourseService
{
    public class CourseRepository : ICourseRepository
    {
        static int currentPage = 0;
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
            var courses = await _collegeDbContext.Courses.Include(x=>x.students).ToListAsync();
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

        public async Task<IEnumerable<Course>> GetCoursesPaged(int page, int pageSize)
        {
            // Implementation for paginated retrieval
            return await _collegeDbContext.Courses.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCoursesByPagesNextPrev(bool nextPage, int pageSize, string searchInput, string sortingInput)
        {
            // You might need to track the current page state
            if (nextPage)
            {
                currentPage++;
            }
            else
            {
                currentPage = Math.Max(1, currentPage - 1);
            }

            var query= await _collegeDbContext.Courses.Skip((currentPage - 1) * pageSize).Take(pageSize).OrderByDescending(x=>x.CreatedAt).ToListAsync();

            if(sortingInput != null)
            {
                switch (sortingInput.ToLower())
                {

                    case "name":
                        {
                            query = query.OrderBy(x => x.Name).ToList();
                            break;
                        }
                    case "update":
                        {
                            query = query.OrderByDescending(x => x.UpdatedAt).ToList();
                            break;
                        }
                    case "created":
                        {
                            query = query.OrderByDescending(x => x.CreatedAt).ToList();
                            break;
                        }
                    default:
                        {
                            query = query.OrderByDescending(x => x.CreatedAt).ToList();
                            break;
                        }
                }

            }
                if(searchInput  != null)
                {
                    query = query.Where(x => x.Name.ToLower().Contains(searchInput.ToLower())).ToList();
                }
           return query;
        }
    }
}
