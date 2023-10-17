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
    //private TcpListener tcpListener;
    //private int serverPort = 12345; // Choose a port number

    //private void Start()
    //{
    //    try
    //    {
    //        tcpListener = new TcpListener(IPAddress.Any, serverPort);
    //        tcpListener.Start();
    //        Debug.Log("TCP server started on port " + serverPort);

    //        // Start accepting client connections asynchronously
    //        tcpListener.BeginAcceptTcpClient(AcceptClientCallback, null);
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError("Error starting TCP server: " + e.Message);
    //    }
    //}

    //private void AcceptClientCallback(IAsyncResult result)
    //{
    //    try
    //    {
    //        TcpClient client = tcpListener.EndAcceptTcpClient(result);
    //        Debug.Log("Accepted connection from " + client.Client.RemoteEndPoint);

    //        // Get the server's name
    //        string serverName = System.Environment.MachineName;

    //        // Respond to the client with the server's name
    //        NetworkStream stream = client.GetStream();
    //        byte[] responseBytes = Encoding.UTF8.GetBytes(serverName);
    //        stream.Write(responseBytes, 0, responseBytes.Length);

    //        // Close the client connection
    //        client.Close();

    //        // Continue accepting more client connections
    //        tcpListener.BeginAcceptTcpClient(AcceptClientCallback, null);
    //    }
    //    catch (Exception e)
    //    {
    //        Debug.LogError("Error accepting or responding to TCP client: " + e.Message);
    //    }
    //}

    //private void OnApplicationQuit()
    //{
    //    // Stop listening for client connections and close the listener when the application quits
    //    tcpListener.Stop();
    //}

    public static void Main()
    {
        int recv;
        byte[] data = new byte[1024];
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any,
                               9050);

        Socket newsock = new
            Socket(AddressFamily.InterNetwork,
                        SocketType.Stream, ProtocolType.Tcp);

        newsock.Bind(ipep);
        newsock.Listen(10);
        Console.WriteLine("Waiting for a client...");
        Socket client = newsock.Accept();
        IPEndPoint clientep =
                     (IPEndPoint)client.RemoteEndPoint;
        Console.WriteLine("Connected with {0} at port {1}",
                        clientep.Address, clientep.Port);


        string welcome = "Welcome to my test server";
        data = Encoding.ASCII.GetBytes(welcome);
        client.Send(data, data.Length,
                          SocketFlags.None);
        while (true)
        {
            data = new byte[1024];
            recv = client.Receive(data);
            if (recv == 0)
                break;

            Console.WriteLine(
                     Encoding.ASCII.GetString(data, 0, recv));
            client.Send(data, recv, SocketFlags.None);
        }
        Console.WriteLine("Disconnected from {0}",
                          clientep.Address);
        client.Close();
        newsock.Close();
    }
}

