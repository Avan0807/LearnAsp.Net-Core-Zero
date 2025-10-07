using TestProject.Samples;
using Xunit;

namespace TestProject.EntityFrameworkCore.Applications;

[Collection(TestProjectTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<TestProjectEntityFrameworkCoreTestModule>
{

}
