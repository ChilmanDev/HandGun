using System.Collections;
using System.Collections. Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public Controller controller;
    public Hand hand;

    void Start()
    {
        //controller = GetComponent<ActionBasedController>();
    }

    void Update()
    {
        //hand.SetFinger(controller.selectAction.action. ReadValue<float>());
        //hand.SetTrigger(controller.activateAction.action.ReadValue<float>());
        for(int i=0; i < 4; i++){
            //hand.SetFinger(i, controller.getFingerValue(i));
        }
    }
}