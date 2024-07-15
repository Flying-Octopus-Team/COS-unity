using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[CreateAssetMenu(fileName = "Main Menu", menuName = "ScriptableObjects/Main Menu")]
public class MainMenu : ScriptableObject {
    [SerializeField] private AudioMixer effectsVolume;
    [SerializeField] private AudioMixer musicVolume;
    [SerializeField] private VolumeProfile SampleSceneProfile;
    [SerializeField] private VolumeProfile SampleSceneProfile1;

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void ChangeGraphicLevel(int level) {
        PlayerPrefs.SetInt("GraphicLevel", level);
        level = Mathf.Clamp(level, 0, QualitySettings.names.Length-1);
        QualitySettings.SetQualityLevel(level, Screen.fullScreen);
    }
    public void ChangeFullscreen(int level)
    {
        PlayerPrefs.SetInt("FullscreenMode", level);
        Screen.fullScreen = Convert.ToBoolean(level);
    }

    public void SetEffectVolume(float sliderValue) {
        PlayerPrefs.SetFloat("EffectVolume", sliderValue);
        effectsVolume.SetFloat("EffectsVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicVolume(float sliderValue) {
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
        musicVolume.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }
    public void ChangeMouseSens(float sliderValue)
    {
        PlayerPrefs.SetFloat("MouseSensitivity", sliderValue);
    }
    public void ChangeBlurEffectStatus(bool value)
    {
        PlayerPrefs.SetInt("BlurEffectStatus", Convert.ToInt32(value));
        MotionBlur motionBlur;
        if(SampleSceneProfile.TryGet<MotionBlur>(out motionBlur))
        {
            motionBlur.active = value;
        }
    }

    public void ExitGame() {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}