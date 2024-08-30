using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BloomLaunch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(cooldown <0f ) cooldown += Time.deltaTime;
        else {
            isOpen = true;
            GetComponent<Light2D>().enabled = true;}
    }

    void FixedUpdate(){
        if(hasPLayer){
            FindObjectOfType<PlayerController>().transform.position = transform.position;
        }
    }

    [SerializeField] float cooldown = 0.1f;
    [SerializeField] bool blooming = true;
    bool isOpen = true;
    bool hasPLayer = false;
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player") && isOpen && blooming){
            other.gameObject.GetComponent<PlayerController>().BloomFlower(gameObject);
            hasPLayer = true;
            
        }
    }

    public void launch(){
        isOpen = false;
        hasPLayer = false;
        cooldown = -2f;
        GetComponent<Light2D>().enabled = false;
        
    }

}
