using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermove : MonoBehaviour
{
    GameObject camera;
    int slashNum;
    Animator anim;
    float xInput, zInput, pSpeed, slashTime, slashCurrentTime, rollTime, rollCurrentTime;
    Vector3 moveVec, point;
    Vector2 turn;
    Rigidbody pRigid;
    bool isSlashing, isRoll;
    void Start()
    {
        pRigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        pSpeed = 4.0f;
        slashCurrentTime = 0f;
        rollCurrentTime = 0f;
        slashTime = 0f;
        rollTime = 1.2f;
        isRoll = false;
    }
    IEnumerator rolling()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        yield return new WaitForSeconds(1.15f);
        //transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }
    void Update()
    {
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
        if (rollCurrentTime > 0.9f)
        {
            pSpeed = 4.0f;
        }
        if (rollCurrentTime > rollTime)
        {
            isRoll = false;
        }
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
        if (Input.GetMouseButton(0) && !isSlashing && !isRoll)
        {
            slashCurrentTime = 0;
            isSlashing = true;
            slashNum = Random.Range(1, 3);
            if (slashNum == 1)
            {
                slashTime = 1.2f / 1.5f;
                anim.SetTrigger("slashing1");
            }
            else if (slashNum == 2)
            {
                slashTime = 1.8f / 1.5f;
                anim.SetTrigger("slashing2");
            }
        }
        else if (Input.GetKeyDown("left shift") && !isSlashing && !isRoll)
        {
            StartCoroutine("rolling");
            rollCurrentTime = 0;
            isRoll = true;
            pSpeed = 10f;
            anim.SetTrigger("roll");
        }
        if (Mathf.Abs(xInput) > Mathf.Epsilon || Mathf.Abs(zInput) > Mathf.Epsilon)
        {
            anim.SetInteger("animState", 1);
        }
        else
        {
            anim.SetInteger("animState", 0);
        }
    }
}
