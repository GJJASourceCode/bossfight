using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPattern : MonoBehaviour
{
    public AudioSource attack1Sound, attack2Sound, jumpAttackSound1, jumpAttackSound2, howlingSound, dashSound;
    public static bool isAttacking, isDeath;
    public static int monsterHealth;
    public static int state;
    Animator anim;
    GameObject[] area;
    public GameObject player, victory;
    Rigidbody rigid;
    Vector3 currentVec;
    bool area1, area2, lookAtPlayer, run;
    Quaternion rotGoal;

    void Awake()
    {
        isAttacking = false;
        monsterHealth = 1000;
        area = new GameObject[4];
        area[0] = GameObject.Find("Hand_collider");//손 공격범위
        area[1] = GameObject.Find("Head_collider");//머리 공격범위
        area[2] = GameObject.Find("jumpRange");//점프 공격범위
        area[3] = GameObject.Find("chargeRange");//돌진 공격범위
        area[0].SetActive(false);
        area[1].SetActive(false);
        area[2].SetActive(false);
        area[3].SetActive(false);
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
                state = Random.Range(1, 4);
            }
            else
            {
                state = 5;
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
        anim.SetTrigger("Attack1");
        yield return new WaitForSeconds(0.7f);
        attack1Sound.Play();
        yield return new WaitForSeconds(0.1f);
        area[0].SetActive(true);
        isAttacking = true;
        yield return new WaitForSeconds(0.2f);
        area[0].SetActive(false);
        isAttacking = false;
        yield return new WaitForSeconds(0.5f);
        state = 0;
        choosePattern();
    }

    IEnumerator attack2()
    {
        anim.SetTrigger("Attack2");
        attack2Sound.Play();
        yield return new WaitForSeconds(0.8f);
        area[1].SetActive(true);
        isAttacking = true;
        yield return new WaitForSeconds(0.1f);
        area[1].SetActive(false);
        isAttacking = false;
        yield return new WaitForSeconds(1.4f);
        state = 0;
        choosePattern();
    }
    IEnumerator charge()
    {
        anim.SetTrigger("scream");
        howlingSound.Play();
        yield return new WaitForSeconds(1.4f);
        anim.SetInteger("run", 1);
        dashSound.Play();
        area[3].SetActive(true);
        isAttacking = true;
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
        lookAtPlayer = false;
        yield return new WaitForSeconds(0.75f);
        area[3].SetActive(false);
        isAttacking = false;
        anim.SetInteger("run", 0);
        run = false;
        yield return new WaitForSeconds(0.5f);
        state = 0;
        choosePattern();
    }
    IEnumerator jumpAttack()
    {

        anim.SetTrigger("jumpAttack");
        yield return new WaitForSeconds(0.4f);
        jumpAttackSound1.Play();
        yield return new WaitForSeconds(1.2f);
        isAttacking = true;
        area[2].SetActive(true);
        jumpAttackSound2.Play();
        yield return new WaitForSeconds(0.1f);
        area[2].SetActive(false);
        isAttacking = false;
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
        if(!isDeath){
            switch (state)
        {
            case 0:
                StartCoroutine("state_0");//일시정지
                break;
            case 1:
                StartCoroutine("attack1");//attack1 손으로 공격
                break;
            case 2:
                StartCoroutine("attack2");//attack2 꺠물기 
                break;
            case 3:
                StartCoroutine("jumpAttack");//점프
                break;
            case 4:
                StartCoroutine("charge");//charge 돌진 공격   
                break;
            case 5:
                StartCoroutine("movetime");
                anim.SetInteger("walk", 1);
                break;
            default:
                break;
        }
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
    IEnumerator Diiie(){
        yield return new WaitForSeconds(3f);
        victory.SetActive(true);

    }
    void Update()
    {
        if(monsterHealth<=0&&!isDeath){
            isDeath = true;
            state = 6;
            monsterHealth = 0;
            anim.SetTrigger("death");
            anim.SetInteger("dying",1);
            StartCoroutine("Diiie");
        }
        if(isDeath){
            monsterHealth = 0;
        }
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
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal, 0.02f);
        }
        else
        {
            anim.SetInteger("walk", 0);
        }

        if (state == 5)
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
        if (state == 0 || state == 1 || state == 2 || state == 3 || state == 6)
        {
            rigid.velocity = zero * 4.0f;
        }
    }
}
