using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeSnap : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] LedgeChecker top;
    [SerializeField] LedgeChecker bottom;
    Rigidbody2D Phys;
    
    public float SnapCooldown = 0f;
    void Start()
    {
        Phys = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SnapCooldown += Time.deltaTime;
        if(bottom.IsTouching && !top.IsTouching){
            if(SnapCooldown>0f){
                //transform.position+=new Vector3(0f,0.1f,0f);
                //if(Phys.velocity.y < 0) Phys.velocity = new Vector2(Phys.velocity.x,0f);
                SnapCooldown = -0.5f;
            }
            
        }
    }
}
