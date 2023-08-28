using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] float speed;
    public Vector3 targ;
    
    private Rigidbody2D player;

    public int state = 0;

    // wait - 0 , move - 1 , restore - 2
    void Start()
    {
        targ = pointB.position;
        targDir = calcDir();
        transform.position = pointA.transform.position;
    }

    // Update is called once per frame
    public void OnTriggerStay2D(Collider2D other){

        if(other.CompareTag("Player")){
            
            other.GetComponent<PlayerController>().onPlatform = true;
            other.GetComponent<PlayerController>().platform = GetComponent<Rigidbody2D>();
            if(state ==0 && speed >1) {state = 1;traveled = 0;buffer = -0.5f;GetComponent<AudioSource>().Play();}
            

            }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.CompareTag("Player")){ 
         other.GetComponent<PlayerController>().onPlatform = false;
         other.GetComponent<PlayerController>().airtime = 0f;
        }
    }

    private Vector2 calcDir(){
        return (targ - transform.position).normalized;
    }

    public Vector3 targDir;
    [SerializeField] float TravelDist;
    public float traveled;
    public float buffer = 0f;

    [SerializeField] float playerCoeff;

    void reset(){
        state = 0;
        transform.position = pointA.position;
        targ = pointB.position;
        targDir = calcDir();
        transform.position = pointA.transform.position;

    }

    void Update(){
        if(FindObjectOfType<death>().isDying){
            state = 0;
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity*0.0f;
            Invoke("reset",0.2f);
        }
    }

    void FixedUpdate()
    {   
        buffer += Time.deltaTime;
        if(buffer > 0f)
        {
        if(state ==1) {GetComponent<Rigidbody2D>().velocity = targDir*speed; traveled +=Time.deltaTime;}
        if(state ==2) {GetComponent<Rigidbody2D>().velocity = targDir * speed*0.2f ; traveled -= Time.deltaTime*0.2f;}
        
        if(Vector2.Distance(pointA.transform.position,gameObject.transform.position)<0.1f){ 
            targ = pointB.position;
            targDir = calcDir();
            
            
            if(state == 2) {
                state = 0;
                buffer = -0.2f;
                GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity*0.0f;
                }
            }
        if(Vector2.Distance(pointB.transform.position,gameObject.transform.position)<0.5f){ 
            targ = pointA.position;
            targDir = calcDir();
           
            if(state ==1){
                pointB.gameObject.GetComponent<AudioSource>().Play();
                state =2;
                buffer = -2f;
                GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity*0.0f;
                }
            }
        
        }
        //if(hasPlayer){player.velocity+=new Vector2(GetComponent<Rigidbody2D>().velocity.x*playerCoeff,0f);}
    }
}
