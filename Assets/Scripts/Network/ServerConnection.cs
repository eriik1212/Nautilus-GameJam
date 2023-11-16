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
using System.Net.WebSockets;

public class ServerConnection : MonoBehaviour
{

    static public bool isClientConnected = false;

    public Socket newsockTCP;
    public Socket con;

    public Socket newsockUDP;
    public IPEndPoint ipep;
    public EndPoint remote;

    private static ServerConnection instance;

    public bool isTCP = false;
    public bool isUDP = false;

    // --------------- Buttons
    private Button createRoomButton;

    public Serializer serializer;

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

    private void Start()
    {
        GameObject buttonObject = GameObject.Find("UDPButtonServer");
        if (buttonObject != null)
            createRoomButton=buttonObject.GetComponent<Button>();
        

        if (createRoomButton != null)
            createRoomButton.onClick.AddListener(CreateServerUDP);

    }

    //public void CreateServerTCP()   
    //{

    //    newsockTCP = new
    //       Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    //    IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);

    //    newsockTCP.Bind(ipep);
    //    newsockTCP.Listen(10);

    //    Debug.Log("Waiting for a client...");

    //    Thread threadServerTCP = new Thread(ReceiveClientTCP);
    //    threadServerTCP.Start();

    //    isTCP = true;

    //}

    //void ReceiveClientTCP()
    //{
    //    con = newsockTCP.Accept();
    //    Debug.Log("Connected!");

    //    byte[] clientMessage = new byte[1024];
    //    string clientUsername = "";
    //    int arraySize = 0;

    //    // ------------------------------------------------------------------ RECEIVE
    //    arraySize = con.Receive(clientMessage, 0, clientMessage.Length, 0);
    //    Array.Resize(ref clientMessage, arraySize);
    //    clientUsername = Encoding.Default.GetString(clientMessage);

    //    Debug.Log("CLIENT USERNAME: " + clientUsername);

    //    // ------------------------------------------------------------------ SEND
    //    byte[] data = new byte[1024];
    //    data = Encoding.ASCII.GetBytes(UpdatedText.roomNameString);
    //    con.Send(data);
    //}

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

        GameObject serializerObject = GameObject.Find("NetworkManagerWaitingRoom");
        if (serializerObject != null)
            serializer = serializerObject.GetComponent<Serializer>();

        // ------------------------------------------------------------------ RECEIVE
        byte[] data = new byte[2048];
        int recv = newsockUDP.ReceiveFrom(data, ref remote);
        Debug.Log("CLIENT USERNAME: " + Encoding.ASCII.GetString(data, 0, recv));
        isClientConnected = true;

        // ------------------------------------------------------------------ SEND
        string serverName = UpdatedText.roomNameString;
        data = Encoding.ASCII.GetBytes(serverName);
        newsockUDP.SendTo(data, data.Length, SocketFlags.None, remote);

        while (true)
        {
            byte[] dataX = new byte[2048];
            int recvX = newsockUDP.ReceiveFrom(dataX, ref remote);
            //Debug.Log("CLIENT USERNAME: " + Encoding.ASCII.GetString(dataX, 0, recvX));
            //Test
            Debug.Log("Data recibida en sevidor");
            serializer.DeserializeXML(dataX);
        }

    }



}
