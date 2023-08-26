using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueberryCollect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     collectTime = 1f;
    }

    public float collectTime;

    // Update is called once per frame

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Respawn")||other.gameObject.CompareTag("finish")){
            collectTime = -1;
        }
    }

    void Update()
    {
        collectTime+=Time.deltaTime;
    }
}
