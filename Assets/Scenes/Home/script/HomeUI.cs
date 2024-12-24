using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : MonoBehaviour
{   
    public GameObject settingFrame;
    public GameObject levelsFrame;
    public GameObject homeFrame;
    public Button settingButton;
    public Button startButton;
    public Button leaveButton;
    public AudioClip buttonClickSound;
    private AudioManager audioManager;

    private void Awake() {
        // 保存AudioManager的引用
        audioManager = AudioManager.Instance;
        // 添加監聽器
        settingButton.onClick.AddListener(SettingFrameVisible);
        leaveButton.onClick.AddListener(QuitGame);
        startButton.onClick.AddListener(ChangeToLevelFrame);
    }

    // 打開LevelsFrame
    private void ChangeToLevelFrame(){
        // 播放聲音
        audioManager.PlaySound(buttonClickSound);
        // 關閉homeFrame並打開levelsFrame
        if(levelsFrame != null && homeFrame != null){
            homeFrame.SetActive(false);
            levelsFrame.SetActive(true);
        }
    }

    // 打開SettingFrame
    private void SettingFrameVisible(){
        // 播放聲音
        audioManager.PlaySound(buttonClickSound);
        // 設置Frame為Active
        if(settingFrame != null){
            settingFrame.SetActive(true);
        }
    }

    // 退出
    private void QuitGame(){
        // 一般正常退出
        audioManager.PlaySound(buttonClickSound);
        Application.Quit();

        // 模擬退出 :P
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
