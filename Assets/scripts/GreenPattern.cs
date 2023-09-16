using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPattern : MonoBehaviour
{
    int state;
    Animator anim;
    GameObject[] area;
    GameObject player;
    Rigidbody rigid;
    Vector3 moveVec;
    bool area1, area2;

    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody>();
        area1 = false;
        area2 = false;
        choosPattern();
    }

    IEnumerator attack1()
    {
        anim.SetTrigger("Attack1");
        //area[0].SetAcitve(true);
        yield return new WaitForSeconds(3f);
        //area[0].SetAcitve(false);
        yield return new WaitForSeconds(2f);
        choosPattern();
    }

    IEnumerator attack2()
    {
        anim.SetTrigger("Attack2");
        //area[1].SetAcitve(true);
        yield return new WaitForSeconds(3f);
        //area[1].SetAcitve(false);
        yield return new WaitForSeconds(2f);
        choosPattern();
    }

    void Go()
    {
        choosPattern();
    }
    void choosPattern()
    {
        /*if (area1)
        {
            if (area2)
            {
                Random.Range(0, 3);
            }
            else
            {
                state = 4;
            }
        }
        else
        {
            state = 3;
        }*/
        state = Random.Range(0, 2);
        switch (state)
        {
            case 0:
                StartCoroutine("attack1");
                break;
            case 1:
                StartCoroutine("attack2");
                break;
            /*case 2:
                Go();
                break;
            case 3:
                Go();
                break;
            case 4:
                break;*/
            default:
                break;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Area1")
        {
            area1 = true;
        }
        if (col.gameObject.name == "Area2")
        {
            area2 = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Area1")
        {
            area1 = false;
        }
        if (col.gameObject.name == "Area2")
        {
            area2 = false;
        }
    }
    void Update()
    {
        Vector3 dir = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.y - transform.position.y);
        if (state == 4)
        {
            transform.rotation = Quaternion.LookRotation(dir);
            rigid.velocity = dir.normalized * 4.0f;
        }
    }
}
