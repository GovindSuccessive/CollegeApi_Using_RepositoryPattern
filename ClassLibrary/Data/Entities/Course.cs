using ClassLibrary.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary.Data.Entities
{
    public class Course : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name {get;set;}
        public string Description { get; set; }

        public ICollection<Student> students {  get; set; }
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
    }
}
