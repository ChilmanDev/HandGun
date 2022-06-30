using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Finger{Index, Middle, Ring, Pinky}

public class Controller : MonoBehaviour
{
    protected float[] fingerAction = {0,0,0,0};

    [SerializeField] protected HandAnimation hand;
    
    [SerializeField] protected Player player;
    protected HandPose handPose;
    

    // Update is called once per frame
    protected void Start() {
        handPose = HandPose.None;
    }
    protected void Update()
    {
        setHandFingers();
        UpdateHandPose();
        setPlayerHandPose();
    }

    protected float getFingerValue(int _finger)
    {
        return fingerAction[_finger];
    }

    protected void setHandFingers()
    {
        for(int i=0; i < 4; i++){
            hand.SetFinger(i, getFingerValue(i));
        }
    }
    
    protected void UpdateHandPose()
    {
        if (checkHandPose(false, false, true, true))
            handPose = HandPose.Gun;
        
        else if (checkHandPose(false, true, true, false))
            handPose = HandPose.Spidey;
        
        else if (checkHandPose(true, true, true, true))
            handPose = HandPose.Fist;
        
        else if (checkHandPose(false, false, false, false))
            handPose = HandPose.Paper;

        else if (checkHandPose(true, false, false, false))
            handPose = HandPose.Point;

        else handPose = HandPose.None;
        
    }

    protected bool checkHandPose(bool index, bool middle, bool ring, bool pinky)
    {
        bool[] pose = {index, middle, ring, pinky};

        for(int i=0; i < 4; i++)
        {
            if((fingerAction[i] == 1 ? true : false) != pose[i])
                return false;
        }
        return true;
    }

    protected void setPlayerHandPose()
    {
        player.setHandPose(handPose);
    }
}
