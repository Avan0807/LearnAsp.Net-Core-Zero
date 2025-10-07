using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestProject.Entities;
using Volo.Abp.Domain.Repositories;

namespace TestProject.Web.Pages.Classes;

public class StudentsModel : TestProjectPageModel
{
    private readonly IRepository<Class, Guid> _classRepository;
    private readonly IRepository<ClassStudent, Guid> _classStudentRepository;
    private readonly IRepository<Student, Guid> _studentRepository;

    public StudentsModel(
        IRepository<Class, Guid> classRepository,
        IRepository<ClassStudent, Guid> classStudentRepository,
        IRepository<Student, Guid> studentRepository)
    {
        _classRepository = classRepository;
        _classStudentRepository = classStudentRepository;
        _studentRepository = studentRepository;
    }

    public Guid ClassId { get; set; }
    public string ClassName { get; set; }
    public List<StudentInClassDto> Students { get; set; }

    public async Task OnGetAsync(Guid classId)
    {
        ClassId = classId;

        var classEntity = await _classRepository.GetAsync(classId);
        ClassName = classEntity.Name;

        var classStudents = await _classStudentRepository.GetListAsync();
        var allStudents = await _studentRepository.GetListAsync();

        Students = classStudents
            .Where(cs => cs.ClassId == classId)
            .Join(allStudents,
                cs => cs.StudentId,
                s => s.Id,
                (cs, s) => new StudentInClassDto
                {
                    StudentId = s.Id,
                    FullName = s.FullName,
                    DateOfBirth = s.DateOfBirth,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    EnrollmentDate = cs.EnrollmentDate
                })
            .ToList();
    }

    public async Task<IActionResult> OnPostAsync(Guid studentId, Guid classId)
    {
        var classStudents = await _classStudentRepository.GetListAsync();
        var record = classStudents.FirstOrDefault(cs =>
            cs.ClassId == classId && cs.StudentId == studentId);

        if (record != null)
        {
            await _classStudentRepository.DeleteAsync(record);
        }

        return RedirectToPage("/Classes/Students", new { classId });
    }
}

public class StudentInClassDto
{
    public Guid StudentId { get; set; }
    public string FullName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime EnrollmentDate { get; set; }
}