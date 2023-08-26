using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject target;
    [SerializeField] float maxLen;
    [SerializeField] float range;
    
    public Vector3 startpos;
    void Start()
    {
        startpos = transform.position;
        target = FindObjectOfType<PlayerController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {   
        if((transform.position - target.transform.position).magnitude<range){
        transform.position = Vector3.MoveTowards(transform.position,target.transform.position,maxLen*Time.deltaTime);
        }
        if(target.GetComponent<death>().isDying)  transform.position = startpos;
    }

}
