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
    Rigidbody pRigid;
    bool isSlashing, isRoll;
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        pRigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        pSpeed = 4.0f;
        slashCurrentTime = 0f;
        rollCurrentTime = 0f;
        slashTime = 0f;
        rollTime = 1.2f;
        isRoll = false;
    }

    void Update()
    {
        camera.transform.position = new Vector3(transform.position.x, 12f, transform.position.z - 6f);
        Vector3 mousePos = Input.mousePosition;
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
        moveVec = new Vector3(xInput, 0f, zInput);
        pRigid.velocity = moveVec.normalized * pSpeed;
        Vector3 dir = new Vector3(mousePos.x - Screen.width / 2, 0f, mousePos.y - Screen.height / 2);
        transform.rotation = Quaternion.LookRotation(dir);
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
        if (Input.GetMouseButton(0) && !isSlashing && !isRoll)
        {
            slashCurrentTime = 0;
            isSlashing = true;
            slashNum = Random.Range(1, 4);
            if (slashNum == 1)
            {
                slashTime = 1.2f;
                anim.SetTrigger("slashing1");
            }
            else if (slashNum == 2)
            {
                slashTime = 1.8f;
                anim.SetTrigger("slashing2");
            }
            else if (slashNum == 3)
            {
                slashTime = 1.82f;
                anim.SetTrigger("slashing3");
            }
        }
        else if (Input.GetKeyDown("left shift") && !isSlashing && !isRoll)
        {
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
