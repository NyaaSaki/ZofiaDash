using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectFollow : MonoBehaviour
{
    // Start is called before the first frame update
    bool isPicked = false;
    [SerializeField] bool quaso = false;
    public bool unlocked = false;
    public Transform portal;

    public bool MovingBase = false;
    public Transform DynamicBase;
    GameObject targ;
    void Start()
    {
        spawn = transform.position;
    }

    Vector3 spawn;

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            if(isPicked == false) GetComponent<AudioSource>().Play();
            isPicked = true;
            targ = other.gameObject;
            Debug.Log("collected");
            
            }

        if(other.CompareTag("Respawn")){
            Debug.Log("obtained");
        }

    }
    void collect(){
        Destroy(gameObject);
    }

    public void unlock(){
            GetComponent<AudioSource>().Play();
            Invoke("StartCollect",0.5f);
            unlocked = true;
    }

    bool isTaken = false;
    void Update()
    {   
        if(MovingBase){
            spawn = DynamicBase.position;
        }
        if(unlocked) {
            transform.position = Vector3.MoveTowards(transform.position,portal.position,Vector3.Distance(transform.position,portal.position)*10f*Time.deltaTime);
            return;
        }
        
        if(FindObjectOfType<death>().isDying && !quaso){
            isPicked = false;

        }

        if(isPicked == true){
            float minDist = (Vector3.Distance(transform.position,targ.transform.position) *0.5f) -(quaso?1f:0.5f);
            if(minDist > 0f) transform.position = Vector3.MoveTowards(transform.position,targ.transform.position +(quaso? new Vector3(0f,1f):new Vector3(0f,0f)),minDist*5f*Time.deltaTime);
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position,spawn,Vector3.Distance(transform.position,spawn)*10f*Time.deltaTime);
        }

    

        if(FindObjectOfType<BlueberryCollect>().collectTime <0 && !isTaken && isPicked && !quaso){
        FindObjectOfType<GameSessionManager>().onPick();
        Invoke("StartCollect",0.5f);
        isTaken = true;
        }
    }

    void StartCollect(){


            
            GetComponent<Animator>().SetBool("Collected",true);
            GetComponent<AudioSource>().Play();
            Invoke("collect",1f);
    }
}
