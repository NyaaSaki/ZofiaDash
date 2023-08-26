using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]Vector2 moveInput;
    
    Rigidbody2D Phys;
    BoxCollider2D coll;
    [SerializeField] float grav = 3.5f;
    Animator anim;


    [SerializeField] float ctime= 0.5f;
    [SerializeField]public float coyote;
    [SerializeField]float jumpBuffer;
    [SerializeField]BoxCollider2D gnd;

    void jumptick(){
        if(gnd.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
        coyote = 0;
        //canDash = true;
        anim.SetBool("InAir",false);
        }
        else anim.SetBool("InAir",true);
        coyote += Time.deltaTime;
        jumpBuffer += Time.deltaTime;
        dashtime += Time.deltaTime;
        airtime += Time.deltaTime;
    }


    // Particle Systems
    [SerializeField] GameObject effDash;
    
    


    void Awake() {
        Phys = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }
    void Start()
    {   
        coyote = 0.1f;
        jumpBuffer = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
       Running();
       jumpcut();
       jumptick();

       if(Input.GetKeyDown(KeyCode.E)) dash();
    }

    public bool onPlatform = false;
    public Rigidbody2D platform;
    private void checkPlatform()
    {

    }
    [SerializeField] GameObject back;
    void turn(){

        bool isMoving = Mathf.Abs(moveInput.x) > Mathf.Epsilon;
        if(isMoving && Mathf.Abs(Phys.velocity.x)>0.2){ 
            transform.localScale = new Vector2(Mathf.Sign(Phys.velocity.x),1);
            //back.transform.localScale = new Vector2(Mathf.Sign(Phys.velocity.x),1);
            anim.SetBool("IsWalk",true);
            }
        else anim.SetBool("IsWalk",false);

        if(!isMoving && moveInput.y<-Mathf.Epsilon) anim.SetBool("IsCrouch",true);
        else anim.SetBool("IsCrouch",false);
    }

    public void setcoyote(){
        coyote = 1f;
    }
    public bool doubleJump = false;


    [SerializeReference] GameObject jumpsound;
    [SerializeField] float jumpcap;
    void jumpcut(){


        if((jumpBuffer < 0.2f && coyote < ctime)){
            isJumping = true;
            if(Phys.velocity.y > jumpcap) Phys.AddForce(jumpforce * Vector2.up*0.4f, ForceMode2D.Impulse);
            else Phys.AddForce(jumpforce * Vector2.up, ForceMode2D.Impulse);
            jumpBuffer = 1f;
            Invoke("setcoyote",0.1f);
            jumpsound.GetComponent<AudioSource>().pitch = 1;
            jumpsound.GetComponent<AudioSource>().Play();
        }
        else if(doubleJump && !isJumping && jumpBuffer < 0.1f){
            if(Phys.velocity.y <0.5) Phys.velocity = new Vector2(Phys.velocity.x,0.5f);
            Phys.AddForce(jumpforce*0.9f * Vector2.up, ForceMode2D.Impulse);
            Debug.Log("double jumped");
            jumpBuffer = 1f;
            doubleJump = false;
            isJumping = true;
            jumpsound.GetComponent<AudioSource>().pitch = 1.3f;
            jumpsound.GetComponent<AudioSource>().Play();
        }

        if(Phys.velocity.y <0){
        Phys.gravityScale = grav * 3;
       }
        else if(Phys.velocity.y < 0.1 && Phys.velocity.y > -0.1){
        Phys.gravityScale = grav * 0.2f;
       }
       else {Phys.gravityScale = grav;}

       if(Input.GetKeyUp(KeyCode.Space)){
            onJumpUp();
       }
    }


    [SerializeField] float acc = 0.1f;
    [SerializeField] float decc = 0.5f;
    [SerializeField] float Speed = 10f; 
    [SerializeField] float velpower = 1.3f;
    [SerializeField] float jumpforce = 10f;
    public float dashtime = 10f;
    public float airtime = 10f;


    void Running(){
        Vector2 tgtSpeed ;

        if(Phys.velocity.y < 0.1 && Phys.velocity.y > -0.1 && !gnd.IsTouchingLayers(LayerMask.GetMask("Ground"))){
        tgtSpeed = moveInput*Speed*1.1f;
        }
        else tgtSpeed = moveInput*Speed;

        if(moveInput.y<0) transform.localScale = new Vector3(1,0.9f,1);
        else transform.localScale = new Vector3(1,1f,1);

        if(onPlatform){
            tgtSpeed += new Vector2(platform.velocity.x,0f);
            if(moveInput.y< -Mathf.Epsilon && platform.velocity.y>=0&& Phys.velocity.y>Mathf.Epsilon) {
                Phys.velocity = new Vector2(Phys.velocity.x,platform.velocity.y);
                Phys.gravityScale = grav*100;
            }
            else Phys.gravityScale = grav;
        }

        float forceNeeded = tgtSpeed.x - Phys.velocity.x;
        float tdecc = decc;
        if(moveInput.y< -Mathf.Epsilon) tdecc = decc*2;



        anim.SetBool("IsWalk",true);
        bool isAcc = (Mathf.Abs(moveInput.x) > Mathf.Epsilon);
        
        if(Phys.velocity.y < -termV) Phys.velocity = new Vector2(Phys.velocity.x,-termV);
        if(dashtime < 0.3f && Phys.velocity.y < 0.1f) Phys.velocity = new Vector2(Phys.velocity.x,0.1f);
        Phys.AddForce( Mathf.Pow((Mathf.Abs(forceNeeded) * (isAcc?acc:(airtime<0.2f?10:tdecc))) ,velpower)* Time.deltaTime * Vector2.right *Mathf.Sign(forceNeeded) );
        back.transform.localPosition = new Vector3(Mathf.Clamp(-Mathf.Abs(Phys.velocity.x)*0.03f,-0.06f,0f),0f,0f);
        turn();
    }

    void OnMove(InputValue inp){
        moveInput = inp.Get<Vector2>();

        
    }
    bool isJumping = false;

    void OnJump(InputValue inp){

        jumpBuffer = 0.05f;

    }

    public void collect(string buff){
        if(buff == "dash") canDash = true;
        else if(buff == "jump") doubleJump = true;
    }




    void onJumpUp(){   
        if(isJumping && Phys.velocity.y > 0){
            Phys.AddForce(jumpforce * Phys.velocity.y * 0.05f * Vector2.down, ForceMode2D.Impulse);

        }

        isJumping = false;
    }

    [SerializeField] public bool canDash;
    [SerializeField] float dashForce = 20f;

    void dash(){
        if(canDash){
            effDash.GetComponent<ParticleSystem>().Play();
            dashtime = 0f;
            airtime = 0f;
            float dir = Mathf.Abs(moveInput.x) > Mathf.Epsilon? moveInput.x : Phys.velocity.x;
            Phys.AddForce(Mathf.Sign(dir) * dashForce * Vector2.right , ForceMode2D.Impulse);
            //if(Phys.velocity.y < 0.1f) 
            Phys.velocity = new Vector2(Phys.velocity.x,0.1f);
            canDash = false;
            GetComponent<AudioSource>().Play();
        }
    }

    [SerializeField] float termV = 2;
}

