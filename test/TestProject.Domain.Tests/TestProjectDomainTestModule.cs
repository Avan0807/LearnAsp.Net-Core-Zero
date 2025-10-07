using Volo.Abp.Modularity;

namespace TestProject;

[DependsOn(
    typeof(TestProjectDomainModule),
    typeof(TestProjectTestBaseModule)
)]
public class TestProjectDomainTestModule : AbpModule
{

}
