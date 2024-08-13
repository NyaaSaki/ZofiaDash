using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAmmo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    [SerializeField] PlayerController zofia;
    // Update is called once per frame
    void Update()
    {
        GetComponent<SpriteRenderer>().enabled = zofia.canShoot;
    }
}
