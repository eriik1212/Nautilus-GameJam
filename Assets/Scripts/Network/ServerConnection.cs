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

    public Socket newsockTCP;
    public Socket con;

    public Socket newsockUDP;
    public IPEndPoint ipep;
    public EndPoint remote;

    private static ServerConnection instance;

    public bool isTCP = false;
    public bool isUDP = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreateServerTCP()
    {

        newsockTCP = new
           Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);

        newsockTCP.Bind(ipep);
        newsockTCP.Listen(10);

        Debug.Log("Waiting for a client...");

        StartCoroutine(ReceiveClientTCP());

        isTCP = true;

    }

    IEnumerator ReceiveClientTCP()
    {
        con = newsockTCP.Accept();
        Debug.Log("Connected!");

        byte[] clientMessage = new byte[1024];
        string clientUsername = "";
        int arraySize = 0;

        // ------------------------------------------------------------------ RECEIVE
        arraySize = con.Receive(clientMessage, 0, clientMessage.Length, 0);
        Array.Resize(ref clientMessage, arraySize);
        clientUsername = Encoding.Default.GetString(clientMessage);

        Debug.Log("CLIENT USERNAME: " + clientUsername);


        // ------------------------------------------------------------------ SEND
        byte[] data = new byte[1024];
        data = Encoding.ASCII.GetBytes(UpdatedText.roomNameString);
        con.Send(data);

        yield return new WaitForSeconds(0.3f);


    }

    public void CreateServerUDP()
    {
        newsockUDP = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp);
        ipep = new IPEndPoint(IPAddress.Any, 9050);
        newsockUDP.Bind(ipep);

        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        remote = (EndPoint)(sender);

        Debug.Log("Waiting for user...");

        StartCoroutine(ReceiveClientUDP());

        isUDP = true;

    }

    IEnumerator ReceiveClientUDP()
    {
        // ------------------------------------------------------------------ RECEIVE
        byte[] data = new byte[2048];
        int recv = newsockUDP.ReceiveFrom(data, ref remote);
        Debug.Log("CLIENT USERNAME: " + Encoding.ASCII.GetString(data, 0, recv));

        // ------------------------------------------------------------------ SEND
        string serverName = UpdatedText.roomNameString;
        data = Encoding.ASCII.GetBytes(serverName);
        newsockUDP.SendTo(data, data.Length, SocketFlags.None, remote);

        yield return new WaitForSeconds(0.3f);

    }

}
