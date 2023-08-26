using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class QuasoGate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [SerializeField] Sprite unlocked;
    [SerializeField] GameObject quasoItem;
    bool opened = false;
    public float DisToKey;
    // Update is called once per frame
    void Update()
    {
        if(opened) return;
        DisToKey = Vector3.Distance(transform.position, quasoItem.transform.position);
        if( DisToKey < 8 && !opened){
            Debug.Log("Quaso Updated");
            quasoItem.GetComponent<CollectFollow>().unlock();
            quasoItem.GetComponent<CollectFollow>().portal = transform;
            opened = true;
            Invoke("openDoor",1.5f);
            opened = true;
        }
    }
    void openDoor(){
        GetComponent<SpriteRenderer>().sprite = unlocked;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player") && opened){
            FindObjectOfType<GameSessionManager>().NextLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {

    }
}
