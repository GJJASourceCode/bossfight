using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermove : MonoBehaviour
{
    public AudioSource slash1, slash2;
    GameObject body;
    public static int slashNum, health;
    Animator anim;
    float xInput, zInput, pSpeed, slashTime, slashCurrentTime, rollTime, rollCurrentTime;
    Vector3 moveVec, point;
    Vector2 turn;
    Rigidbody pRigid;
    bool canRoll;
    public static bool isSlashing, isRoll, isHited, isDeath;
    void Start()
    {
        body = GameObject.Find("Armature");
        pRigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        pSpeed = 4.0f;
        slashCurrentTime = 0f;
        rollCurrentTime = 0f;
        slashTime = 0f;
        rollTime = 0.9f;
        isRoll = false;
        canRoll = true;
        health = 200;
    }
    void Update()
    {
        if(health<=0&&!isDeath){
            anim.SetTrigger("death");
            isDeath = true;
            health=0;
        }
        turn.x += Input.GetAxis("Mouse X");
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
        moveVec = new Vector3(xInput, 0f, zInput);
        slashCurrentTime += Time.deltaTime;
        rollCurrentTime += Time.deltaTime;
        if (slashCurrentTime > slashTime)
        {
            isSlashing = false;
        }
        if (rollCurrentTime > 0.3f)
        {
            pSpeed = 4.0f;
            isRoll = false;
        }
        if (rollCurrentTime > rollTime)
        {
            canRoll = true;
        }
        if(!isDeath){
            if (Input.GetKey("w"))
        {
            pRigid.velocity = transform.forward * pSpeed;
        }
        if (Input.GetKey("s"))
        {
            pRigid.velocity = -transform.forward * pSpeed;
        }
        if (Input.GetKey("d"))
        {
            pRigid.velocity = transform.right * pSpeed;
        }
        if (Input.GetKey("a"))
        {
            pRigid.velocity = -transform.right * pSpeed;
        }
        }
        if (Input.GetMouseButton(0) && !isSlashing && !isRoll && !isDeath)
        {
            slashCurrentTime = 0;
            isSlashing = true;
            slashNum = Random.Range(1, 3);
            if (slashNum == 1)
            {
                slashTime = 1.2f / 1.5f;
                anim.SetTrigger("slashing1");
                slash1.Play();
            }
            else if (slashNum == 2)
            {
                slashTime = 1.8f / 1.5f;
                anim.SetTrigger("slashing2");
                slash2.Play();
            }
        }
        else if (Input.GetKeyDown("left shift") && !isSlashing && canRoll && !isDeath)
        {
            rollCurrentTime = 0;
            isRoll = true;
            canRoll = false;
            pSpeed = 10f;
            anim.SetTrigger("roll");
        }
        if (Mathf.Abs(xInput) > Mathf.Epsilon || Mathf.Abs(zInput) > Mathf.Epsilon)
        {
            if(!isDeath){
                anim.SetInteger("animState", 1);
            }
        }
        else
        {
            if(!isDeath){
                anim.SetInteger("animState", 0);
            }
        }
    }
}
