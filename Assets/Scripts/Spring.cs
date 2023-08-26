using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject sign;
    void Start()
    {
        sign.GetComponent<SpriteRenderer>().enabled= false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            sign.GetComponent<SpriteRenderer>().enabled= true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            sign.GetComponent<SpriteRenderer>().enabled= false;
        }
    } 
     
}
