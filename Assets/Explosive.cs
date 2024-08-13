using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [SerializeField] GameObject parent;
    public void onHit(){
        FindObjectOfType<CameraShake>().Shake(transform.localScale.x*0.2f , transform.localScale.x*0.3f );
        Destroy(parent.gameObject);
        Destroy(gameObject);
        
    }

    }
