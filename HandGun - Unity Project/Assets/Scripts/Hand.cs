using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]

public class HandAnimation : MonoBehaviour
{
    public float speed;
    Animator animator;
    private float[] fingerTarget = {0,0,0,0};
    private float[] fingerCurrent = {0,0,0,0};
    private string[] animatorFingersParam = {"Index", "Middle", "Ring", "Pinky"};

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        AnimateHand();
    }

    internal void SetFinger(int _finger, float v)
    {
        fingerTarget[_finger] = v;
        //gripTarget = v;
    }
    internal void SetTrigger(float v)
    {
        //triggerTarget = v;
    }

    void AnimateHand ()
    {
        for(int i=0; i < 4; i++){
            if(fingerCurrent[i] != fingerTarget[i]){
                fingerCurrent[i] = Mathf.MoveTowards(fingerCurrent[i], fingerTarget[i], Time.deltaTime * speed);
                animator.SetFloat(animatorFingersParam[i], fingerCurrent[i]);
            }
        }
    }   
}
