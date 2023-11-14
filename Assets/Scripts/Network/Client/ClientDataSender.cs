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

public class ClientDataSender : MonoBehaviour
{
    private Socket socketUDP;
    private EndPoint ipepUDP;

    public ClientDataSender(Socket socketUDP, EndPoint ipepUDP)
    {
        this.socketUDP = socketUDP;
        this.ipepUDP = ipepUDP;
        StartCoroutine(SendDataPeriodically());
    }

    public void SetInfo()
    {

        ClientConnection clientConnection = new ClientConnection();
        socketUDP =clientConnection.socketInfo();
        ipepUDP = clientConnection.ipInfo();
    }
    private IEnumerator SendDataPeriodically()
    {
        while (true)
        {
            // Insert your code to get the serialized bytes here.
            byte[] data = new byte[1024];
            Serializer sr = null;
            data = sr.SerializeXML();
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
                Debug.Log("Mandando data");
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


    /////////////////////
    //Ejemplo de como llamar la clase
    /////////////////////
    /*
     DataSender dataSender = new DataSender(socketUDP, ipepUDP);
bool isSuccess = dataSender.SendSerializedData(bytes);

if (isSuccess)
{
    UnityEngine.Debug.Log("Data sent successfully.");
}
else
{
    UnityEngine.Debug.Log("Failed to send data.");
}

      */
}

