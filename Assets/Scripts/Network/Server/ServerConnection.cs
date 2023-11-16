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
using UnityEditor.PackageManager;

public class ServerConnection : MonoBehaviour
{
    
    public bool isClientConnected = false;
   
    public Socket newsockTCP;
    public Socket con;

    public Socket newsockUDP;
    public IPEndPoint ipep;
    private string clientIP;
    IPEndPoint remoteIpEndPoint;
    public EndPoint remote;

    private static ServerConnection instance;

    public bool isTCP = false;
    public bool isUDP = false;

    public bool serverCreated = false;

    // --------------- Buttons
    //private Button createRoomButton;

    public Serializer serializer;
    private ServerDataSender hostDataSend;


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
        //GameObject buttonObject = GameObject.Find("UDPButtonServer");
        //if (buttonObject != null)
        //    createRoomButton = buttonObject.GetComponent<Button>();


        //if (createRoomButton != null)
        //    createRoomButton.onClick.AddListener(CreateServerUDP);

        serverCreated = false;

    }

    private void Update()
    {
        if ((SceneManager.GetActiveScene().name == "WaitingRoom") && serializer == null)
        {
            GameObject serializerObject = GameObject.Find("NetworkManagerWaitingRoom");
            if (serializerObject != null)
                serializer = serializerObject.GetComponent<Serializer>();
        }

        if ((SceneManager.GetActiveScene().name == "WaitingRoom") && serializer != null && !serverCreated)
        {
            CreateServerUDP();
            serverCreated = true;
        }

        if ((SceneManager.GetActiveScene().name == "WaitingRoom") && hostDataSend == null)
        {
            GameObject hostDataObj = GameObject.Find("DataSender");
            if (hostDataObj != null)
                hostDataSend = hostDataObj.GetComponent<ServerDataSender>();
        }

        if (isClientConnected)
        {
            // Obtener la dirección IP del cliente
            if (remote is IPEndPoint)
            {
                remoteIpEndPoint = (IPEndPoint)remote;
                clientIP = remoteIpEndPoint.Address.ToString();
                Debug.Log("Client connected from IP: " + clientIP);
            }

            hostDataSend.SetInfo(newsockUDP, remoteIpEndPoint);
            hostDataSend.SendInfo(serializer);
        }

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

        isUDP = true;
    }

    void ReceiveClientUDP()
    {

        // ------------------------------------------------------------------ RECEIVE
        byte[] data = new byte[2048];
        int recv = newsockUDP.ReceiveFrom(data, ref remote);
        Debug.Log("CLIENT USERNAME: " + Encoding.ASCII.GetString(data, 0, recv));
        isClientConnected = true;

        // ------------------------------------------------------------------ SEND
        string serverName = UpdatedText.roomNameString;
        data = Encoding.ASCII.GetBytes(serverName);
        newsockUDP.SendTo(data, data.Length, SocketFlags.None, remote);

        //while (true)
        {
            byte[] dataX = new byte[2048];
            int recvX = newsockUDP.ReceiveFrom(dataX, ref remote);
            //Debug.Log("CLIENT USERNAME: " + Encoding.ASCII.GetString(dataX, 0, recvX));
            //Test
            Debug.Log("Data recibida en sevidor");

            if(serializer != null)
                serializer.DeserializeClientDataXML(dataX);
        }

    }



}
