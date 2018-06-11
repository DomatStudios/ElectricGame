using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {
    [SerializeField]
    AudioMixer audioMixer;

    //Easter Egg
    string filePath;
    //Resolution
    Resolution[] resolutions;

    //dropdowns
    [SerializeField]
    Dropdown resolutionDropdown;

    //Options Panel
    [SerializeField]
    GameObject optionsPanel;

    //Easter Egg stuff
    void Awake() {
        filePath = "C:/Users/" + Environment.UserName + "/Desktop/game";
        //OptionsPanel
        optionsPanel.SetActive(false);
    }

	void Start() {
        //for making a easter egg
        var folder = Directory.CreateDirectory(filePath);
        //Resolutions
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string> ();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
	}
	
	// This is for making a Easter Egg
	public void Button() {
        File.WriteAllText("C:/Users/" + Environment.UserName + "/Desktop/game/game.txt", "I'm a secret guy.");
	}

    public void NewGame() {
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit() {
        Application.Quit();
    }

    // ---------------Options-------------//

    public void ShowOptionsPanel(bool isShowing) {
        optionsPanel.SetActive(isShowing);
    }

    public void SetResolution(int resolutionIndex) {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void FullScreen(bool isFullScreen) {
        Screen.fullScreen = isFullScreen;
    }
}
