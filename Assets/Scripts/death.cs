using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class death : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider2D coll;
    Rigidbody2D rb;
    [SerializeField] GameObject onDeath;
    [SerializeField] GameObject onRespawn;
    [SerializeField] Vector3 respawn;
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        respawn = gameObject.transform.position;
    }

    public bool isDying = false;

    // Update is called once per frame
    void Update()
    {
        if(isDying) {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        else {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
    }
    void reload(){
        gameObject.transform.position = respawn;
        rb.isKinematic = false;
        isDying = false;
        GetComponent<SpriteRenderer>().enabled = true;
        onRespawn.GetComponent<ParticleSystem>().Play();
        GetComponent<BoxCollider2D>().enabled = true;
    }
    

    public void Kill(){
            GetComponent<AudioSource>().Play();
            isDying = true;
            onDeath.GetComponent<ParticleSystem>().Play();
            GetComponent<BoxCollider2D>().enabled = false;
            Invoke("reload",0.3f);
            FindObjectOfType<GameSessionManager>().onDie();
            //Debug.Log("dying");
            isDying = true;
            GetComponent<SpriteRenderer>().enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "water" && !isDying){
            GetComponent<AudioSource>().Play();
            isDying = true;
            onDeath.GetComponent<ParticleSystem>().Play();
            GetComponent<BoxCollider2D>().enabled = false;
            Invoke("reload",0.3f);
            //Debug.Log("dying");
            isDying = true;
            GetComponent<SpriteRenderer>().enabled = false;
            FindObjectOfType<GameSessionManager>().onDie();
        }

        if(other.gameObject.tag == "Respawn"){
            //Debug.Log("Respawned!");
            respawn = transform.position;

        }
    }
    
}
