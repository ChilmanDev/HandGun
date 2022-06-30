using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gyro : MonoBehaviour
{
    public JustRead msgListener;

    private Vector3 offset;

    
    // Start is called before the first frame update
    void Start()
    { 
        offset = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 localRotation = Vector3.zero;// = transform.rotation.eulerAngles;
        float angleX = (msgListener.x % 360) - 180;
        float angleY = (msgListener.y % 360) - 180;

        float x = angleX - transform.eulerAngles.x;
        float y = angleY - transform.eulerAngles.y;

        //.Rotate(new Vector3(angleX, 0, 0), Space.Self);

        //Debug.Log(angleX);

        localRotation.x = msgListener.x;
        //localRotation.y = msgListener.y;
        localRotation.y = msgListener.y;

        Vector3 translate = msgListener.gyro;

        if(Mathf.Abs(translate.x) < 2f) translate.x = 0f;
        if(Mathf.Abs(translate.y) < 3.5f) translate.y = 0f;
        if(Mathf.Abs(translate.z) < 3f) translate.z = 0f;

        transform.Translate(translate);

        //Debug.Log(x + ", " + y);

        //if(Mathf.Abs(x) > 5)
        {
            //transform.Rotate(new Vector3(x, 0, 0), Space.Self);
        }
        
        //transform.rotation = GyroToUnity(Input.gyro.attitude);
        //transform.rotation = Quaternion.Euler(new Vector3(localRotation.x, transform.rotation.y, transform.rotation.z) + offset);
        //transform.rotation.eulerAngles.x = msgListener.y;
    }
}
