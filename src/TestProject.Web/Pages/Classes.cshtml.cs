using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Entities;
using Volo.Abp.Domain.Repositories;

namespace TestProject.Web.Pages;

public class ClassesModel : TestProjectPageModel
{
    private readonly IRepository<Class, Guid> _classRepository;

    public ClassesModel(IRepository<Class, Guid> classRepository)
    {
        _classRepository = classRepository;
    }

    public List<ClassDto> Classes { get; set; }

    public async Task OnGetAsync()
    {
        var classes = await _classRepository.GetListAsync();

        Classes = classes.Select(c => new ClassDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            SchoolYear = c.SchoolYear,
            MaxStudents = c.MaxStudents
        }).ToList();
    }
    public async Task<IActionResult> OnPostAsync(Guid id)
    {
        await _classRepository.DeleteAsync(id);
        return RedirectToPage("/Classes");
    }
}

public class ClassDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int SchoolYear { get; set; }
    public int MaxStudents { get; set; }
}