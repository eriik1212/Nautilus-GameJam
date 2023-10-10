using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TCPServer : MonoBehaviour
{
    private TcpListener tcpListener;
    private int serverPort = 12345; // Choose a port number

    private void Start()
    {
        try
        {
            tcpListener = new TcpListener(IPAddress.Any, serverPort);
            tcpListener.Start();
            Debug.Log("TCP server started on port " + serverPort);

            // Start accepting client connections asynchronously
            tcpListener.BeginAcceptTcpClient(AcceptClientCallback, null);
        }
        catch (Exception e)
        {
            Debug.LogError("Error starting TCP server: " + e.Message);
        }
    }

    private void AcceptClientCallback(IAsyncResult result)
    {
        try
        {
            TcpClient client = tcpListener.EndAcceptTcpClient(result);
            Debug.Log("Accepted connection from " + client.Client.RemoteEndPoint);

            // Get the server's name
            string serverName = System.Environment.MachineName;

            // Respond to the client with the server's name
            NetworkStream stream = client.GetStream();
            byte[] responseBytes = Encoding.UTF8.GetBytes(serverName);
            stream.Write(responseBytes, 0, responseBytes.Length);

            // Close the client connection
            client.Close();

            // Continue accepting more client connections
            tcpListener.BeginAcceptTcpClient(AcceptClientCallback, null);
        }
        catch (Exception e)
        {
            Debug.LogError("Error accepting or responding to TCP client: " + e.Message);
        }
    }

    private void OnApplicationQuit()
    {
        // Stop listening for client connections and close the listener when the application quits
        tcpListener.Stop();
    }
}

