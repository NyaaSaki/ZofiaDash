using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GhostButtonPress : MonoBehaviour
{
    [SerializeField] List<SlimePath> ActivateBlocks;
    [SerializeField] GameObject background;

    [SerializeField] Color32 ActiveColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        foreach (SlimePath block in ActivateBlocks)
        {
            block.freq = 0.375f;
            if(block.gameObject.GetComponent<Tilemap>()!=null) 
                {block.gameObject.GetComponent<Tilemap>().color = ActiveColor;}
        }
        background.SetActive(true);
        background.GetComponent<AudioSource>().Play();
        Destroy(gameObject);
    }
}
