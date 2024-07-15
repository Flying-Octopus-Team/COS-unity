using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SettingsMainMenu : MonoBehaviour
{
    [SerializeField] Slider MusicVolumeSlider;
    [SerializeField] Slider EffectVolumeSlider;
    [SerializeField] Slider MouseSensitivitySlider;
    [SerializeField] Toggle BlurEffectToggle;
    [SerializeField] TMP_Dropdown FullscreenDropdown;
    [SerializeField] TMP_Dropdown GraphicLevelDropdown;

    private void Start()
    {
        LoadSettingsData();
    }

    void LoadSettingsData()
    {
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        EffectVolumeSlider.value = PlayerPrefs.GetFloat("EffectVolume");
        MouseSensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity");
        FullscreenDropdown.value = PlayerPrefs.GetInt("FullscreenMode");
        GraphicLevelDropdown.value = PlayerPrefs.GetInt("GraphicLEvel");
        BlurEffectToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("BlurEffectStatus"));
    }
}
