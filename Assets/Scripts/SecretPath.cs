using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SecretPath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    Tilemap sprite;
    bool HasPLayer = false;
    
    private void OnTriggerEnter2D(Collider2D other) {
        
        if(other.gameObject.CompareTag("Player")) HasPLayer = true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) HasPLayer = false;
    }
    void FixedUpdate()
    {
        if(HasPLayer){
            if(sprite.color.a>0) sprite.color = sprite.color - new Color(0,0,0,Time.deltaTime*2);
        }
        else{
            if(sprite.color.a<1) sprite.color = sprite.color + new Color(0,0,0,Time.deltaTime);
        }
    }
}
