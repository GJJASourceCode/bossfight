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
    Vector3 currentVec;
    bool area1, area2, lookAtPlayer, run;
    Quaternion rotGoal;

    void Awake()
    {
        area = new GameObject[2];
        area[0] = GameObject.Find("Hand_collider");
        area[1] = GameObject.Find("Head_collider");
        area[0].SetActive(false);
        area[1].SetActive(false);
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody>();
        state = 0;
        area1 = false;
        area2 = false;
        lookAtPlayer = false;
        run = false;
        choosePattern();
    }

    IEnumerator state_0()
    {
        lookAtPlayer = true;
        yield return new WaitForSeconds(1f);
        if (area1)
        {
            if (area2)
            {
                state = Random.Range(1, 3);
            }
            else
            {
                state = 4;
            }
        }
        else
        {
            state = 3;
        }
        lookAtPlayer = false;
        choosePattern();
    }
    IEnumerator attack1()
    {
        anim.SetTrigger("Attack1");
        area[0].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        area[0].SetActive(false);
        state = 0;
        choosePattern();
    }

    IEnumerator attack2()
    {
        anim.SetTrigger("Attack2");
        area[1].SetActive(true);
        yield return new WaitForSeconds(3f);
        area[1].SetActive(false);
        state = 0;
        choosePattern();
    }
    IEnumerator charge()
    {
        anim.SetTrigger("scream");
        yield return new WaitForSeconds(1.4f);
        anim.SetInteger("run", 1);
        run = true;
        lookAtPlayer = true;
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        yield return new WaitForSeconds(0.25f);
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        yield return new WaitForSeconds(0.25f);
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        yield return new WaitForSeconds(0.25f);
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        yield return new WaitForSeconds(0.25f);
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        yield return new WaitForSeconds(0.25f);
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        yield return new WaitForSeconds(0.25f);
        currentVec = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        lookAtPlayer = false;
        yield return new WaitForSeconds(0.5f);
        anim.SetInteger("run", 0);
        run = false;
        state = 0;
        choosePattern();
    }
    IEnumerator movetime()
    {//이동시 2초만 이동
        yield return new WaitForSeconds(2f);
        state = 0;
        choosePattern();
    }
    void choosePattern()
    {
        Debug.Log("츄즈패턴");
        switch (state)
        {
            case 0:
                StartCoroutine("state_0");
                break;
            case 1:
                StartCoroutine("attack1");//attack1 손으로 공격
                break;
            case 2:
                StartCoroutine("attack2");//attack2 꺠물기 
                break;
            case 3:
                StartCoroutine("charge");//charge 돌진 공격
                break;
            case 4:
                StartCoroutine("movetime");
                anim.SetInteger("walk", 1);
                break;
            case 5:
                break;
            default:
                break;
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Area1")//바깥원
        {
            area1 = true;
        }
        if (col.gameObject.name == "Area2")//안쪽원
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
        Debug.Log(area2);
        Debug.Log(state);
        Vector3 dir = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        Vector3 zero = new Vector3(0f, 0f, 0f);

        if (run)
        {
            rigid.velocity = currentVec.normalized * 20.0f;
        }

        if (lookAtPlayer)
        {
            rotGoal = Quaternion.LookRotation(dir.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, 0.01f);
        }

        if (state == 4)
        {
            lookAtPlayer = true;
            if (area2 == true)
            {
                anim.SetInteger("walk", 0);
                StopCoroutine("movetime");
                lookAtPlayer = false;
                state = 0;
                choosePattern();
            }
            rigid.velocity = dir.normalized * 4.0f;

        }
        else if (state != 3)
        {
            rigid.velocity = zero * 4.0f;
        }
    }
}
