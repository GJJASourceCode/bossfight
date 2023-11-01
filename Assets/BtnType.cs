using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnType : MonoBehaviour
{
   public BTNType currentType;
   public void OnBtnClick()
   {
     switch(currentType)
     {
        case BTNType.Red:
         Debug.Log("빨강");
         break;
        

     }
   } 
}
