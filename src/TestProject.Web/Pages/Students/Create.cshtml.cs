using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestProject.Entities;
using Volo.Abp.Domain.Repositories;

namespace TestProject.Web.Pages.Students;

public class CreateModel : TestProjectPageModel
{
    private readonly IRepository<Student, Guid> _studentRepository;

    public CreateModel(IRepository<Student, Guid> studentRepository)
    {
        _studentRepository = studentRepository;
    }

    [BindProperty]
    public CreateStudentInput Input { get; set; }

    public string Message { get; set; }

    public void OnGet()
    {
        Input = new CreateStudentInput
        {
            DateOfBirth = DateTime.Now.AddYears(-15)
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var student = new Student(
            Guid.NewGuid(),
            Input.FullName,
            Input.DateOfBirth
        )
        {
            Email = Input.Email,
            PhoneNumber = Input.PhoneNumber,
            Address = Input.Address
        };

        await _studentRepository.InsertAsync(student);

        return RedirectToPage("/Students");
    }
}

public class CreateStudentInput
{
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}