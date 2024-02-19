using ClassLibrary.Data.Entities;

namespace CollegeWebApis.Model.Dto
{
    public class UpdateStudentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string GmailId { get; set; }

        public Guid CourseRefId { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
