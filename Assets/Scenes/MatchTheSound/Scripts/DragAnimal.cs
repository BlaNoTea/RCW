using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAnimal : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{   
    public TapesManager tapesManager;
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 OriginalPos;
    public AudioClip clickSound;
    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        OriginalPos = rectTransform.anchoredPosition;
    }
    public void OnPointerUp(PointerEventData eventData){
        rectTransform.anchoredPosition = OriginalPos;

        if(tapesManager != null){
            tapesManager.DragEnded(gameObject, eventData);
        }
    }

    public void OnPointerDown(PointerEventData eventData){

        AudioManager.Instance.PlaySound(clickSound);

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        rectTransform.anchoredPosition = localPoint;
    }

    public void OnDrag(PointerEventData eventData){
        if(canvas == null) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
