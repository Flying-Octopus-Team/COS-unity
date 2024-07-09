using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettingsMainMenu : MonoBehaviour
{
    [SerializeField] Slider MusicVolumeSlider;
    [SerializeField] Slider EffectVolumeSlider;
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
        FullscreenDropdown.value = PlayerPrefs.GetInt("FullscreenMode");
        GraphicLevelDropdown.value = PlayerPrefs.GetInt("GraphicLEvel");
    }
}
