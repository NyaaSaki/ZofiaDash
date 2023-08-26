using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpring : MonoBehaviour
{
    [SerializeField] Vector3 targ;
    // Start is called before the first frame update
    [SerializeField] Sprite active;
    [SerializeField] Sprite inactive;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void deactivate(){
        GetComponent<SpriteRenderer>().sprite = active;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            other.gameObject.transform.position = new Vector3(other.transform .position.x,transform.position.y + 0.5f,0);
            other.gameObject.GetComponent<Rigidbody2D>().velocity = targ;
            other.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(targ,ForceMode2D.Impulse);
            other.gameObject.GetComponent<PlayerController>().airtime= -0.5f;
            GetComponent<SpriteRenderer>().sprite = inactive;
            Invoke("deactivate",0.2f);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")) other.gameObject.GetComponent<PlayerController>().coyote= 2f;
    }
}
