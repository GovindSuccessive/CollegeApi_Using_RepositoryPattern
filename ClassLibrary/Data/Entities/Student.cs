using ClassLibrary.Data.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClassLibrary.Data.Entities
{
    public class Student:BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; } 

        public string PhoneNumber { get; set; }

        public string GmailId { get; set; }

        public Guid CourseRefId { get; set; }

        public virtual Course Course { get; set; }
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}
    }
}
