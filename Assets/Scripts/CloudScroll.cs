using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloudScroll : MonoBehaviour
{
    // Start is called before the first frame update
    RawImage img;
    [SerializeField] float speed = 1f;
    void Start()
    {
        img = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        img.uvRect = new Rect(img.uvRect.position + new Vector2(speed*Time.deltaTime,0f),img.uvRect.size);
    }
}
