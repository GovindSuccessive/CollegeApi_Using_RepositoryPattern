using System.ComponentModel.DataAnnotations;

namespace CollegeWebApis.Model.Dto
{
    public class AddUserDto
    {
        [StringLength(80, ErrorMessage = "Name is more then 80 characters long")]
        public string Name { get; set; } = "";

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(120, ErrorMessage = "Gmail is more then 120 character long")]
        public string Email { get; set; } = "";

        [StringLength(150, ErrorMessage = "Address is more then 150 character long")]
        public string? address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(12, ErrorMessage = "Phone is more then 12 character long")]
        public string? PhoneNo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "Password is more then 30 character long")]
        public string Password { get; set; } = "";
    }
}
