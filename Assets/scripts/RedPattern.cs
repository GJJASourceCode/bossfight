using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPattern : MonoBehaviour
{
    public static bool isAttacking;
    public static int state;
    public static int monsterHealth;
    Animator anim;
    GameObject[] area;
    GameObject player;
    Rigidbody rigid;
    Vector3 moveVec;
    bool area1, area2, lookAtPlayer, run, Last_ditch;

    void Awake()
    {
        isAttacking = false;
        monsterHealth = 1000;
        area = new GameObject[5];
        area[0] = GameObject.Find("Head_Collider");
        area[1] = GameObject.Find("Tail_Collider1");
        area[2] = GameObject.Find("Tail_Collider2");
        area[3] = GameObject.Find("Tail_Collider3");
        area[4] = GameObject.Find("Tail_Collider4");
        area[0].SetActive(false);
        area[1].SetActive(false);
        area[2].SetActive(false);
        area[3].SetActive(false);
        area[4].SetActive(false);
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = GetComponent<Rigidbody>();
        state = 0;
        area1 = false;
        area2 = false;
        Last_ditch = false;
        choosPattern();
    }
    IEnumerator state_0()
    {
        float time;
        yield return new WaitForSeconds(0.2f);
        lookAtPlayer = true;
        time = Random.Range(0f, 0.8f);
        yield return new WaitForSeconds(time);
        if (monsterHealth > 50 || Last_ditch)
        {
            if (area1)
            {
                if (area2)
                {
                    state = Random.Range(1, 3);
                }
                else
                {
                    state = 5;
                }
            }
            else
            {
                state = 3;
            }
        }
        else
        {
            state = 4;
        }
        lookAtPlayer = false;
        choosePattern();
    }

    IEnumerator attack1()
    {
        anim.SetTrigger("Basic Attack");
        area[0].SetActive(true);
        yield return new WaitForSeconds(1f);
        area[0].SetActive(false);
        yield return new WaitForSeconds(2f);
        choosPattern();
    }

    IEnumerator attack2()
    {
        anim.SetTrigger("Tail Attack");
        area[0].SetActive(true);
        yield return new WaitForSeconds(3f);
        area[0].SetActive(false);
        yield return new WaitForSeconds(2f);
        choosPattern();
    }

    IEnumerator attack3()
    {
        anim.SetTrigger("Fireball Shoot");
        area[1].SetActive(true);
        yield return new WaitForSeconds(3f);
        area[1].SetActive(false);
        yield return new WaitForSeconds(2f);
        choosPattern();
    }

    IEnumerator attack4()
    {
        anim.SetTrigger("Take Off");
        area[1].SetActive(true);
        yield return new WaitForSeconds(3f);
        area[1].SetActive(false);
        yield return new WaitForSeconds(2f);
        Last_ditch = true;
        choosPattern();
    }

    void Go()
    {
        choosPattern();
    }
    void choosPattern()
    {
        state = Random.Range(0, 4);
        switch (state)
        {
            case 0:
                StartCoroutine("state_0");
                break;
            case 1:
                StartCoroutine("attack1");
                break;
            case 2:
                StartCoroutine("attack2");
                break;
            case 3:
                StartCoroutine("attack3");
                break;
            case 4:
                StartCoroutine("attack4");
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
