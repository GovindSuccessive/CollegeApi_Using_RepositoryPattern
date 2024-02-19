using ClassLibrary.Service.CourseService;
using ClassLibrary.Service.StudentService;

namespace ClassLibrary.Service.UnitOfWork
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }
        ICourseRepository CourseRepository { get; }

        void SaveChange();
    }
}
