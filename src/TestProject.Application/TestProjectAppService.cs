using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Localization;
using Volo.Abp.Application.Services;

namespace TestProject;

/* Inherit your application services from this class.
 */
public abstract class TestProjectAppService : ApplicationService
{
    protected TestProjectAppService()
    {
        LocalizationResource = typeof(TestProjectResource);
    }
}
