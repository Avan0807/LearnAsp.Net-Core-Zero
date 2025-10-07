using Xunit;

namespace TestProject.EntityFrameworkCore;

[CollectionDefinition(TestProjectTestConsts.CollectionDefinitionName)]
public class TestProjectEntityFrameworkCoreCollection : ICollectionFixture<TestProjectEntityFrameworkCoreFixture>
{

}
