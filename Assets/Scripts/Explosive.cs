using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int blockID= -1;
    void Start()
    {
        if(FindObjectOfType<SaveFile>()){
            print("Save retrieved");
            if(FindObjectOfType<SaveFile>().BrokenBlocks.Contains(blockID)){
                print(blockID + " block broken");
                Destroy(parent.gameObject);
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    [SerializeField] GameObject parent;
    public void onHit(){
        FindObjectOfType<CameraShake>().Shake(transform.localScale.x*0.2f , transform.localScale.x*0.3f );
        try{
            FindObjectOfType<SaveFile>().BrokenBlocks.Add(blockID);
            FindObjectOfType<SaveFile>().saveCache();
            }
        catch(Exception){print("no save");}
        Destroy(parent.gameObject);
        Destroy(gameObject);
        
    }

    }
