using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestProject.Entities;
using Volo.Abp.Domain.Repositories;

namespace TestProject.Web.Pages.Classes;

public class CreateModel : TestProjectPageModel
{
    private readonly IRepository<Class, Guid> _classRepository;

    public CreateModel(IRepository<Class, Guid> classRepository)
    {
        _classRepository = classRepository;
    }

    [BindProperty]
    public CreateClassInput Input { get; set; }

    public string Message { get; set; }

    public void OnGet()
    {
        Input = new CreateClassInput
        {
            SchoolYear = DateTime.Now.Year,
            MaxStudents = 40
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var newClass = new Class(
            Guid.NewGuid(),
            Input.Name,
            Input.SchoolYear
        )
        {
            Description = Input.Description,
            MaxStudents = Input.MaxStudents
        };

        await _classRepository.InsertAsync(newClass);

        return RedirectToPage("/Classes");
    }
}

public class CreateClassInput
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int SchoolYear { get; set; }
    public int MaxStudents { get; set; }
}