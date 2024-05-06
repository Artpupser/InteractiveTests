using Newtonsoft.Json;

public interface IJsonData
{
    public string Serialize() => JsonConvert.SerializeObject(this);
}
