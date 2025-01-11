using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TapesManager : MonoBehaviour
{
    public AudioClip[] music;
    public GameObject[] tapes;
    public GameObject[] images;
    private bool[] canPair;
    private AudioSource audioSource;
    private int total;
    public static event Action OnLevelCompeleted;

    private void Awake(){
        audioSource = gameObject.GetComponent<AudioSource>();
        if(audioSource == null){
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        total = music.Length;

        canPair = new bool[3];
        for(int i = 0; i < canPair.Length; i++){
            canPair[i] = false;
        }

        FisherYatesShuffle(tapes);

        for(int i = 0; i < tapes.Length; i++){
            int idx = i;
            Button tape = tapes[i].GetComponent<Button>();
            tape.onClick.AddListener(() => OnButtonClicked(idx));
        }
    }

    private void FisherYatesShuffle(GameObject[] list){
        System.Random rng = new System.Random();
        int n = list.Length;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            GameObject value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private bool checkOverlapping(GameObject btn, PointerEventData eventData){
        return RectTransformUtility.RectangleContainsScreenPoint(btn.GetComponent<RectTransform>(), eventData.position);
    }

    private void OnButtonClicked(int idx){
        if(idx >= 0 && idx < music.Length){
            canPair[idx] = true;
            audioSource.clip = music[idx];
            audioSource.Play();
        }
    }

    public void DragEnded(GameObject currimg, PointerEventData eventData){
        int idx = System.Array.IndexOf(images, currimg);
        if(canPair[idx] == true && tapes[idx] != null && checkOverlapping(tapes[idx], eventData)){
            images[idx].SetActive(false);
            tapes[idx].SetActive(false);
            total--;

            if(total == 0){
                OnLevelCompeleted?.Invoke();
            }

            return;
        }

        foreach(GameObject tape in tapes){
            if(checkOverlapping(tape, eventData)){
                Handheld.Vibrate();
                break;
            }
        }
    }
}
