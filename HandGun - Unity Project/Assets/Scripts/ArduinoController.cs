using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArduinoController : Controller
{
    public JustRead ardData;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        setFingers(ardData.finger);
    }

    void setFingers(bool[] fValues){
        fingerAction[0] = fValues[0] ? 1 : 0;
        fingerAction[1] = fValues[1] ? 1 : 0;
        fingerAction[2] = fValues[2] ? 1 : 0;
        fingerAction[3] = fValues[3] ? 1 : 0;
    }
}
