using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermove : MonoBehaviour
{
    public GameObject camera;
    Animator anim;
    float xInput, zInput, pSpeed;
    Vector3 moveVec, point;
    Rigidbody pRigid;
    void Start()
    {
        pRigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        pSpeed = 4.0f;
    }

    void Update()
    {
        camera.transform.position = new Vector3(transform.position.x, 12f, transform.position.z - 6f);
        Vector3 mousePos = Input.mousePosition;
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
        moveVec = new Vector3(xInput, 0f, zInput);
        pRigid.velocity = moveVec.normalized * pSpeed;
        Vector3 dir = new Vector3(mousePos.x - 550, 0f, mousePos.y - 235);
        transform.rotation = Quaternion.LookRotation(dir);
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
