using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragQuaso : MonoBehaviour , IDragHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
      void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;    
    }
}
