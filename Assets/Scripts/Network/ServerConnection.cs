using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading;
using UnityEngine.tvOS;
using UnityEngine.UIElements;
using System.Net.WebSockets;
using UnityEditor.PackageManager;

public class ServerConnection : MonoBehaviour
{

    bool joinAble;

    Socket newsockTCP;
    Socket con;

    Socket newsockUDP;
    IPEndPoint ipep;
    EndPoint remote;

    // Start is called before the first frame update
    void Start()
    {
        bool joinAble = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateServerTCP()
    {

        newsockTCP = new
           Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);

        newsockTCP.Bind(ipep);
        newsockTCP.Listen(10);

        Debug.Log("Waiting for a client...");

        Thread threadServerTCP = new Thread(ReceiveClientTCP);
        threadServerTCP.Start();

    }

    void ReceiveClientTCP()
    {
        con = newsockTCP.Accept();
        Debug.Log("Connected!");

        byte[] clientMessage = new byte[1024];
        string data = "";
        int arraySize = 0;

        arraySize = con.Receive(clientMessage, 0, clientMessage.Length, 0);
        Array.Resize(ref clientMessage, arraySize);
        data = Encoding.Default.GetString(clientMessage);

        Debug.Log(data);

    }

    public void CreateServerUDP()
    {
        newsockUDP = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);
        ipep = new IPEndPoint(IPAddress.Any, 9050);
        newsockUDP.Bind(ipep);

        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        remote = (EndPoint)(sender);

        Debug.Log("Waiting for user...");

        Thread threadServerUDP = new Thread(ReceiveClientUDP);
        threadServerUDP.Start();



    }

    void ReceiveClientUDP()
    {
        byte[] data = new byte[2048];
        int recv = newsockUDP.ReceiveFrom(data, ref remote);
        Debug.Log("Message received from {0}:");
        Debug.Log(Encoding.ASCII.GetString(data, 0, recv));

        string message = "You are connected to server!";
        data = Encoding.ASCII.GetBytes(message);
        newsockUDP.SendTo(data, data.Length, SocketFlags.None, remote);


    }
}
