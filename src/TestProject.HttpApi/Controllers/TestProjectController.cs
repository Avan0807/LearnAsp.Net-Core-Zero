using TestProject.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace TestProject.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TestProjectController : AbpControllerBase
{
    protected TestProjectController()
    {
        LocalizationResource = typeof(TestProjectResource);
    }
}
