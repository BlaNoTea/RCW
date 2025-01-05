using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public GameObject loadingScreen;  
    private Animator loadingAnimator;
    public void Start(){
        loadingAnimator = loadingScreen.GetComponent<Animator>();
        StartCoroutine(Init("Home"));
    }

    public void Sceneloader(string sceneName){
        StartCoroutine(FadeInAndOut(sceneName));
    }

    IEnumerator Init(string sceneName){
        yield return StartCoroutine(LoadSceneAsync(sceneName));
        yield return new WaitForSeconds(1.0f);

        loadingAnimator.Play("loading_fadeout");
    }

    IEnumerator FadeInAndOut(string sceneName){
        loadingAnimator.Play("loading_fadein");

        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(LoadSceneAsync(sceneName));

        loadingAnimator.Play("loading_fadeout");
    }

    IEnumerator LoadSceneAsync(string sceneName){
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while(!operation.isDone){
            if (operation.progress >= 0.9f){
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
