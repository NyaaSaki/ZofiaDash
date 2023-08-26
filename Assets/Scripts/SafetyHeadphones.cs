using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyHeadphones : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isTouching;
    [SerializeField] bool isSide;
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other) {
        //Debug.Log(other.gameObject.layer + "entered");
        if(other.gameObject.layer == 3 && !other.isTrigger){
            isTouching = true;
        }
        if(isSide && other.name.CompareTo("OneWay")==0) isTouching = false;
    }

    private void OnTriggerExit2D(Collider2D other) {
        //Debug.Log(other.gameObject.layer + "left");
        if(other.gameObject.layer == 3 && !other.isTrigger){
            isTouching = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
