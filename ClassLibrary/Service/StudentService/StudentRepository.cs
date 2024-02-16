using ClassLibrary.Context;
using ClassLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Service.StudentService
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CollegeDbContext _collegeDbContext;

        public StudentRepository(CollegeDbContext collegeDbContext) {
            _collegeDbContext = collegeDbContext;
        }
        public async Task AddAsync(Student student)
        {
            await _collegeDbContext.Students.AddAsync(student);
            await _collegeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student student)
        {
             _collegeDbContext.Students.Remove(student);
            await _collegeDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var students = await _collegeDbContext.Students.ToListAsync();
            return students;
        }

        public async Task<Student> GetByIdAsync(Guid id)
        {
            var result =await _collegeDbContext.Students.FirstOrDefaultAsync(x=>x.Id == id);
            return result;
        }

        public async Task UpdateAsync(Student student)
        {
           _collegeDbContext.Update(student);
            await _collegeDbContext.SaveChangesAsync();
        }
    }
}
