using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace TestProject.Data;

/* This is used if database provider does't define
 * ITestProjectDbSchemaMigrator implementation.
 */
public class NullTestProjectDbSchemaMigrator : ITestProjectDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
