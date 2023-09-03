using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragQuaso : MonoBehaviour , IDragHandler
{
    // Start is called before the first frame update
    SaveFile save;
    [SerializeField] int Room;
    [SerializeField] bool IsQuaso;
    
    private void Awake() {
      save = FindObjectOfType<SaveFile>();  
    }
    void Start()
    {
        GetComponent<Image>().enabled = false;
    }

    // Update is called once per frame

    void Update()
    {   
      if(GetComponent<Image>().enabled){return;}
      if(IsQuaso){
        if(save.CheckCollectible(Room,true)) GetComponent<Image>().enabled = true;
      }
      else {
        if(save.CheckLevel(Room)) GetComponent<Image>().enabled = true;
      }
    }
      void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;    
    }
}
