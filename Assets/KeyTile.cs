using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class KeyTile : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject door;
    [SerializeField] Color32 doorTag;
    [SerializeField] Color32 disabled;

    [SerializeField] Sprite mid;
    [SerializeField] Sprite open;

    [SerializeField] Sprite buttonOn;
    [SerializeField] Sprite buttonFlicker;
    [SerializeField] Sprite buttonTriggered;
    [SerializeField] Sprite buttonOff;

    private float openTime = 1f;
    bool activated = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(activated == false){
            openTime +=0.01f;
            if(openTime > 3.2f) {
                openTime = 2f; 
                GetComponent<SpriteRenderer>().sprite = buttonFlicker;}

            else if(openTime>2.5f) { GetComponent<SpriteRenderer>().sprite = buttonOn; }
        }
        if(activated && openTime>-1f){
            openTime -= 0.1f;
        }
        if(openTime<0f) {
            door.GetComponent<SpriteRenderer>().sprite = open;
            GetComponent<SpriteRenderer>().sprite = buttonOff;
        }
    }

    public void onHit(){
        activated = true;
        door.GetComponent<SpriteRenderer>().sprite = mid; 
        door.GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = buttonTriggered;
    }
}
