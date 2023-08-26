using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeReference] bool onStart;
    GameSessionManager manager;

    void Awake(){
        manager = FindObjectOfType<GameSessionManager>();
    }
    void Start()
    {
        if(!manager.isMuted){
            GetComponent<AudioSource>().Play();
        }
        if(manager.isMuted){
            GetComponent<AudioSource>().Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.isMuted){
            GetComponent<AudioSource>().Stop();
        }
    }

}
