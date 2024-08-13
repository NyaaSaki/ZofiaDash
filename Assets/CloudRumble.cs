using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CloudRumble : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<RawImage>();
        targetV = img.uvRect.position;
    }

    // Update is called once per frame
    [SerializeField] float MaxX;
    [SerializeField] float MaxY;
    [SerializeField] float min = 0.1f;

    RawImage img;
    [SerializeField] Vector2 target;
    [SerializeField] Vector2 targetV;
    [SerializeField] float smoothV = 0;
    void Update()
    {
        if( (targetV - target).magnitude < min ){
            targetV =new  Vector2(Random.Range(-MaxX , +MaxX) , Random.Range(-MaxY , +MaxY));
        }
        else{
            smoothV = (smoothV*99 + (targetV - target).magnitude) /100;
            target = Vector2.MoveTowards(target , targetV , smoothV/200);
            img.uvRect = new Rect(target,img.uvRect.size);
        }
    }
}
