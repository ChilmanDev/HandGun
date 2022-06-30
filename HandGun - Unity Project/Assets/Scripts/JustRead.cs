/**
 * Ardity (Serial Communication for Arduino + Unity)
 * Author: Daniel Wilches <dwilches@gmail.com>
 *
 * This work is released under the Creative Commons Attributions license.
 * https://creativecommons.org/licenses/by/2.0/
 */

using UnityEngine;
using System.Collections;
using System;

/**
 * Sample for reading using polling by yourself. In case you are fond of that.
 */
 
public class JustRead : SampleUserPolling_JustRead
{
    public bool[] finger= {false, false, false, false};

    public bool trigger = false, reload = false;

    public int x,y;

    public Vector3 gyro;

    // Executed each frame
    void Update()
    {
        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Connection attempt failed or disconnection detected");
        else
        {
            string[] data = message.Split(' ');


            if(data[0].Length > 0)
            {            
                finger[0] = (data[0] == "CLOSED") ? true : false;
                finger[1] = (data[1] == "CLOSED") ? true : false;
                finger[2] = (data[2] == "CLOSED") ? true : false;
                finger[3] = (data[3] == "CLOSED") ? true : false;
            }

            if(data.Length >= 5)
            {
                trigger = (data[4] == "FIRE") ? true : false;

                reload = (data[5] == "RELOAD") ? true : false;
                
            }

            if(data.Length >= 7)
            {
                gyro[0] = Single.Parse(data[6]);
                gyro[1] = Single.Parse(data[7]);
                gyro[2] = Single.Parse(data[8]);
            }
            //x = Int32.Parse(data[4]);

            //y = Int32.Parse(data[5]);

            //gyro.x = Single.Parse(data[6]);
            //gyro.y = Single.Parse(data[7]);
            //gyro.z = Single.Parse(data[8]);

            //Debug.Log("X: " + x + ", Y: " + y);            
        }
    }
}
