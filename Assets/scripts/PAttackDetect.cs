using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAttackDetect : MonoBehaviour
{
    void Start()
    {
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "AttackRange")
        {
            if (GreenPattern.isAttacking && !Playermove.isRoll && !Playermove.isDeath)
            {
                switch (GreenPattern.state)
                {
                    case 1:
                        Playermove.health -= 23;
                        break;
                    case 2:
                        Playermove.health -= 32;
                        break;
                    case 3:
                        Playermove.health -= 40;
                        break;
                    case 4:
                        Playermove.health -= 45;
                        break;
                    default:
                        break;
                }
            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(Playermove.health);
    }
}
