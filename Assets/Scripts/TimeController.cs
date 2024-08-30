using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject DayBG;
    [SerializeField] GameObject NightBG;
    [SerializeField] GameObject NightLight;
    [SerializeField] GameObject DayLight;
    [SerializeField] GameObject ZofiaTorch;

    [SerializeField] GameObject[] NightFeatures;

    [SerializeField] GlassFlip Flipper;
    

    [SerializeField] public bool Day = true;
    void Start()
    {
        TimeSwitch(Day);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TimeSwitch( bool isDay = true){
        Day = isDay;
        DayBG.SetActive(isDay);
        DayLight.SetActive(isDay);
        NightBG.SetActive(!isDay);
        NightLight.SetActive(!isDay);
        ZofiaTorch.SetActive(!isDay);
        Flipper.enabled = isDay;
        foreach (GameObject feature in NightFeatures)
        {
            feature.SetActive(!isDay);
        }
    }
}
