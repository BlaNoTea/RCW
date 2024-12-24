using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    private float deltaTime = 0.0f;
    
    void Update()
    {
        // 計算每幀的時間
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        // 計算每秒幾幀
        float fps = 1.0f / deltaTime;

        // 設置顯示位置和顏色
        GUI.color = Color.green;
        GUI.skin.label.fontSize = 24;

        // 顯示 FPS
        GUI.Label(new Rect(10, 10, 100, 30), "FPS: " + Mathf.Ceil(fps).ToString());
    }
}
