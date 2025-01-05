using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellingBee : MonoBehaviour
{
    public GameObject[] images;
    private int idx = 0;
    private string targetStr = "walrus";
    public AudioClip clickSound;
    public static event Action OnLevelCompeleted;
    private void Awake(){
        foreach(GameObject image in images){
            Image img = image.GetComponent<Image>();
            img.color = new Color32(150, 150, 150, 255);
        }
    }

    public void checkPair(char str){
        AudioManager.Instance.PlaySound(clickSound);

        if(idx < targetStr.Length && str == targetStr[idx]){
            Image img = images[idx].GetComponent<Image>();
            img.color = new Color32(255, 255, 255, 255);

            idx++;
            if(idx == targetStr.Length){
                OnLevelCompeleted?.Invoke();
            }
        }
    }
}
