using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushDetect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SafetyHeadphones left;
    [SerializeField] SafetyHeadphones right;
    [SerializeField] SafetyHeadphones up;
    [SerializeField] SafetyHeadphones down;

    public bool isUp;
    public bool isDown;
    public bool isLeft;
    public bool isRight;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isUp = up.isTouching;
        isDown = down.isTouching;
        isRight = right.isTouching;
        isLeft = left.isTouching;
        if(isRight && isLeft && !gameObject.GetComponent<death>().isDying ) gameObject.GetComponent<death>().Kill();
        if(isUp && isDown && !gameObject.GetComponent<death>().isDying) gameObject.GetComponent<death>().Kill();
    }

    

}
