using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Grid roomGrid;
    [SerializeField] GameObject Zofia;
    [SerializeField] GameObject Camera;
    [SerializeField] Vector3 CameraTarget;
    [SerializeField] float TransitionSpeed;
    [SerializeField] public bool isZoomed;
    
    [SerializeField] Tilemap CorridorA;
    [SerializeField] Tilemap CorridorB;
    [SerializeField] public bool AdvancedCam;
    void Start()
    {
        
    }

    // Update is called once per frame
 

    Vector3 limitCam(Vector3Int cur,Tilemap Corridor){
        float lim = 0;
        CameraTarget = Zofia.transform.position + new Vector3(0,0,-2);

        //If Right room is corridor
        if(!Corridor.HasTile(cur + new Vector3Int(1,0))){
            lim = roomGrid.GetCellCenterWorld(cur).x;
            CameraTarget = new Vector3(Mathf.Min(CameraTarget.x,lim),CameraTarget.y,-2);
            }
        //If Left room is corridor
        if(!Corridor.HasTile(cur + new Vector3Int(-1,0))){
            lim = roomGrid.GetCellCenterWorld(cur).x;
            CameraTarget = new Vector3(Mathf.Max(CameraTarget.x,lim),CameraTarget.y,-2);
            }
        if(!Corridor.HasTile(cur + new Vector3Int(0,1))){
            lim = roomGrid.GetCellCenterWorld(cur).y;
            CameraTarget = new Vector3(CameraTarget.x,Mathf.Min(CameraTarget.y,lim),-2);
            }
        if(!Corridor.HasTile(cur + new Vector3Int(0,-1))){
            lim = roomGrid.GetCellCenterWorld(cur).y;
            CameraTarget = new Vector3(CameraTarget.x,Mathf.Max(CameraTarget.y,lim),-2);
            }
        return CameraTarget;
    }

    void Update()
    {
        Vector3Int GetCell = roomGrid.WorldToCell(Zofia.transform.position);

        if(CorridorA.HasTile(GetCell)){
            AdvancedCam = true;
            CameraTarget = limitCam(GetCell,CorridorA);
        }
        else if(CorridorB.HasTile(GetCell)){
            AdvancedCam = true;
            CameraTarget = limitCam(GetCell,CorridorB);
        }
        else {
            AdvancedCam = false;
            CameraTarget = roomGrid.GetCellCenterWorld(GetCell);
            }

        if(isZoomed) {CameraTarget = Zofia.transform.position + new Vector3(1,2,-2);
         Camera.GetComponent<Camera>().orthographicSize = 6;
         } 
        else {Camera.GetComponent<Camera>().orthographicSize = 7.965f;}

        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, CameraTarget, TransitionSpeed);
    }
}
