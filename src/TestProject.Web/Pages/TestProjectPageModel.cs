using TestProject.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace TestProject.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class TestProjectPageModel : AbpPageModel
{
    protected TestProjectPageModel()
    {
        LocalizationResourceType = typeof(TestProjectResource);
    }
}
