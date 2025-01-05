using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{   
    public SpellingBee spellingBee;
    public Button[] buttons;
    private string buttonStrings = "wsacurlod";

    private void Awake(){
        for(int i = 0; i < buttons.Length; i++){
            int idx = i;
            buttons[i].onClick.AddListener(() => spellingBee.checkPair(buttonStrings[idx]));
        }
    }
}
