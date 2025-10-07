using System;
using Volo.Abp.Domain.Entities;

namespace TestProject.Entities;

public class ClassStudent : Entity<Guid>
{
    public Guid ClassId { get; set; }            // Lớp học
    public Guid StudentId { get; set; }          // Học sinh
    public DateTime EnrollmentDate { get; set; } // Ngày vào lớp

    protected ClassStudent() { }

    public ClassStudent(Guid id, Guid classId, Guid studentId) : base(id)
    {
        ClassId = classId;
        StudentId = studentId;
        EnrollmentDate = DateTime.Now;
    }
}