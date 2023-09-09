using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FlipCheck : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] List<SafetyHeadphones> Checklist;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<SafetyHeadphones>() != null){
            Checklist.Add(other.GetComponent<SafetyHeadphones>());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.GetComponent<SafetyHeadphones>() != null && Checklist.Contains(other.GetComponent<SafetyHeadphones>())){
            Checklist.Remove(other.GetComponent<SafetyHeadphones>());
        }
    }
    public void OffCheck(){
        if(Checklist.Count !=0) foreach (SafetyHeadphones item in Checklist)
        {
            item.isTouching = false;
        }
        GetComponent<TilemapCollider2D>().enabled = false;
        Checklist.Clear();
        
    }
}
