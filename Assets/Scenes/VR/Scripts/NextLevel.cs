using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{   
    public Button testbtn;
    public static event Action OnLevelCompeleted;

    private void Awake(){
        testbtn.onClick.AddListener(() => OnLevelCompeleted?.Invoke());
    }
}
