using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurplePattern : MonoBehaviour
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
    Quaternion rotGoal; // set varieties

    void Awake() 
    {
        area = new GameObject[2];
        area[0] = GameObject.Find("Claw_collider");
        area[1] = GameObject.Find("Head_collider");

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
    IEnumerator idle()
    {
        float time;
        yield return new WaitForSeconds(0.2f);
        lookAtPlayer = true;
        time = Random.Range(0f,0.8f);
        yield return new WaitForSeconds(time);
        if (area1)
        {
            if(area2)
            {
                state = Random.Range(1,4);
            }
            else
            {
                state = 4;
            }
        }
        else
        {
            state = 5;
        }
        lookAtPlayer = false;
        choosePattern();
    }

    void choosePattern()
    {
        switch (state)
        {
            case 0:
                StartCoroutine("idle"); //calm state
                break;
            case 1:
                StartCoroutine("basic attack"); // headbutt 
                break;
            case 2:
                StartCoroutine("flame attack"); // hot breath 
                break;
            case 3:
                StartCoroutine("fly flame attack"); // welcome to hell
                break;
             case 4:
                StartCoroutine("movetime"); // walk
                anim.SetInteger("walk",1);
                break;    
            case 5:
                StartCoroutine("run"); // shorten distance
                break;
            default:
                break;
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
            anim.SetInteger("walk",1);
            rotGoal = Quaternion.LookRotation(dir.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotGoal,0.008f);
        }
        else
        {
            anim.SetInteger("walk",0);
        }
        
        if (state == 4)
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
        if (state ==0 || state == 1 || state == 2 || state == 3)
        {
            rigid.velocity = zero * 4.0f;
        }
    }
    
}
