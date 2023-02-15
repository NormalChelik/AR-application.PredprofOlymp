using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField]
    private Button SettingsButton;
    [SerializeField]
    private Button StartButton;

    [Header("Slider")]
    [SerializeField]
    private Slider AudioSlider;

    [Header("Settings")]
    [SerializeField]
    private AudioSource audioSource;

    [Header("Other")]
    [SerializeField]
    private GameObject LoadingBar;

    private void Start()
    {
        SettingsButton.onClick.AddListener(SettingsButtonActive);
        StartButton.onClick.AddListener(StartFunction);

        if (PlayerPrefs.HasKey("musicValue"))
        {
            AudioSlider.value = PlayerPrefs.GetFloat("musicValue");
        }
        else AudioSlider.value = 0.8f;
    }

    private void Update()
    {
        audioSource.volume = AudioSlider.value;
    }
    private void SettingsButtonActive()
    {
        StartButton.gameObject.SetActive(!StartButton.gameObject.activeSelf);
        AudioSlider.gameObject.SetActive(!AudioSlider.gameObject.activeSelf);
    }
    private void StartFunction()
    {
        LoadingBar.SetActive(true);
    }

    public void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat("musicValue", AudioSlider.value);
    }

}
