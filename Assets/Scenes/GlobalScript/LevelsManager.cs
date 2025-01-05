using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public LoadingManager loadingManager;
    private string[] scenes = new string[6];
    private int currIdx = 0;
    private void OnEnable(){
        LevelsUI.OnLevelsReady += handleReady;
        NextLevel.OnLevelCompeleted += nextLevel;
        DragFish.OnLevelCompeleted += nextLevel;
        TapesManager.OnLevelCompeleted += nextLevel;
        SpellingBee.OnLevelCompeleted += nextLevel;
        Suggestion.OnLevelCompeleted += nextLevel;
        WreckTheWaste.OnLevelCompeleted += nextLevel;
    }

    private void OnDisable(){
        LevelsUI.OnLevelsReady -= handleReady;
        NextLevel.OnLevelCompeleted -= nextLevel;
        DragFish.OnLevelCompeleted -= nextLevel;
        TapesManager.OnLevelCompeleted -= nextLevel;
        SpellingBee.OnLevelCompeleted -= nextLevel;
        Suggestion.OnLevelCompeleted -= nextLevel;
        WreckTheWaste.OnLevelCompeleted -= nextLevel;
    }

    private void handleReady(string[] levels){
        scenes = levels;
        loadingManager.Sceneloader(scenes[currIdx]);
    }

    private void nextLevel(){
        currIdx++;
        if(currIdx == 6){
            loadingManager.Sceneloader("Home");
            currIdx = 0;
        } else {
            loadingManager.Sceneloader(scenes[currIdx]);
        }
    }
}
