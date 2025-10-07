using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestProject.Entities;
using Volo.Abp.Domain.Repositories;

namespace TestProject.Web.Pages.Classes;

public class EditModel : TestProjectPageModel
{
    private readonly IRepository<Class, Guid> _classRepository;

    public EditModel(IRepository<Class, Guid> classRepository)
    {
        _classRepository = classRepository;
    }

    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public EditClassInput Input { get; set; }

    public async Task OnGetAsync()
    {
        var classEntity = await _classRepository.GetAsync(Id);

        Input = new EditClassInput
        {
            Name = classEntity.Name,
            Description = classEntity.Description,
            SchoolYear = classEntity.SchoolYear,
            MaxStudents = classEntity.MaxStudents
        };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var classEntity = await _classRepository.GetAsync(Id);

        classEntity.Name = Input.Name;
        classEntity.Description = Input.Description;
        classEntity.SchoolYear = Input.SchoolYear;
        classEntity.MaxStudents = Input.MaxStudents;

        await _classRepository.UpdateAsync(classEntity);

        return RedirectToPage("/Classes");
    }
}

public class EditClassInput
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int SchoolYear { get; set; }
    public int MaxStudents { get; set; }
}