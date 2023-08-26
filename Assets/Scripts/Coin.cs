using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem collect;
    SpriteRenderer self;
    [SerializeField] string bufftype;
    bool canCollect = true;
    void Awake(){
        collect = GetComponentInChildren<ParticleSystem>();
        self = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        self.gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    void activate(){
        canCollect = true;
    }

    void deactivate(){
        self.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        self.gameObject.GetComponent<SpriteRenderer>().enabled = canCollect;
    }


    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("not player");
        if(other.gameObject.tag == "Player"){
            Debug.Log("collected");
        if(canCollect){

        other.gameObject.GetComponent<PlayerController>().collect(bufftype);
        GetComponent<AudioSource>().Play();
        collect.Play();
        canCollect = false;
        Invoke("activate",0.5f);
        
        }
        
        
        }
    }
}
