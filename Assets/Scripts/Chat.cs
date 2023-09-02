using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chat : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject DialogeBox;
    [SerializeField] TextMeshProUGUI ActorText;
    [SerializeField] TextMeshProUGUI ChatBox;
    [SerializeField] string ActorName = "Zofia";
    [SerializeField] string ChatContent = "wow saki u really did forget to put some content here didnt you";
    [SerializeField] RoomCamera cam;
    [SerializeField] GameObject Zofia;
    bool canClose;
    void Start()
    {
        canClose = false;
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
        if(cam.isZoomed && canClose && Input.anyKeyDown)
        {DialogeBox.SetActive(false);
        cam.isZoomed = false;
        Zofia.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Zofia.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        canClose = false;
        }
    }

    void AllowClose(){

        canClose = true;
    }

    float cooldown = -1f;
    private void OnTriggerEnter2D(Collider2D other) {

        if(cooldown > 0 || !other.CompareTag("Player")) return; 
        else {
        canClose = false;
        DialogeBox.SetActive(true);
        ActorText.text = ActorName;
        ChatBox.text = ChatContent;
        cam.isZoomed = true;
        Zofia.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        Invoke("AllowClose",3f);
        cooldown = 20;
        }
    }
}
