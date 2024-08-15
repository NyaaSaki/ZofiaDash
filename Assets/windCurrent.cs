using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class windCurrent : MonoBehaviour
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
        print(other.gameObject.name);
        if(other.gameObject.CompareTag("Player")) other.GetComponent<PlayerController>().GravUp = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) other.GetComponent<PlayerController>().GravUp = false;
    }
}
