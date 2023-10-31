using Volo.Abp.Settings;

namespace IsteerPortal.Settings;

public class IsteerPortalSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(IsteerPortalSettings.MySetting1));
    }
}
