using System.ComponentModel.DataAnnotations;

namespace CollegeWebApis.Model.RequestModel
{
    public class LoginRequest
    {
        [Required]
        [StringLength(120, ErrorMessage = "Length of Email ID more then 120 character long")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(30, ErrorMessage = "Password is more then 30 character long")]
        public string Password { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        [Compare("Password",ErrorMessage ="Password MissMatch")]
        public string ConfirmPassword { get; set; }
    }
}
