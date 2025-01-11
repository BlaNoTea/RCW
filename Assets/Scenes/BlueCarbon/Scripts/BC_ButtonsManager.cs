using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BC_ButtonsManager : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    public GameObject buttonTemplate;
    public GameObject[] allButtons;

    private void Awake(){
        
    }

    private void Hide(GameObject obj){
        GameObject bg = obj.transform.GetChild(0).gameObject;
        GameObject textMesh = obj.transform.GetChild(1).gameObject;
        
        bg.SetActive(false);
        textMesh.SetActive(false);
    }

    private void Show(GameObject obj){
        GameObject bg = obj.transform.GetChild(0).gameObject;
        GameObject textMesh = obj.transform.GetChild(1).gameObject;

        bg.SetActive(false);
        textMesh.SetActive(false);
    }

    public void OnPointerUp(PointerEventData eventData){
        
    }

    public void OnPointerDown(PointerEventData eventData){

    }

    public void OnDrag(PointerEventData eventData){

    }
}
