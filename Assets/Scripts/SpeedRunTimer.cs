using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedRunTimer : MonoBehaviour
{
    // Start is called before the first frame update

    public float timer = 0;
    [SerializeField] TextMeshProUGUI clock;
    [SerializeField] Color32 invalid;
    Color32 baseColor;
 
    
    public bool IsValid = true;
    public bool isWaiting = true;
    void Start()
    {
        timer = 0;
        baseColor = clock.color;
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.anyKeyDown) isWaiting = false;
        if(IsValid && !isWaiting){        
            timer += Time.deltaTime;
            string m = Mathf.FloorToInt(timer/60).ToString("00");
            string s = Mathf.FloorToInt(timer%60).ToString("00");
            string mss= Mathf.FloorToInt((timer%1)*100).ToString("00");
            clock.text = string.Format("{0}:{1}:{2}", m , s , mss);
        }

    }

    public void invalidate(){
        clock.text = "Assist Mode";
        clock.color = invalid;
        IsValid = false;
    }

    public void resetClock(){
        clock.color = baseColor;
        clock.text = "00:00:00";
        IsValid = true;
        isWaiting = true;
        timer = 0;
    }
}
