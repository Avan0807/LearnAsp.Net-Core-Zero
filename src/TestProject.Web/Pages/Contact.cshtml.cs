using Microsoft.AspNetCore.Mvc;

namespace TestProject.Web.Pages;

public class ContactModel : TestProjectPageModel
{
    [BindProperty]
    public string Name { get; set; }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Subject { get; set; }

    [BindProperty]
    public string Message { get; set; }

    public string SuccessMessage { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
            return Page();

        SuccessMessage = $"Thank you {Name}! We received your message and will contact you soon.";

        ModelState.Clear();
        Name = Email = Subject = Message = string.Empty;

        return Page();
    }
}