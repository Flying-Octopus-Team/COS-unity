using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Main Menu", menuName = "ScriptableObjects/Main Menu")]
public class MainMenu : ScriptableObject {
    [SerializeField] private AudioMixer effectsVolume;
    [SerializeField] private AudioMixer musicVolume;

    public void StartGame() {
        SceneManager.LoadScene(1);
    }

    public void ChangeGraphicLevel(int level) {
        QualitySettings.SetQualityLevel(level, true);
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