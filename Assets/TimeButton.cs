using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite Day;
    [SerializeField] Sprite Night;
    [SerializeField] TimeController time;
    void Start()
    {
        time = FindObjectOfType<TimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")){
            time.TimeSwitch(!time.Day);
            switchSprite(time.Day);
            foreach (TimeButton button in FindObjectsOfType<TimeButton>())
            {
                print(button);
                button.switchSprite(time.Day);
            }
            
        }
    }

    public void switchSprite(bool day){
        if(day) GetComponent<SpriteRenderer>().sprite = Day;
        else GetComponent<SpriteRenderer>().sprite = Night;
    }
}
