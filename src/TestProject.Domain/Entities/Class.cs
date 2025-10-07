using System;
using Volo.Abp.Domain.Entities;

namespace TestProject.Entities;

public class Class : Entity<Guid>
{
    public string Name { get; set; }              // Tên lớp (VD: 10A1)
    public string Description { get; set; }       // Mô tả
    public int SchoolYear { get; set; }          // Năm học (VD: 2024)
    public Guid? TeacherId { get; set; }         // Giáo viên chủ nhiệm
    public int MaxStudents { get; set; }         // Sĩ số tối đa

    protected Class() { }

    public Class(Guid id, string name, int schoolYear) : base(id)
    {
        Name = name;
        SchoolYear = schoolYear;
        MaxStudents = 40;
    }
}