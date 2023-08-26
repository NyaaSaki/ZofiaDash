using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ActivatePlatform : MonoBehaviour
{
    [SerializeField] MovingPlatform master;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other) {
        //master.checkTrigger(other);
    }
}
