using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudScroll : MonoBehaviour
{
    // Start is called before the first frame update
    RawImage img;
    [SerializeField] float speed = 1f;
    [SerializeField] GameObject Cam;
    void Start()
    {
        img = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        img.uvRect = new Rect(new Vector2(0.4f + Cam.transform.position.x/400 , 0.03f + Cam.transform.position.y/450), img.uvRect.size);
    }
}
