using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsUI : MonoBehaviour
{
    public Button[] group1 = new Button[3];
    public Button[] group2 = new Button[3];
    public Button[] group3 = new Button[3];
    public Button[] group4 = new Button[3];
    public Button[] group5 = new Button[3];
    public Button[] group6 = new Button[3];

    private Button[][] buttonGroups;
    private bool[][] buttonGroupStates;
    private string[][] scenes;
    private string[] sharedLevels;
    public AudioClip clickSound;

    public static event Action<string[]> OnLevelsReady;

    private void Awake() {
        sharedLevels = new string[6];
        scenes = new string[6][];

        scenes[0] = new string[]{"VRtest", "", ""};
        scenes[1] = new string[]{"", "WreckTheWaste", ""};
        scenes[2] = new string[]{"Coral", "", ""};
        scenes[3] = new string[]{"", "Suggestion", ""};
        scenes[4] = new string[]{"SpellingBee", "", ""};
        scenes[5] = new string[]{"MatchTheSound", "", ""};

        buttonGroups = new Button[][]{
            group1,
            group2, 
            group3,
            group4,
            group5,
            group6
        };

        buttonGroupStates = new bool[buttonGroups.Length][];

        for (int i = 0; i < buttonGroups.Length; i++){
            buttonGroupStates[i] = new bool[3];

            for (int j = 0; j < buttonGroups[i].Length; j++){
                int idx1 = i;
                int idx2 = j;
                buttonGroups[i][j].onClick.AddListener(() => OnButtonClick(buttonGroups[idx1][idx2], idx1, idx2));
            }
        }
    }

    private void OnButtonClick(Button clickedButton, int groupIndex, int Index){
        AudioManager.Instance.PlaySound(clickSound);

        if (buttonGroupStates[groupIndex].Length != 3) return;

        Button[] groupButtons = buttonGroups[groupIndex];

        for (int i = 0; i < groupButtons.Length; i++)
        {
            if (groupButtons[i] == clickedButton)
            {   
                sharedLevels[groupIndex] = scenes[groupIndex][Index];
                groupButtons[i].GetComponent<Image>().color = new Color32(190, 190, 190, 255);
            }
            else
            {
                groupButtons[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
        }

        foreach(string str in sharedLevels){
            if(str == null || str == "") return;
        }

        OnLevelsReady?.Invoke(sharedLevels);
    }
}
