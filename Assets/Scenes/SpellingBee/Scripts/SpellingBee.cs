using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
            Hide(image);
        }
    }

    private void Show(GameObject image){
        Image img = image.GetComponent<Image>();
        GameObject childText = findTextChild(image);

        img.color = new Color32(255, 255, 255, 255);
        childText.SetActive(true);
    }

    private void Hide(GameObject image){
        Image img = image.GetComponent<Image>();
        GameObject childText = findTextChild(image);

        img.color = new Color32(150, 150, 150, 255);
        childText.SetActive(false);
    }

    private GameObject findTextChild(GameObject parentObject){            
        GameObject childText = parentObject.transform.GetChild(0).gameObject;

        return childText;
    }

    public void checkPair(char str){
        AudioManager.Instance.PlaySound(clickSound);

        if(idx < targetStr.Length && str == targetStr[idx]){
            Show(images[idx]);
            
            idx++;
            if(idx == targetStr.Length){
                OnLevelCompeleted?.Invoke();
            }
        } else {
            idx = 0;
            foreach(GameObject image in images){
                Hide(image);
            }
        }
    }
}
