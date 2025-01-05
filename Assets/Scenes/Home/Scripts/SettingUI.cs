using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingUI : MonoBehaviour
{   
    public GameObject settingFrame;
    public Button closeButton;
    public Button languageButton;
    public Slider soundToggle;
    public Slider bgmToggle;
    public Slider vibrationToggle;
    public AudioClip buttonClickSound;
    AudioManager audioManager;
    private void Awake() {
        Initialization();
    }
    
    // 初始化
    private void Initialization(){
        audioManager = AudioManager.Instance;

        closeButton.onClick.AddListener(SettingFrameVisible);
        languageButton.onClick.AddListener(ChangeLanguage);
        soundToggle.onValueChanged.AddListener(SoundManager);
        bgmToggle.onValueChanged.AddListener(BGMManager);
        vibrationToggle.onValueChanged.AddListener(VibrationManager);        
    }

    // 關閉SettingFrame
    private void SettingFrameVisible(){
        audioManager.PlaySound(buttonClickSound);
        if(settingFrame != null){
            settingFrame.SetActive(false);
        }
    }

    // 切換中英文
    private void ChangeLanguage(){
        var currentLocal = LocalizationSettings.SelectedLocale;
        Transform lgb = languageButton.transform;
        TextMeshProUGUI chineseText = lgb.Find("Chinese").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI englishText = lgb.Find("English").GetComponent<TextMeshProUGUI>();
        
        Color32 off = new Color32(0, 0, 0, 255);
        Color32 on = new Color32(255, 255, 255, 255);

        audioManager.PlaySound(buttonClickSound);
        if(currentLocal == LocalizationSettings.AvailableLocales.Locales[0]){
            SetLanguage("en");
            chineseText.color = off;
            englishText.color = on;
        } else {
            SetLanguage("zh-TW");
            chineseText.color = on;
            englishText.color = off;
        }
    }

    // 管理音樂開關
    private void SoundManager(float value){
        if(Mathf.Approximately(value, 1.0f)){
            audioManager.canPlaySounds = true;
        } else if(Mathf.Approximately(value, 0.0f)){
            audioManager.canPlaySounds = false;
        }
    }

    // 管理背景音樂開關
    private void BGMManager(float value){
        if(Mathf.Approximately(value, 1.0f)){
            
        } else if(Mathf.Approximately(value, 0.0f)){

        }
    }

    // 管理震動開關
    private void VibrationManager(float value){
        if(Mathf.Approximately(value, 1.0f)){
            Handheld.Vibrate();
        } else if(Mathf.Approximately(value, 0.0f)){

        }
    }

    private void SetLanguage(string localeCode)
    {
        Locale locale = LocalizationSettings.AvailableLocales.GetLocale(localeCode);
        if (locale != null)
        {
            LocalizationSettings.SelectedLocale = locale;
        }
        else
        {
            Debug.LogWarning($"Locale '{localeCode}' not found!");
        }
    }
}
