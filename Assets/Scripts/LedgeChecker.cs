using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeChecker : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsTouching = false;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"))) IsTouching = true;
        else IsTouching = false;
    }
}
