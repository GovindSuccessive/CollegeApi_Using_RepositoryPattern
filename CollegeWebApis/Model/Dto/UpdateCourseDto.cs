using System.ComponentModel.DataAnnotations;

namespace CollegeWebApis.Model.Dto
{
    public class UpdateCourseDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(80, ErrorMessage = "Maximum Length should be 80 character long")]
        public string Name { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "Maximum Length should be 300 character long")]
        public string Description { get; set; }

    }
}
