using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {
    // Start is called before the first frame update
    List<(int w, int h)> _resolutions = new List<(int w, int h)>()
    {
        (2560, 1440),
        (1920, 1080),
        (1680, 1050),
        (1600, 900),
        (1440, 900),
        (1366, 768),
        (1360, 768),
        (1280, 720),
        (800, 600),
    };

    public GameObject _optionContainer;
    public TMP_Dropdown _resDrop;

    (int w, int h) _currentResolution;
    bool _fullscreen;




    void Start() {

        _currentResolution = (Screen.width, Screen.height);
        _fullscreen = Screen.fullScreen;

        _resDrop.options.Clear();
        foreach ((int w, int h) res in _resolutions) {
            string optionText = res.w + "x" + res.h;
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData() { text = optionText };
            _resDrop.options.Add(option);
        }
    }

    // Update is called once per frame
    void Update() {

    }


    public void LoadScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }
    public void ToggleOptions() {
        _optionContainer.SetActive(!_optionContainer.activeSelf);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void ChangeResolution(int val) {
        _currentResolution = _resolutions[val];
        UpdateResolution();
    }

    public void ChangeFullscreen(bool val) {
        _fullscreen = val;
        UpdateResolution();

    }

    void UpdateResolution() {
        Screen.SetResolution(_currentResolution.w, _currentResolution.h, _fullscreen);
    }

    public void ChangeVolume(float val) {
        // GlobalSettings.ChangeVolume(val);
        Debug.Log("Changed volume to: " + val);
    }
}
