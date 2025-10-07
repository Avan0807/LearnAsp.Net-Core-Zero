using Microsoft.Extensions.Localization;
using TestProject.Localization;
using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace TestProject.Web;

[Dependency(ReplaceServices = true)]
public class TestProjectBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<TestProjectResource> _localizer;

    public TestProjectBrandingProvider(IStringLocalizer<TestProjectResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
