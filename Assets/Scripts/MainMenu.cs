using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;

[CreateAssetMenu(fileName = "Main Menu", menuName = "ScriptableObjects/Main Menu")]
public class MainMenu : ScriptableObject {
    [SerializeField] private AudioMixer effectsVolume;
    [SerializeField] private AudioMixer musicVolume;

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void ChangeGraphicLevel(int level) {
        level = Mathf.Clamp(level, 0, QualitySettings.names.Length-1);
        QualitySettings.SetQualityLevel(level, true);
    }
    public void ChangeFullscreen(int level)
    {
        Screen.fullScreen = Convert.ToBoolean(level);
    }

    public void SetEffectVolume(float sliderValue) {
        effectsVolume.SetFloat("EffectsVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetMusicVolume(float sliderValue) {
        musicVolume.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void ExitGame() {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}