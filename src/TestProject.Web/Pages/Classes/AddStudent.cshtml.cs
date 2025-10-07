using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestProject.Entities;
using Volo.Abp.Domain.Repositories;

namespace TestProject.Web.Pages.Classes;

public class AddStudentModel : TestProjectPageModel
{
    private readonly IRepository<Class, Guid> _classRepository;
    private readonly IRepository<Student, Guid> _studentRepository;
    private readonly IRepository<ClassStudent, Guid> _classStudentRepository;

    public AddStudentModel(
        IRepository<Class, Guid> classRepository,
        IRepository<Student, Guid> studentRepository,
        IRepository<ClassStudent, Guid> classStudentRepository)
    {
        _classRepository = classRepository;
        _studentRepository = studentRepository;
        _classStudentRepository = classStudentRepository;
    }

    [BindProperty(SupportsGet = true)]
    public Guid ClassId { get; set; }

    [BindProperty]
    public Guid StudentId { get; set; }

    public string ClassName { get; set; }
    public List<StudentDto> AvailableStudents { get; set; }
    public string Message { get; set; }

    public async Task OnGetAsync()
    {
        var classEntity = await _classRepository.GetAsync(ClassId);
        ClassName = classEntity.Name;

        var allStudents = await _studentRepository.GetListAsync();
        var classStudents = await _classStudentRepository.GetListAsync();

        var studentsInClass = classStudents
            .Where(cs => cs.ClassId == ClassId)
            .Select(cs => cs.StudentId)
            .ToList();

        AvailableStudents = allStudents
            .Where(s => !studentsInClass.Contains(s.Id))
            .Select(s => new StudentDto
            {
                Id = s.Id,
                FullName = s.FullName,
                Email = s.Email
            })
            .ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var newClassStudent = new ClassStudent(
            Guid.NewGuid(),
            ClassId,
            StudentId
        );

        await _classStudentRepository.InsertAsync(newClassStudent);

        return RedirectToPage("/Classes/Students", new { classId = ClassId });
    }
}

public class StudentDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
}