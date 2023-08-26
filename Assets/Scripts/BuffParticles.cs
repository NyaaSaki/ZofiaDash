using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffParticles : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject core;
    PlayerController pc;
    [SerializeField] string attr;
    void Awake()
    {
        pc = core.GetComponent<PlayerController>();
    }

    void part(bool arg){
        if(arg) GetComponent<ParticleSystem>().Play();
        else GetComponent<ParticleSystem>().Stop();
    }
    // Update is called once per frame
    void Update()
    {   if(attr == "dash") part(pc.canDash);
        else if(attr == "jump") part(pc.doubleJump);
    
    }
}
