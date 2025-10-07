using Microsoft.AspNetCore.Builder;
using TestProject;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("TestProject.Web.csproj");
await builder.RunAbpModuleAsync<TestProjectWebTestModule>(applicationName: "TestProject.Web" );

public partial class Program
{
}
