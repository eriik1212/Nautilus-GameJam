using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class UDPServer : MonoBehaviour
{
    private UdpClient udpServer;
    private int serverPort = 12345; // Choose a port number

    private void Start()
    {
        udpServer = new UdpClient(serverPort);
        Debug.Log("UDP server started on port " + serverPort);

        // Start listening for incoming messages in the background
        udpServer.BeginReceive(ReceiveCallback, null);
    }

    private void ReceiveCallback(IAsyncResult result)
    {
        IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
        byte[] receivedData;

        try
        {
            receivedData = udpServer.EndReceive(result, ref clientEndPoint);
            string clientMessage = System.Text.Encoding.UTF8.GetString(receivedData);
            Debug.Log("Received message from " + clientEndPoint + ": " + clientMessage);

            // Get the server's name
            string serverName = System.Environment.MachineName;

            // Respond to the client with the server's name
            byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes(serverName);
            udpServer.Send(responseBytes, responseBytes.Length, clientEndPoint);

            // Continue listening for more messages
            udpServer.BeginReceive(ReceiveCallback, null);
        }
        catch (Exception e)
        {
            Debug.LogError("Error receiving or responding to UDP message: " + e.Message);
        }
    }

    private void OnApplicationQuit()
    {
        // Close the UDP socket when the application quits
        udpServer.Close();
    }
}

