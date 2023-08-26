using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] int tgtroom = 0;
    [SerializeField] GameObject manager; 
    Animator controller;
    GameObject player;
    Vector2 Vel;
    void Start()
    {   
        controller = manager.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  

    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("entered room");
        if(other.gameObject.tag == "Player"){
            controller.SetInteger("Room",tgtroom);
        }
    }

    
}
