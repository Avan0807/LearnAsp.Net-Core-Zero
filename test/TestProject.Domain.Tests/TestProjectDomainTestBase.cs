using Volo.Abp.Modularity;

namespace TestProject;

/* Inherit from this class for your domain layer tests. */
public abstract class TestProjectDomainTestBase<TStartupModule> : TestProjectTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
