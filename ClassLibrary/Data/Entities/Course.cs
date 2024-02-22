using ClassLibrary.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClassLibrary.Data.Entities
{
    public class Course : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name should be maximum 50 character long")]
        public string Name {get;set;}

        [Required]
        [StringLength(300, ErrorMessage = "Description should be maximum 300 character long")]
        public string Description { get; set; }
        
        public virtual ICollection<Student>? students {  get; set; }
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
    }
}
