using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _maxScoreInput;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private TextMeshProUGUI _debugText;
    [SerializeField] private AudioMixer _mixer;
    private void OnEnable()
    {
        _maxScoreInput.onValueChanged.AddListener(OnChangeAnyValue);
        _resolutionDropdown.onValueChanged.AddListener(OnChangeAnyValue);
        _musicVolumeSlider.onValueChanged.AddListener(OnChangeAnyValue);
    }
    private void OnDisable()
    {
        _maxScoreInput.onValueChanged.RemoveListener(OnChangeAnyValue);
        _resolutionDropdown.onValueChanged.RemoveListener(OnChangeAnyValue);
        _musicVolumeSlider.onValueChanged.RemoveListener(OnChangeAnyValue);
    }
    private void Awake()
    {
        _maxScoreInput.text = SettingsData.Instance.MaxScore.ToString();
        _resolutionDropdown.value = SettingsData.Instance.ResolutionLevel;
        _musicVolumeSlider.value = SettingsData.Instance.MusicVolume;
    }
    private void OnChangeAnyValue<T>(T change)
    {
        if(Apply())
            Save();
    }
    public static void Save()
    {
        FileManager.SaveFile(SettingsData.FileName, SettingsData.Instance);
    }
    public void OnBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public static void SetResolution(byte level)
    {
        switch (level)
        {
            case 0:
                Screen.SetResolution(1280, 720, true);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 2:
                Screen.SetResolution(2560, 1440, true);
                break;
            case 3:
                Screen.SetResolution(3840, 2160, true);
                break;
        }
    }
    private bool Apply()
    {
        if (!int.TryParse(_maxScoreInput.text, out int value) || value < 1) //Bug fixed
        {
            _debugText.text = "[Макс балл] Данное значени не валидно!, [Настройки не сохранились]";
            return false;
        }
        _debugText.text = "";
        SettingsData.Instance.ResolutionLevel = (byte)_resolutionDropdown.value;
        SettingsData.Instance.MusicVolume = _musicVolumeSlider.value;
        SettingsData.Instance.MaxScore = value;
        _mixer.SetFloat("Music", SettingsData.Instance.MusicVolume);
        SetResolution(SettingsData.Instance.ResolutionLevel);
        return true;
    }
}