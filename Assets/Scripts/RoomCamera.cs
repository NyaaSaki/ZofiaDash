using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Grid roomGrid;
    [SerializeField] GameObject Zofia;
    [SerializeField] GameObject Camera;
    [SerializeField] Vector3 CameraTarget;
    [SerializeField] float TransitionSpeed;

    [SerializeField] public bool isZoomed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3Int GetCell = roomGrid.WorldToCell(Zofia.transform.position);
        CameraTarget = roomGrid.GetCellCenterWorld(GetCell);
        
        if(isZoomed) {CameraTarget = Zofia.transform.position + new Vector3(1,2,-2);
         Camera.GetComponent<Camera>().orthographicSize = 6;
         } 
        else {Camera.GetComponent<Camera>().orthographicSize = 7.965f;}

        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, CameraTarget, TransitionSpeed);
    }
}
