using ClassLibrary.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClassLibrary.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");
            builder.Property(x => x.Id).IsRequired();
            builder.HasOne(x=>x.Course)
                .WithMany(x=>x.students)
                .HasForeignKey(x=>x.CourseRefId)
                .HasPrincipalKey(x=>x.Id)
                .IsRequired();
        }
    }
}
