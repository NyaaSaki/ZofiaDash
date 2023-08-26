using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldShawarma : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public bool isCompleted = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(isCompleted){
            transform.position = player.transform.position + new Vector3(-0.05f,0.6f);
        }
    }
}
