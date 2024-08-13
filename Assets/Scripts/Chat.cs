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
    
    [SerializeField] GameObject skipchat;
    [SerializeField] string ActorName = "Zofia";
    [SerializeField] string ChatContent = "wow saki u really did forget to put some content here didnt you";
    [SerializeField] RoomCamera cam;
    [SerializeField] GameObject Zofia;

    [SerializeField] Vector3 CamTarget;
    [SerializeField] bool hasTarget;
    private bool seen = false;

    bool canClose;
    void Start()
    {
        canClose = false;
        skipchat.SetActive(false);
        DialogeBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;

        if(canClose && Input.anyKey)
        {DialogeBox.SetActive(false);
        cam.isZoomed = false;
        Zofia.GetComponent<PlayerController>().canMove = true;
        canClose = false;
        }
    }

    void AllowClose(){
        skipchat.SetActive(true);
        canClose = true;
    }

    float cooldown = -1f;
    private void OnTriggerEnter2D(Collider2D other) {

        if(seen || !other.CompareTag("Player")) return; 
        else {
        seen= true;
        skipchat.SetActive(false);
        canClose = false;
        DialogeBox.SetActive(true);
        ActorText.text = ActorName;
        ChatBox.text = ChatContent;
        if(hasTarget) cam.LookAt(CamTarget);
        else cam.isZoomed = true;
        Zofia.GetComponent<PlayerController>().canMove = false;
        Invoke("AllowClose",2f);
        cooldown = 20;
        }
    }
}
