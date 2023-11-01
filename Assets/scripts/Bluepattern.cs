using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bluepattern : MonoBehaviour
{
    public static bool isAttacking;
    public static int monsterHealth;
    public static int state;
    Animator anim;
    GameObject[] area;
    GameObject player;
    Rigidbody rigid;
    Vector3 currentVec;
    bool area1, area2, lookAtPlayer, run;
    Quaternion rotGoal;

    void Awake()
    {
        area = new GameObject[5];
        area[0] = GameObject.Find("Chest");
        area[1] = GameObject.Find("Head");
        area[2] = GameObject.Find("Tongue01");
        area[3] = GameObject.Find("Middle01_L");
        area[4] = GameObject.Find("Middle01_R");
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
        lookAtPlayer = false;
        run = false;
        choosePattern();
    }

     IEnumerator state_0()
    {
        float time;
        yield return new WaitForSeconds(0.2f);
        lookAtPlayer = true;
        time = Random.Range(0f, 0.8f);
        yield return new WaitForSeconds(time);
        if (area1)
        {
            if (area2)
            {
                state = Random.Range(1, 2);
            }
            /*else
            {
                state = 3;
            }*/
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
        yield return new WaitForSeconds(0.7f);
        yield return new WaitForSeconds(0.1f);
        area[0].SetActive(true);
        area[1].SetActive(true);
        area[2].SetActive(true);
        isAttacking = true;
        yield return new WaitForSeconds(0.2f);
        area[0].SetActive(false);
        area[1].SetActive(false);
        area[2].SetActive(false);
        isAttacking = false;
        yield return new WaitForSeconds(0.5f);
        state = 0;
        choosePattern();
    }
   
    IEnumerator attack2()
    {
        anim.SetTrigger("Attack2");
        yield return new WaitForSeconds(0.8f);
        area[0].SetActive(true);
        area[1].SetActive(true);
        area[2].SetActive(true);
        area[3].SetActive(true);
        area[4].SetActive(true);
        isAttacking = true;
        yield return new WaitForSeconds(0.1f);
        area[0].SetActive(false);
        area[1].SetActive(false);
        area[2].SetActive(false);
        area[3].SetActive(false);
        area[4].SetActive(false);
        isAttacking = false;
        yield return new WaitForSeconds(1.4f);
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
        Debug.Log(state);
        switch (state)
        {
            case 0:
                StartCoroutine("state_0");
                break;
            case 1:
                StartCoroutine("attack1");//attack1 깨물기
                break;
            case 2:
                StartCoroutine("attack2");//attack2 적극적인 꺠물기
                break;
            case 3:
                StartCoroutine("movetime");
                anim.SetInteger("walk", 1);
                break;
            default:
                break;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "Area1")//바깥원
        {
            area1 = true;
            Debug.Log("접근1");
        }
        if (col.gameObject.name == "Area2")//안쪽원
        {
            area2 = true;
            Debug.Log("접근2");
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
        Vector3 dir = new Vector3(player.transform.position.x - transform.position.x, 0f, player.transform.position.z - transform.position.z);
        Vector3 zero = new Vector3(0f, 0f, 0f);
        if (run)
        {
            rigid.velocity = currentVec.normalized * 20.0f;
        }

        if (lookAtPlayer)
        {
            anim.SetInteger("walk", 1);
            rotGoal = Quaternion.LookRotation(dir.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, 0.008f);
        }
        else
        {
            anim.SetInteger("walk", 0);
        }

        if (state == 3)
        {
            lookAtPlayer = true;
            if (area2 == true)
            {
                StopCoroutine("movetime");
                lookAtPlayer = false;
                state = 0;
                choosePattern();
            }
            rigid.velocity = dir.normalized * 4.0f;

        }
        if (state == 0 || state == 1 || state == 2)
        {
            rigid.velocity = zero * 4.0f;
        }
    }
}
