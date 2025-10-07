using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace TestProject.Pages;

public class Index_Tests : TestProjectWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
