using Volo.Abp.Modularity;

namespace TestProject;

[DependsOn(
    typeof(TestProjectApplicationModule),
    typeof(TestProjectDomainTestModule)
)]
public class TestProjectApplicationTestModule : AbpModule
{

}
