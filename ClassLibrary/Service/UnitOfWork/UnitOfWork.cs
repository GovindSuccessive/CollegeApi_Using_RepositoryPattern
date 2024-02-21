using ClassLibrary.Context;
using ClassLibrary.Service.AuthService;
using ClassLibrary.Service.CourseService;
using ClassLibrary.Service.StudentService;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary.Service.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly CollegeDbContext _collegeDbContext;
        private IStudentRepository _studentRepository;
        private ICourseRepository _courseRepository;
        private IAuthRepository _authRepository;

        public UnitOfWork(CollegeDbContext collegeDbContext)
        {
            _collegeDbContext = collegeDbContext;
        }

  /*      public IStudentRepository Students => _studentRepository ??= new StudentRepository(_collegeDbContext);
        public ICourseRepository Courses => _courseRepository ??= new CourseRepository(_collegeDbContext);*/

        public IStudentRepository StudentRepository => _studentRepository ??= new StudentRepository(_collegeDbContext);

        public ICourseRepository CourseRepository => _courseRepository ??= new CourseRepository(_collegeDbContext);

        public void Dispose()
        {
            _collegeDbContext.Dispose();
        }

        public void SaveChange()
        {
            _collegeDbContext.SaveChanges();
        }
    }
}
