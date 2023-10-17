/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;*/

//public class UDPServer : MonoBehaviour

// -------------------------------------------------- FIRST TRY
//private UdpClient udpServer;
//private int serverPort = 12345; // Choose a port number

//private void Start()
//{
//    udpServer = new UdpClient(serverPort);
//    Debug.Log("UDP server started on port " + serverPort);

//    // Start listening for incoming messages in the background
//    udpServer.BeginReceive(ReceiveCallback, null);
//}

//private void ReceiveCallback(IAsyncResult result)
//{
//    IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
//    byte[] receivedData;

//    try
//    {
//        receivedData = udpServer.EndReceive(result, ref clientEndPoint);
//        string clientMessage = System.Text.Encoding.UTF8.GetString(receivedData);
//        Debug.Log("Received message from " + clientEndPoint + ": " + clientMessage);

//        // Get the server's name
//        string serverName = System.Environment.MachineName;

//        // Respond to the client with the server's name
//        byte[] responseBytes = System.Text.Encoding.UTF8.GetBytes(serverName);
//        udpServer.Send(responseBytes, responseBytes.Length, clientEndPoint);

//        // Continue listening for more messages
//        udpServer.BeginReceive(ReceiveCallback, null);
//    }
//    catch (Exception e)
//    {
//        Debug.LogError("Error receiving or responding to UDP message: " + e.Message);
//    }
//}

//private void OnApplicationQuit()
//{
//    // Close the UDP socket when the application quits
//    udpServer.Close();
//}

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UDPReceiver : MonoBehaviour
{
    void Start()
    {
        Task.Run(() => StartReceiving());
    }

    private void StartReceiving()
    {
        int recv;
        byte[] data = new byte[1024];
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);

        Socket newsock = new Socket(AddressFamily.InterNetwork,
                        SocketType.Dgram, ProtocolType.Udp);

        newsock.Bind(ipep);
        Console.WriteLine("Waiting for a client...");

        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        EndPoint Remote = (EndPoint)(sender);

        IPAddress ipAddress = IPAddress.Parse("0.0.0.0");
        int port = 0; // puedes cambiar esto al número de puerto que desees
        IPEndPoint endPoint = new IPEndPoint(ipAddress, port);

        if (Remote != endPoint)
        {
            recv = newsock.ReceiveFrom(data, ref Remote);

            Console.WriteLine("Message received from {0}:", Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            string welcome = "Welcome to my test server";
            data = Encoding.ASCII.GetBytes(welcome);
            newsock.SendTo(data, data.Length, SocketFlags.None, Remote);
            while (true)
            {
                data = new byte[1024];
                recv = newsock.ReceiveFrom(data, ref Remote);

                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
                newsock.SendTo(data, recv, SocketFlags.None, Remote);
            }
        }
        else
        {
            Debug.Log("Sin jugadores");
        }
    }
}



