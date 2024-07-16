using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResolutionControl : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;
    private int currentResolutionIndex = 0;

    private void Start()
    {
        resolutions = Screen.resolutions;

        List<string> options = new List<string>();
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRateRatio + "Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen); // z tym fullscreenem popracuj
    }
}
