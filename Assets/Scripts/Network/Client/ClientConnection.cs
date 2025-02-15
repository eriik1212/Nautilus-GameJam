using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;
using static UnityEngine.InputSystem.InputRemoting;
public class ClientConnection : MonoBehaviour
{
    public bool connectionAlive = true;

    // --------------------- TCP
    Socket socketTCP;
    IPEndPoint ipepTCP;

    // --------------------- UDP
    Socket socketUDP;
    IPEndPoint ipepUDP;
    EndPoint remote;

    // --------------------- BUTTONS
    private Button connectUDPButton;
    private GameObject playButton;


    // --------------------- IP
    static public string ipAdress = "";

    private static ClientConnection instance;
    private Serializer seri;
    private ClientDataSender clientDataSend;

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
        connectUDPButton = GameObject.Find("UDPButton").GetComponent<Button>();

        if (connectUDPButton != null)
            connectUDPButton.onClick.AddListener(ClientConnectionUDP);

    }

    private void Update()
    {
        // PLAY BUTTON
        if ((SceneManager.GetActiveScene().name == "WaitingRoom") && playButton == null)
        {
            playButton = GameObject.Find("PlayButton");
            playButton.SetActive(false);
        }
        
        // SERIALIZER
        if ((SceneManager.GetActiveScene().name == "WaitingRoom") && seri == null)
        {
            GameObject serializerObject = GameObject.Find("NetworkManagerWaitingRoom");
            if (serializerObject != null)
                seri = serializerObject.GetComponent<Serializer>();
        }

        // CLIENT DATA SENDER
        if ((SceneManager.GetActiveScene().name == "WaitingRoom") && clientDataSend == null)
        {
            GameObject clientDataObj = GameObject.Find("DataSender");
            if (clientDataObj != null)
                clientDataSend = clientDataObj.GetComponent<ClientDataSender>();
        }

        // LEVEL SCENE
        if ((SceneManager.GetActiveScene().name == "LevelScene") && seri == null)
        {
            GameObject serializerObject = GameObject.Find("PositionsHandler");
            if (serializerObject != null)
                seri = serializerObject.GetComponent<Serializer>();
        }

        if (clientDataSend != null && seri != null && socketUDP.Connected && remote != null)
        {
            // SERIALIZE INFO IN BUCLE
            clientDataSend.SendBucleInfoWaitingRoom(seri);

            // DESERIALIZE IN BUCLE
            byte[] dataX = new byte[2048];
            int recvX = socketUDP.ReceiveFrom(dataX, ref remote);

            seri.PlayButtonDeserialize(dataX);
            //socketUDP.Close();
        }

        if (SceneManager.GetActiveScene().name == "LevelScene" && connectionAlive)
        {
            // SERIALIZE INFO IN BUCLE
            clientDataSend.SendGirlData(seri);

            // DESERIALIZE IN BUCLE
            byte[] dataX = new byte[2048];
            int recvX = socketUDP.ReceiveFrom(dataX, ref remote);

            seri.DeserializeBoyDataXML(dataX);
        }

            
    }

    public Socket socketInfo()
    {
        return socketUDP;
    } 
    public IPEndPoint ipInfo()
    {
        return ipepUDP;
    }
    public void ClientConnectionUDP()
    {
        socketUDP = new Socket(AddressFamily.InterNetwork,
                       SocketType.Dgram, ProtocolType.Udp);

        ipepUDP = new IPEndPoint(
              IPAddress.Parse(ipAdress), 9050);

        StartCoroutine(JoinRoom_UDP());

    }

    IEnumerator JoinRoom_UDP()
    {

        socketUDP.Connect(ipepUDP);

        yield return new WaitForSeconds(0.3f);

        if (socketUDP.Connected)
        {
            Debug.Log("Room Joined!");

            playButton.SetActive(true);

            // ------------------------------------------------------------------ SEND
            byte[] data = new byte[1024];
            string clientUsername = UpdatedText.ClientUsernameString;
            data = Encoding.ASCII.GetBytes(clientUsername);//What we send
            socketUDP.SendTo(data, data.Length, SocketFlags.None, ipepUDP);


            // ------------------------------------------------------------------ RECEIVE
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            remote  = (EndPoint)sender;

            data = new byte[1024];
            int recv = socketUDP.ReceiveFrom(data, ref remote);

            Debug.Log("You have connected to IP: " + remote.ToString() + " SERVER NAME: " + Encoding.ASCII.GetString(data, 0, recv));

            clientDataSend.SetInfo(socketUDP, ipepUDP);
            clientDataSend.SendInfo(seri);

            Thread threadReceiveDataUDP = new Thread(ReceiveXMLData);
            threadReceiveDataUDP.Start();

        }
        else
        {
            Debug.Log("Imposible to join.");
        }
    }

    void ReceiveXMLData()
    {
        // DESEARIALIZE
        //while (true)
        {
            byte[] dataX = new byte[2048];
            int recvX = socketUDP.ReceiveFrom(dataX, ref remote);
            Debug.Log("Data recibida en cliente");

            if (seri != null)
                seri.DeserializeHostDataXML(dataX);
        }
        //socketUDP.Close();

    }

    public void CloseSocket()
    {
        socketUDP.Close();
    }
}
