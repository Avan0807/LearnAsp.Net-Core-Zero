using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestProject.Entities;
using Volo.Abp.Domain.Repositories;

namespace TestProject.Web.Pages;

public class StudentsModel : TestProjectPageModel
{
    private readonly IRepository<Student, Guid> _studentRepository;

    public StudentsModel(IRepository<Student, Guid> studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public List<StudentListDto> Students { get; set; }

    public async Task OnGetAsync()
    {
        var students = await _studentRepository.GetListAsync();

        Students = students.Select(s => new StudentListDto
        {
            Id = s.Id,
            FullName = s.FullName,
            DateOfBirth = s.DateOfBirth,
            Age = DateTime.Now.Year - s.DateOfBirth.Year,
            Email = s.Email,
            PhoneNumber = s.PhoneNumber,
            Address = s.Address
        }).OrderBy(s => s.FullName).ToList();
    }

    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        await _studentRepository.DeleteAsync(id);
        return RedirectToPage("/Students");
    }
}

public class StudentListDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}