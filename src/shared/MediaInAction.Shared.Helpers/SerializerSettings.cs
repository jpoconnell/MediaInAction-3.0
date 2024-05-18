using Newtonsoft.Json;

namespace MediaInAction.Helpers
{
    public static class SerializerSettings
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            //TypeNameHandling = TypeNameHandling.Objects,
            //TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
            NullValueHandling = NullValueHandling.Ignore
        };
    }
}