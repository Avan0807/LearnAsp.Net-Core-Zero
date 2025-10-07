using TestProject.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace TestProject.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(TestProjectEntityFrameworkCoreModule),
    typeof(TestProjectApplicationContractsModule)
    )]
public class TestProjectDbMigratorModule : AbpModule
{
}
