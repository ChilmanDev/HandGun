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
 
public class ReadWrite : SampleUserPolling_ReadWrite
{
    public bool[] finger= {false, false, false, false};

    public bool trigger = false, reload = false;

    public int x,y;

    public Vector3 gyro;

    // Executed each frame
    void Update()
    {
        //---------------------------------------------------------------------
        // Send data
        //---------------------------------------------------------------------

        // If you press one of these keys send it to the serial device. A
        // sample serial device that accepts this input is given in the README.
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Sending A");
            serialController.SendSerialMessage("A");
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Sending Z");
            serialController.SendSerialMessage("Z");
        }


        //---------------------------------------------------------------------
        // Receive data
        //---------------------------------------------------------------------
        
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

            finger[0] = (data[0] == "CLOSED") ? true : false;
            finger[1] = (data[1] == "CLOSED") ? true : false;
            finger[2] = (data[2] == "CLOSED") ? true : false;
            finger[3] = (data[3] == "CLOSED") ? true : false;

            trigger = (data[4] == "FIRE") ? true : false;

            reload = (data[5] == "RELOAD") ? true : false;
            
            gyro[0] = Single.Parse(data[6]);
            gyro[1] = Single.Parse(data[7]);
            gyro[2] = Single.Parse(data[8]);

            //x = Int32.Parse(data[4]);

            //y = Int32.Parse(data[5]);

            //gyro.x = Single.Parse(data[6]);
            //gyro.y = Single.Parse(data[7]);
            //gyro.z = Single.Parse(data[8]);

            //Debug.Log("X: " + x + ", Y: " + y);            
        }
    }
}
