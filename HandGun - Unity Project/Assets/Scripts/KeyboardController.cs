using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : Controller
{
    // Update is called once per frame
    void Update()
    {
        base.Update();

        fingerAction[(int)Finger.Index] = Input.GetKey(KeyCode.U) ?  1 : 0;
        fingerAction[(int)Finger.Middle] = Input.GetKey(KeyCode.I) ?  1 : 0;
        fingerAction[(int)Finger.Ring] = Input.GetKey(KeyCode.O) ?  1 : 0;
        fingerAction[(int)Finger.Pinky] = Input.GetKey(KeyCode.P) ?  1 : 0;


        if(Input.GetKeyDown(KeyCode.U)) Debug.Log("ssss");
    }
}
