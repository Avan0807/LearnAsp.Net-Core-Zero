using Volo.Abp.Settings;

namespace TestProject.Settings;

public class TestProjectSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(TestProjectSettings.MySetting1));
    }
}
