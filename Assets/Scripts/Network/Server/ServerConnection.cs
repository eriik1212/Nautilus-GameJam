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
//using UnityEditor.PackageManager;

public class ServerConnection : MonoBehaviour
{
    
    public bool isClientConnected = false;
    public Socket newsockUDP;
    public IPEndPoint ipep;
    private string clientIP;

    private List<Socket> connectedClients = new List<Socket>();
    private List<Thread> clientThreads = new List<Thread>();

    private static ServerConnection instance;

    public bool isTCP = false;
    public bool isUDP = false;

    public bool serverCreated = false;

    // --------------- Buttons
    //private Button createRoomButton;
    private GameObject playButton;

    public Serializer serializer;
    private ServerDataSender hostDataSend;
    private bool dataSended = false;

    private float lastReceiveTime;
    private float timeoutDuration=15.5f;
    private float timeHandler;

    Thread threadServerUDP;


    // Testing del Boces que igual se borra
    IPEndPoint remoteIpEndPoint;
    public EndPoint remote;



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
        serverCreated = false;

    }

    private void Update()
    {
        timeHandler = Time.time;
        // PLAY BUTTON
        if ((SceneManager.GetActiveScene().name == "WaitingRoom") && playButton == null)
        {
            playButton = GameObject.Find("PlayButton");
            playButton.SetActive(false);
        }

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

        // LEVEL SCENE
        if ((SceneManager.GetActiveScene().name == "LevelScene") && serializer == null && isClientConnected)
        {
            GameObject serializerObject = GameObject.Find("PositionsHandler");
            if (serializerObject != null)
                serializer = serializerObject.GetComponent<Serializer>();
        }

        // PLAYER IS CONNECTED
        if (isClientConnected && !dataSended)
        {
            playButton.SetActive(true);

            // Obtener la dirección IP del cliente
            if (remote is IPEndPoint)
            {
                remoteIpEndPoint = (IPEndPoint)remote;
                clientIP = remoteIpEndPoint.Address.ToString();
                Debug.Log("Client connected from IP: " + clientIP);
            }

            hostDataSend.SetInfo(newsockUDP, remoteIpEndPoint);
            hostDataSend.SendInfo(serializer);

            dataSended = true;
        }

        if (hostDataSend != null && serializer != null && isClientConnected) 
        {

            // SERIALIZE INFO IN BUCLE
            hostDataSend.SendBucleInfoWaitingRoom(serializer);

            // DESERIALIZE IN BUCLE
            byte[] dataX = new byte[2048];
            int recvX = newsockUDP.ReceiveFrom(dataX, ref remote);

            serializer.PlayButtonDeserialize(dataX);

            //newsockUDP.Close();
        }

        if (SceneManager.GetActiveScene().name == "LevelScene" && isClientConnected)
        {

            // SERIALIZE INFO IN BUCLE
            hostDataSend.SendBoyData(serializer);

            // DESERIALIZE IN BUCLE
            byte[] dataX = new byte[2048];
            int recvX = newsockUDP.ReceiveFrom(dataX, ref remote);

            serializer.DeserializeGirlDataXML(dataX);
        }

        if (serverCreated)
        {
            


        }

        if (isClientConnected && SceneManager.GetActiveScene().name!= "WaitingRoom" && Time.time - lastReceiveTime > timeoutDuration)
        {
            Debug.Log("Cliente desconectado");
            isClientConnected = false;
            // Aquí puedes agregar la lógica para manejar la desconexión del cliente
            CloseSocket();// Cierra el socket cuando el cliente se desconecta
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

        threadServerUDP = new Thread(ReceiveClientUDP);
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

        lastReceiveTime = timeHandler;

        //while (true)
        {
            byte[] dataX = new byte[2048];
            int recvX = newsockUDP.ReceiveFrom(dataX, ref remote);
            //Debug.Log("CLIENT USERNAME: " + Encoding.ASCII.GetString(dataX, 0, recvX));
            //Test
            Debug.Log("Data recibida en sevidor");

            if(serializer != null)
                serializer.DeserializeClientDataXML(dataX);

            lastReceiveTime = timeHandler;
        }
        //newsockUDP.Close();

    }

    public void CloseSocket()
    {
        newsockUDP.Close();
    }

}
