using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class Wifi : MonoBehaviour
{
    public string url = "http://172.20.10.2/";

    private void Awake() {
        init();
    }

    public void changeLevel(int idx){
        Debug.Log(idx);
        StartCoroutine(Connect(string.Format("{0}electromagnet{1}_on", url, idx % 6)));

        if(idx == 6){
            water();
        }
    }

    private void init(){
        StartCoroutine(Connect(string.Format("{0}electromagnet1_on", url)));
    }

    private void water(){
        StartCoroutine(ConnectWater(string.Format("{0}electromagnet6_on", url), 1));
        StartCoroutine(Connect(string.Format("{0}electromagnet6_off")));
    }

    IEnumerator Connect(string command){
        UnityWebRequest uwr = UnityWebRequest.Get(command);
        yield return uwr.SendWebRequest();
    }

    IEnumerator ConnectWater(string command, int sec){
        UnityWebRequest uwr = UnityWebRequest.Get(command);

        yield return uwr.SendWebRequest();
        yield return new WaitForSeconds(sec);
    }
}