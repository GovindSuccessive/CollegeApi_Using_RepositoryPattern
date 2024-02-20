using ClassLibrary.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClassLibrary.Data.Entities
{
    public class Course : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name {get;set;}
        public string Description { get; set; }
        
        public virtual ICollection<Student>? students {  get; set; }
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
    }
}
