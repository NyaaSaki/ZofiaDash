using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWay : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Interactable Door1;
    [SerializeField] Interactable Door2;
    [SerializeField] GameObject Zofia;
    void Start()
    {
        
    }

    // Update is called once per frame
    bool jump = false;
    void Update()
    {
        if(jump){
            FindObjectOfType<RoomCamera>().jumpCam();
            jump = false;
        }
        if(Input.GetKeyDown(KeyCode.F)){

            if(Door1.IsActive){
                Move(true);
            }
            else if(Door2.IsActive){
                Move(false);
            }

        }
    }
    void Move(bool isEntry){
        Zofia.transform.position = (isEntry?Door2.transform.position:Door1.transform.position)+ Vector3.down*0.5f;
        jump = true;
    }
}
