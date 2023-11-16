using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using static UnityEngine.InputSystem.InputRemoting;

public class ServerDataSender : MonoBehaviour
{
    private Socket socketUDP;
    private EndPoint ipepUDP;

    public void SetInfo(Socket socketUDP, EndPoint ipepUDP)
    {
        this.socketUDP = socketUDP;
        this.ipepUDP = ipepUDP;

    }

    public void SendInfo(Serializer ser)
    {
        //StartCoroutine(SendDataPeriodically(ser));

        byte[] data = new byte[1024];
        data = ser.SerializeHostDataXML();
        SendSerializedData(data);

    }
    private IEnumerator SendDataPeriodically(Serializer sr)
    {
        while (true)
        {
            // Insert your code to get the serialized bytes here.
            byte[] data = new byte[1024];
            data = sr.SerializeHostDataXML();
            SendSerializedData(data);

            // Wait for 1 second before the next send.
            yield return new WaitForSeconds(1.0f);
        }

    }
    public bool SendSerializedData(byte[] data)
    {
        try
        {
            // Connect to the server.
            socketUDP.Connect(ipepUDP);
            Debug.Log(ipepUDP);
            // Check if the connection is successful.
            if (socketUDP.Connected)
            {
                Debug.Log("Mandando data a cliente");
                // Send the serialized data.
                socketUDP.SendTo(data, data.Length, SocketFlags.None, ipepUDP);
                return true;
            }
            else
            {
                UnityEngine.Debug.Log("Unable to connect to the server.");
                return false;
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur.
            UnityEngine.Debug.LogError("An error occurred while sending data: " + ex.Message);
            return false;
        }
    }
}
