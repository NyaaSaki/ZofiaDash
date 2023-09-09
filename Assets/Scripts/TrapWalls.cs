using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWalls : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform target;
    
    Vector3 TargetPosition;
    Vector3 StartPosition;
    bool trapped;
    void Start()
    {
        TargetPosition = target.position;
        StartPosition = transform.position;
    }

    public void resetTrap(){
        trapped = false;
        transform.position = StartPosition;
    }

    // Update is called once per frame
    public void TriggerTrap(){
        trapped = true;
        transform.position = StartPosition;
    }

    void Update()
    {
        
    }
}
