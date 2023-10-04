using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurplePattern : MonoBehaviour
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
        area = new GameObject[2];
        area[0] = GameObject.Find("Claw_collider");
        area[1] = GameObject.Find("Head_collider");
    }
}
