using System;
using Volo.Abp.Domain.Entities;

namespace TestProject.Entities;

public class Student : Entity<Guid>
{
    public string FullName { get; set; }         // Họ tên
    public DateTime DateOfBirth { get; set; }    // Ngày sinh
    public string Email { get; set; }            // Email
    public string PhoneNumber { get; set; }      // Số điện thoại
    public string Address { get; set; }          // Địa chỉ

    protected Student() { }

    public Student(Guid id, string fullName, DateTime dateOfBirth) : base(id)
    {
        FullName = fullName;
        DateOfBirth = dateOfBirth;
    }
}