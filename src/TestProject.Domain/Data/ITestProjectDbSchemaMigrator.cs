using System.Threading.Tasks;

namespace TestProject.Data;

public interface ITestProjectDbSchemaMigrator
{
    Task MigrateAsync();
}
