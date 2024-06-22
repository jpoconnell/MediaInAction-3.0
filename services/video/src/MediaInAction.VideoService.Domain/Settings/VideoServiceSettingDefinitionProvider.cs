using Volo.Abp.Settings;

namespace MediaInAction.VideoService.Settings
{
    public class VideoServiceSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(VideoServiceSettings.MySetting1));
        }
    }
}
