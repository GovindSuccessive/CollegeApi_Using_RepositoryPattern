using ClassLibrary.Data.Entities;

namespace CollegeWebApis.Model.Dto
{
    public class AddStudentDto
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string GmailId { get; set; }

        public Guid CourseRefId { get; set; }

    }
}
