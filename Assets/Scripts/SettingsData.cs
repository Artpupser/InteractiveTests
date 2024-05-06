using System;
using Newtonsoft.Json;
[Serializable]
public class SettingsData : IJsonData
{
    public static string FileName { get => "settings.json"; }
    public static SettingsData Instance { get; set; }
    [JsonProperty]
    public float MaxScore { get => _maxScore; set => _maxScore = value; }
    [JsonProperty]
    public float MusicVolume { get => _musicVolume; set => _musicVolume = value; }
    [JsonProperty]
    public byte ResolutionLevel { get => _resolutionLevel; set => _resolutionLevel = value; }

    private float _maxScore = 5;
    private float _musicVolume = 1f;
    private byte _resolutionLevel = 1;

    [JsonConstructor]
    public SettingsData(float maxScore, float musicVolume, byte resolutionLevel)
    {
        MaxScore = maxScore;
        MusicVolume = musicVolume;
        ResolutionLevel = resolutionLevel;
    }
}