using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] GameObject Item;
    [SerializeField] GameObject Camera;

    [SerializeField] SpeedRunTimer timer;

    void Start()
    {
        timer = FindObjectOfType<SpeedRunTimer>();
    }

    IEnumerator Next(){
        yield return new WaitForSecondsRealtime(5);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    // Update is called once per frame
    void Update()
    {   
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag=="Player"){
            timer.IsValid = false;
            Item.GetComponent<HoldShawarma>().player = other.gameObject;
            Item.GetComponent<HoldShawarma>().isCompleted = true;
            FindObjectOfType<GameSessionManager>().saveLevel();
            other.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;

            other.gameObject.GetComponent<Animator>().SetBool("IsCollect",true);
            Camera.GetComponent<Animator>().SetInteger("Room",-1);
            StartCoroutine("Next");
            GetComponent<AudioSource>().Play();
            GetComponentInChildren<ParticleSystem>().Play();
        }

    }
}
