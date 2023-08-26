using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimePath : MonoBehaviour
{
    // Start is called before the first frame update

    public float pathRate;
    
    [SerializeField] public float freq;
    [SerializeField] Transform goal;
    [SerializeField] bool canFlip = true;

    [SerializeField] bool isPlatform = false;
    Vector3 starting;

    public Vector3 targ;
    public bool dir;

    float t = 0;
    void Start()
    {
        starting = transform.position;
    }

    // Update is called once per frame

    float getrate(float x){
        float f =   Mathf.Sin(Mathf.PI * x * freq);
        //Debug.Log(" sine: " + f);

        return (1 + Mathf.Atan(4*f)/1.3258f)/2;
    }

    void Update()
    {   if(freq ==0) return;

        t+= Time.deltaTime;
        dir = (Mathf.PI*freq*Mathf.Cos(Mathf.PI * t * freq) <0);
        targ=  starting + (getrate(t) * (goal.position-starting));
        
        if(isPlatform){
            float Reqforce = -Mathf.Pow(Mathf.PI*freq,2)*Mathf.Sin(Mathf.PI*freq*t);
            GetComponent<Rigidbody2D>().velocity = 2*Reqforce*(starting-goal.position).normalized;
        }
        else transform.position = targ; 


        if(canFlip){
            if(dir) GetComponent<SpriteRenderer>().flipX = true;
            else GetComponent<SpriteRenderer>().flipX = false;
        }
        
        //Vector3.MoveTowards(transform.position, targ, 0.1f);

        //Debug.Log(targ);
    }


    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player") && isPlatform){
            other.gameObject.GetComponent<PlayerController>().onPlatform = false;

            }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("collided");
         if(other.gameObject.CompareTag("Player") && isPlatform){
            other.gameObject.GetComponent<PlayerController>().platform = GetComponent<Rigidbody2D>();
            other.gameObject.GetComponent<PlayerController>().onPlatform = true;
            other.gameObject.GetComponent<PlayerController>().airtime = 0f;

        }
    }
}
