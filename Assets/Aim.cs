using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aim : MonoBehaviour
{
    // Start is called before the first frame update

    private Camera mainCam;
    private Vector3 MousePos;

    [SerializeField] LineRenderer laser;
    [SerializeField] GameObject crossair;
    

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        zofia = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        getAim();
        ChargeShot();

        
    }
    [SerializeField] float laserCharge = -0.2f;
    public GameObject zofia;
    [SerializeField] float DischargeRate = 0.1f;
    [SerializeField] Color32 Active;
    [SerializeField] Color32 noAmmo;
    private bool ChargingShot = false;
    void ChargeShot(){
        
        if(Input.GetMouseButton(0) && laser.enabled == false){
            crossair.GetComponent<SpriteRenderer>().enabled = true;

            if(zofia.GetComponent<PlayerController>().canShoot == false && laser.enabled == false){
                
                crossair.GetComponent<SpriteRenderer>().color = noAmmo;
            }
            else{
                
                crossair.GetComponent<SpriteRenderer>().color = Active;
                if(laserCharge<0f) laserCharge = 0.3f;
                else if(laserCharge<1f) laserCharge += 0.05f;
                ChargingShot = true;
            }
            
            
        }
        //shooting
        else{
            crossair.GetComponent<SpriteRenderer>().enabled = false;
            if( ChargingShot ){
                Shoot();
                laser.enabled = true;
                zofia.GetComponent<PlayerController>().pulse(transform.right * setCrossair() * 0.8f);
                GetComponent<AudioSource>().Play();
                ChargingShot = false;
                }
            
            if(laserCharge < 0f) laser.enabled = false;
            else laserCharge -= DischargeRate;
        }

        crossair.transform.localScale = new Vector3(0.7f , setCrossair() * 0.7f/3 ,1);
    }

    int setCrossair(){
        if(laserCharge<0.3) return 0;
        else if(laserCharge<0.6) return 1;
        else if(laserCharge<0.9) return 2;
        else return 3;
    }

    void getAim(){
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        MousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = MousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler( 0 , 0 , rotZ);
    }
    
    void Shoot(){
        laser.SetPosition(0,transform.position);
        if (Physics2D.Raycast(transform.position, transform.right, 120f ,LayerMask.GetMask("Ground"))){
            
            RaycastHit2D _hit = Physics2D.Raycast(transform.position, transform.right , 120f ,LayerMask.GetMask("Ground"));
            print( _hit.transform.gameObject.name);

            if (_hit.transform.gameObject.CompareTag("target")) _hit.transform.gameObject.GetComponent<KeyTile>().onHit();
            if (_hit.transform.gameObject.CompareTag("bomb")) _hit.transform.gameObject.GetComponent<Explosive>().onHit();
            laser.SetPosition(1,_hit.point);
        }
        else{
            laser.SetPosition(1 , transform.position + 120f * transform.right);
        }
        
    }
}
