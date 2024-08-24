using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

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
    [SerializeField] Tilemap CorridorC;
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

    [SerializeField] public float stall = 0f;

    public void LookAt(Vector3 loc){
        stall = 3f;
        CameraTarget = loc;
    }

    void movecam(){
        Camera.transform.position = Vector3.MoveTowards(Camera.transform.position, CameraTarget, TransitionSpeed);
    }

    public void jumpCam(){
        Camera.transform.position = CameraTarget;
    }


    void Update()
    {   
        if(stall>0f){
            stall -= Time.deltaTime;
            movecam();
            return;
        }
        
        Vector3Int GetCell = roomGrid.WorldToCell(Zofia.transform.position);
        if(CorridorA&&CorridorB&&CorridorC){
            if(CorridorA.HasTile(GetCell)){
            AdvancedCam = true;
            CameraTarget = limitCam(GetCell,CorridorA);
        }
            else if(CorridorB.HasTile(GetCell)){
            AdvancedCam = true;
            CameraTarget = limitCam(GetCell,CorridorB);
        }

        else if(CorridorC.HasTile(GetCell)){
            AdvancedCam = true;
            CameraTarget = limitCam(GetCell,CorridorC);
        }
            else {
            AdvancedCam = false;
            CameraTarget = roomGrid.GetCellCenterWorld(GetCell);
            }
        }
        else {
            AdvancedCam = false;
            CameraTarget = roomGrid.GetCellCenterWorld(GetCell);
            }


    
        if(isZoomed) {CameraTarget = Zofia.transform.position + new Vector3(1,2,-2);
         Camera.GetComponent<Camera>().orthographicSize = 5.4f;
         } 
        else {Camera.GetComponent<Camera>().orthographicSize = 7.965f;}

        movecam();
    }
}
