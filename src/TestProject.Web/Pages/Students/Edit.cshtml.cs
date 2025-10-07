using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestProject.Entities;
using Volo.Abp.Domain.Repositories;

namespace TestProject.Web.Pages.Students;

public class EditModel : TestProjectPageModel
{
    private readonly IRepository<Student, Guid> _studentRepository;

    public EditModel(IRepository<Student, Guid> studentRepository)
    {
        _studentRepository = studentRepository;
    }

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public EditStudentInput Input { get; set; }

    public async Task OnGetAsync()
    {
        var student = await _studentRepository.GetAsync(Id);

        Input = new EditStudentInput
        {
            FullName = student.FullName,
            DateOfBirth = student.DateOfBirth,
            Email = student.Email,
            PhoneNumber = student.PhoneNumber,
            Address = student.Address
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var student = await _studentRepository.GetAsync(Id);

        student.FullName = Input.FullName;
        student.DateOfBirth = Input.DateOfBirth;
        student.Email = Input.Email;
        student.PhoneNumber = Input.PhoneNumber;
        student.Address = Input.Address;

        await _studentRepository.UpdateAsync(student);

        return RedirectToPage("/Students");
    }
}

public class EditStudentInput
{
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}