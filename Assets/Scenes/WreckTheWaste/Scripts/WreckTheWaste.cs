using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WreckTheWaste : MonoBehaviour
{
    public GameObject[] suitable;
    public GameObject[] unsuitable;
    public int total;
    public static event Action OnLevelCompeleted;

    private bool checkOverlapping(GameObject image, PointerEventData eventData){
        return RectTransformUtility.RectangleContainsScreenPoint(image.GetComponent<RectTransform>(), eventData.position);
    }

    public void DragEnded(PointerEventData eventData){
        foreach(GameObject image in suitable){
            if(image != null && checkOverlapping(image, eventData)){
                image.SetActive(false);

                total--;
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
