using Volo.Abp.Modularity;

namespace TestProject;

public abstract class TestProjectApplicationTestBase<TStartupModule> : TestProjectTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
