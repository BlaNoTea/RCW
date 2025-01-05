using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class WHW_DragNet : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    public WreckTheWaste wreckTheWaste;
    public RectTransform image2;
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 OriginalPos;
    private Vector2 Anchors;
    public AudioClip clickSound;
    private void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        Anchors = new Vector2(rectTransform.anchorMin.x, rectTransform.anchorMin.y);
        OriginalPos = rectTransform.anchoredPosition;
    }

    public void OnPointerUp(PointerEventData eventData){
        rectTransform.anchorMin = Anchors;
        rectTransform.anchorMax = Anchors;
        
        rectTransform.anchoredPosition = OriginalPos;

        if(wreckTheWaste != null){
            wreckTheWaste.DragEnded(eventData);
        }
    }

    public void OnPointerDown(PointerEventData eventData){
        AudioManager.Instance.PlaySound(clickSound);

        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);

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
