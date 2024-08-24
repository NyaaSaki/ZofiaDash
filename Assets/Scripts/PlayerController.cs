using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //__________ Zofia Components ________________
    [SerializeField] public Vector2 moveInput;
    Rigidbody2D Phys;
    BoxCollider2D coll;
    Animator anim;
    [SerializeField] GameObject effDash;
    [SerializeField] GameObject back;
    public bool canMove = true;

    //_______________ Jump ________________

    [SerializeField] float grav = 3.5f;
    [SerializeField] float ctime= 0.5f;
    [SerializeField]public float coyote;
    [SerializeField]float jumpBuffer;
    [SerializeField]BoxCollider2D gnd;
    [SerializeField] float termV = 2;


    void jumptick(){
        if(gnd.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
        coyote = 0;
        canShoot = true;
        anim.SetBool("InAir",false);
        }
        else anim.SetBool("InAir",true);
        coyote += Time.deltaTime;
        jumpBuffer += Time.deltaTime;
        dashtime += Time.deltaTime;
        airtime += Time.deltaTime;
    }

    public void setcoyote(){
        coyote = 1f;
    }
    public bool doubleJump = false;


    [SerializeReference] GameObject jumpsound;
    [SerializeField] float jumpcap;

    //________________ Dynamic Jumping ______________________
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

        
        if(PlaneShiftBar >0) Phys.gravityScale=0f;
        else if(Phys.velocity.y <0) Phys.gravityScale = grav * 3;
        else if(Phys.velocity.y < 0.1 && Phys.velocity.y > -0.1)  Phys.gravityScale = grav * 0.2f;
        else if(dashtime<-0.3f) Phys.gravityScale = grav * 0.6f;
        else Phys.gravityScale = grav;

       if(Input.GetKeyUp(KeyCode.Space) & dashtime > 2f){
            onJumpUp();
       }
    }

    void onJumpUp(){   
        if(isJumping && Phys.velocity.y > 0){
            Phys.AddForce(jumpforce * Phys.velocity.y * 0.05f * Vector2.down, ForceMode2D.Impulse);

        }

        isJumping = false;
    }

    //______________________________________ Umbrella _________________
    [SerializeField] public bool hasUmbrella = false;
    [SerializeField] SpriteRenderer UmbrellaSprite;
    float modified_TermV;

    [SerializeField] public bool GravUp = false;

    void checkUmbrella(){
        if(hasUmbrella && Input.GetKey(KeyCode.Mouse1)){
            UmbrellaSprite.enabled = true;
            modified_TermV = termV * (0.1f + (moveInput.y > -Mathf.Epsilon?0f:0.5f));
        }
        else {modified_TermV = termV; UmbrellaSprite.enabled = false;}

        

        if(GravUp){
            modified_TermV = modified_TermV/2 - 4;
        }

        if(hasPaws && (SL.isTouching || SR.isTouching)) modified_TermV = modified_TermV *0.2f;
        print(modified_TermV);
    }
    
    
    


    //_______________________Turn Sprite________________________


    void turn(){

        bool isMoving = Mathf.Abs(moveInput.x) > Mathf.Epsilon;
        if(isMoving && Mathf.Abs(Phys.velocity.x)>0.2){ 
            if(Mathf.Abs(Phys.velocity.x)>2) transform.localScale = new Vector2(Mathf.Sign(Phys.velocity.x),1);
            
            anim.SetBool("IsWalk",true);
            }
        else anim.SetBool("IsWalk",false);

        if(moveInput.y<-Mathf.Epsilon) anim.SetBool("IsCrouch",true);
        else anim.SetBool("IsCrouch",false);
    }

    
    // __________________________ Horizontal Movement ___________

    [SerializeField] float acc = 0.1f;
    [SerializeField] float decc = 0.5f;
    [SerializeField] float Speed = 10f; 
    [SerializeField] float velpower = 1.3f;
    [SerializeField] float jumpforce = 10f;
    public float dashtime = 10f;
    public float airtime = 10f;

    public bool onPlatform = false;
    public Rigidbody2D platform;
    
  
    [SerializeField] Vector2 tgtSpeed;

    void Running(){
        
        
        if(PlaneShiftBar >0){
            PlaneMovement();
            GetComponent<SpriteRenderer>().color = PlaneColor;
            return;
        }
        GetComponent<SpriteRenderer>().color = baseColor;
        if(Phys.velocity.y < 0.1 && Phys.velocity.y > -0.1 && !gnd.IsTouchingLayers(LayerMask.GetMask("Ground"))){
        tgtSpeed = moveInput*Speed*1.1f;
        }

        else if(!canMove) tgtSpeed = new Vector2(0,0);
        else if(dashtime < 0f) tgtSpeed = Phys.velocity * 0.95f;
        else if(dashtime < 0.3f) tgtSpeed = Phys.velocity * 0.7f + moveInput*Speed;
        else tgtSpeed = moveInput*Speed;

        if(moveInput.y<0) transform.localScale = new Vector3(transform.localScale.x,0.9f,1);
        else transform.localScale = new Vector3(transform.localScale.x,1f,1);

        if(onPlatform){
            tgtSpeed += new Vector2(platform.velocity.x,0f);
            if(moveInput.y< -Mathf.Epsilon && platform.velocity.y>=0&& Phys.velocity.y>Mathf.Epsilon) {
                Phys.velocity = new Vector2(Phys.velocity.x,platform.velocity.y);
                Phys.gravityScale = grav*100;
            }
            else Phys.gravityScale = grav;
        }

        float forceNeeded = tgtSpeed.x - Phys.velocity.x;
        //Debug.Log("" + forceNeeded);
        float tdecc = decc;
        //if(moveInput.y< -Mathf.Epsilon) tdecc = decc*2;



        anim.SetBool("IsWalk",true);
        bool isAcc = (Mathf.Abs(moveInput.x) > Mathf.Epsilon);
        
        

        //____Terminal Velocity____
        if(Phys.velocity.y < -modified_TermV) Phys.velocity = new Vector2(Phys.velocity.x,-modified_TermV);
        //if(dashtime < 0.3f && Phys.velocity.y < 0.1f) Phys.velocity = new Vector2(Phys.velocity.x,0.1f);
        Phys.AddForce( Mathf.Pow((Mathf.Abs(forceNeeded) * ((isAcc&&!onPlatform)?acc:(airtime<0.2f?10:tdecc))) ,velpower)* Time.deltaTime * Vector2.right *Mathf.Sign(forceNeeded) );
        back.transform.localPosition = new Vector3(Mathf.Clamp(-Mathf.Abs(Phys.velocity.x)*0.03f,-0.06f,0f),0f,0f);
        turn();
    }

    //__________________________Pulse__________________________________

    public void pulse(Vector3 direction , int type = 1){
        freeze = false;
        Phys.bodyType = RigidbodyType2D.Dynamic;
        if (activeFlr) {
            activeFlr.GetComponent<BloomLaunch>().launch();
            activeFlr = null;
            type = 2;
            direction = direction * 1.2f;
            }
        if (type ==1 || type == 2) canShoot = false;
        Vector2 perpendicular = Vector2.Perpendicular(new Vector2(direction.x,direction.y).normalized);
        Vector2 oldMove = perpendicular * Vector2.Dot(Phys.velocity , perpendicular);
        if(type == 1) Phys.velocity = oldMove;
        else if(type == 3) {
            Phys.velocity = new Vector2(Phys.velocity.x , Mathf.Max(Phys.velocity.y , 0.1f)); 
        }
        if(moveInput.y<0) Phys.AddForce(-direction * dashForce * 0.02f ,  ForceMode2D.Impulse);
        else Phys.AddForce(-direction * dashForce/4 ,  ForceMode2D.Impulse);
        dashtime = -0.1f * direction.magnitude;
        airtime = 0f;
        Invoke("setcoyote",0.1f);
        return;
    }


    //________________________inputs________________________
    void OnMove(InputValue inp){
        moveInput = inp.Get<Vector2>();
    }
    public bool isJumping = false;


    [SerializeField] SafetyHeadphones SL;
    [SerializeField] SafetyHeadphones SR;

    public bool hasPaws = false;
    void OnJump(InputValue inp){
        jumpBuffer = 0.05f;
        if(hasPaws){
            if(SL.isTouching && coyote > 0.1f) pulse(new Vector3(-1.4f* transform.localScale.x, -1.4f), 3 );
            else if(SR.isTouching && coyote > 0.1f) pulse(new Vector3(1.4f* transform.localScale.x, -1.4f), 3 );
        }
        
    }

    public void collect(string buff){
        if(buff == "dash") canDash = true;
        else if(buff == "jump") doubleJump = true;
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

    public bool canShoot = true;

    [SerializeField] GameObject aim;

    //__________________ BloomFlower _________________________

    public GameObject activeFlr;
    public void BloomFlower(GameObject flr){
        activeFlr = flr;
        Phys.velocity = Vector2.zero ;
        freeze = true;
        canShoot = true;
        transform.position = flr.transform.position + new Vector3(0f, -0.0f, 0f);
    }

    //___________________Plane movement__________________

    [SerializeField] float PlaneShiftBar = -1f;

    [SerializeField] Color PlaneColor;
    Color baseColor;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Plane")) {
            PlaneShiftBar = 3f;
            transform.position = other.transform.position;
            }
        else if(other.CompareTag("water")) {
            PlaneShiftBar = -1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        PlaneShiftBar = -1f;
    }

    void PlaneMovement(){
        Vector2 tgtSpeed = moveInput * Time.deltaTime * Speed * 170;
        Vector2 RequiredForce = tgtSpeed - Phys.velocity;
        if(RequiredForce.magnitude>1f) Phys.AddForce(RequiredForce*20);
        PlaneShiftBar -=Time.deltaTime;
    }


    
    //________________ Monobehaviour ___________________

    void Awake() {
        Phys = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        baseColor = GetComponent<SpriteRenderer>().color;
    }
    void Start()
    {   
        coyote = 0.1f;
        jumpBuffer = 0.5f;
    }

    [SerializeField] public bool freeze = false;
    // Update is called once per frame


    void Update(){
       Running();

       checkUmbrella();

        if(!freeze) {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic ;
            jumpcut();
            jumptick();
            if(Input.GetKeyDown(KeyCode.E)) FindObjectOfType<EquipSelect>().scroll(GetComponent<PlayerController>());
        }
        else  GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

}

