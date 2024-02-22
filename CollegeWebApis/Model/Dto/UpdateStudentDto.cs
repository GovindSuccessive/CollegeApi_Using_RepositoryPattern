using ClassLibrary.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace CollegeWebApis.Model.Dto
{
    public class UpdateStudentDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name should be maximum 50 character long")]
        public string Name { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "Address should be maximum 200 character long")]
        public string Address { get; set; }

        [StringLength(13, ErrorMessage = "Phone Number should be maximum 50 character long")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "Email should be maximum 80 character long")]
        [DataType(DataType.EmailAddress)]
        public string GmailId { get; set; }

        public Guid CourseRefId { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
