using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GlassFlip : MonoBehaviour
{
    // Start is called before the first frame update
    Color32 ColorA;
    Color32 ColorB;
    [SerializeField] Color32 Inactive;
    [SerializeField] GameObject BlockA;
    [SerializeField] GameObject BlockB;
    [SerializeField] Tilemap FlipSound;
    [SerializeField] Grid roomGrid;
    [SerializeField] GameObject Zofia;

    bool IsA = false;
    float clock = 0f;
    void Start()
    {
        ColorA = BlockA.GetComponent<Tilemap>().color;
        ColorB = BlockB.GetComponent<Tilemap>().color;
    }

    [SerializeField] float FlipTime = 2f;
    // Update is called once per frame
    void Update()
    {   
        bool playSound = false;
        Vector3Int GetCell = roomGrid.WorldToCell(Zofia.transform.position);
        if(FlipSound.HasTile(GetCell)){
            playSound = true;
        }
        if(clock < FlipTime){
            clock += Time.deltaTime;
            return;
        }
        IsA = !IsA;
        if(IsA){
            BlockA.GetComponent<Tilemap>().color = ColorA;
            BlockA.GetComponent<TilemapCollider2D>().enabled = true;
            BlockB.GetComponent<Tilemap>().color = Inactive;
            BlockB.GetComponent<FlipCheck>().OffCheck();
            GetComponent<AudioSource>().pitch = 0.6f;
            if(playSound) GetComponent<AudioSource>().Play();
        }
        else{
            BlockA.GetComponent<Tilemap>().color = Inactive;
            BlockA.GetComponent<FlipCheck>().OffCheck();
            BlockB.GetComponent<Tilemap>().color = ColorB;
            BlockB.GetComponent<TilemapCollider2D>().enabled = true;
            GetComponent<AudioSource>().pitch = 1.2f;
            if(playSound) GetComponent<AudioSource>().Play();
        }
        clock = 0;
    }
}
