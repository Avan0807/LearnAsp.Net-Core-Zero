using TestProject.Samples;
using Xunit;

namespace TestProject.EntityFrameworkCore.Domains;

[Collection(TestProjectTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<TestProjectEntityFrameworkCoreTestModule>
{

}
