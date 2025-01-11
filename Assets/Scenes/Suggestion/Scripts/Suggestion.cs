using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization.SmartFormat.Core.Parsing;

public class Suggestion : MonoBehaviour
{
    public GameObject[] suitable;
    public GameObject[] unsuitable;
    public TextMeshProUGUI score;
    public int total;
    public static event Action OnLevelCompeleted;
    private void Awake(){
        total = suitable.Length;
        score.text = string.Format("{0}/4", total);
    }

    private bool checkOverlapping(GameObject image, PointerEventData eventData){
        return RectTransformUtility.RectangleContainsScreenPoint(image.GetComponent<RectTransform>(), eventData.position);
    }

    public void DragEnded(PointerEventData eventData){
        foreach(GameObject image in suitable){
            if(image.activeSelf == true && image != null && checkOverlapping(image, eventData)){
                image.SetActive(false);

                total--;
                score.text = string.Format("{0}/4", total);

                if(total == 0){
                    OnLevelCompeleted?.Invoke();
                }

                return;
            }
        }

        foreach(GameObject image in unsuitable){
            if(image != null && checkOverlapping(image, eventData)){
                Handheld.Vibrate();
                return;
            }
        }
    }
}
