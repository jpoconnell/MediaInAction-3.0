using Volo.Abp.Settings;

namespace MediaInAction.TraktService.Settings
{
    public class TraktServiceSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(TraktServiceSettings.MySetting1));
        }
    }
}
