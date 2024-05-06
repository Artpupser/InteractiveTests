using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    private void Awake()
    {
        if (FileManager.LoadFile(SettingsData.FileName, out SettingsData output))
        {
            SettingsData.Instance = output;
        }
        else
        {
            SettingsData.Instance = new(5, 0, 1);
            SettingsManager.Save();
        }
        SettingsManager.SetResolution(SettingsData.Instance.ResolutionLevel);
        mixer.SetFloat("Music", SettingsData.Instance.MusicVolume);
        SceneManager.LoadScene("MainMenu");
    }
}
