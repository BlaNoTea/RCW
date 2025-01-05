using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class DragFish : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 originalPosition;
    public static event Action OnLevelCompeleted;
    public AudioClip clickSound;
    [SerializeField] private GameObject targetImage;
    [SerializeField] private GameObject[] AllImages;

    private void Awake(){
        reOrder();

        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        originalPosition = rectTransform.anchoredPosition;
    }

    private void reOrder(){
        List<GameObject> objects = new List<GameObject>(AllImages);

        FisherYatesShuffle(objects);

        for (int i = 0; i < objects.Count; i++){
            RectTransform targetRect = objects[i].GetComponent<RectTransform>();
            targetRect.SetSiblingIndex(i);
        }
    }
    private void FisherYatesShuffle(List<GameObject> list){
        System.Random rng = new System.Random();
        int n = list.Count;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            GameObject value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public void OnPointerDown(PointerEventData eventData){
        AudioManager.Instance.PlaySound(clickSound);

        // 將觸控點轉換為父物件的本地座標
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPointerPosition
        ))
        {
            // 計算目標位置，考慮 Canvas 縮放
            Vector2 pivotOffset = new Vector2(
                (0.5f - rectTransform.pivot.x) * rectTransform.rect.width * rectTransform.lossyScale.x,
                (3.75f - rectTransform.pivot.y) * rectTransform.rect.height * rectTransform.lossyScale.y
            );

            // 修正目標位置
            rectTransform.anchoredPosition = (localPointerPosition + pivotOffset) / canvas.scaleFactor;
        }
    }

    public void OnPointerUp(PointerEventData eventData){
        // 偵測是否對應到目標圖片上
        if(targetImage != null && RectTransformUtility.RectangleContainsScreenPoint(targetImage.GetComponent<RectTransform>(), eventData.position)){
            gameObject.SetActive(false);
            OnLevelCompeleted?.Invoke();
            return;
        }

        foreach(var target in AllImages){
            if(target != null && RectTransformUtility.RectangleContainsScreenPoint(target.GetComponent<RectTransform>(), eventData.position)){
                Handheld.Vibrate();
                break;
            }
        }

        rectTransform.anchoredPosition = originalPosition;
    }

    public void OnDrag(PointerEventData eventData){
        if (canvas == null) return;

        // 調整移動的偏移量
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}