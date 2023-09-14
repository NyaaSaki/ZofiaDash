using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShawarmaTruck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            transform.localScale = new Vector3(transform.localScale.x,0.9f,1);
            transform.position = transform.position + new Vector3(0,-0.1f,0);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
                if(other.CompareTag("Player")){
            transform.localScale = new Vector3(transform.localScale.x,1,1);
        }
    }
}
