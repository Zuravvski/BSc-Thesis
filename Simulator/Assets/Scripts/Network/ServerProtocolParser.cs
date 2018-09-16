using Newtonsoft.Json;

public class ServerProtocolParser : IFrameParser
{
    private readonly JsonSerializerSettings _settings;

    public ServerProtocolParser()
    {
        _settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
        };
    }

    public ICommand Parse(string frame)
    {
        return JsonConvert.DeserializeObject<ICommand>(frame, _settings);
    }
}
