using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;

public class UDPClient : MonoBehaviour
{
    // -------------------------------------------------- FIRST TRY
    //private UdpClient client;
    //private IPEndPoint serverEndPoint;

    //static public InputField ipInputField;
    //static public InputField usernameInputField;

    //private string serverIP;
    //private string playerName;


    //private void Start()
    //{
    //    client = new UdpClient();
    //    //serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
    //}

    //public void SetIP(string inputIP)
    //{
    //    serverIP = inputIP;
    //    Debug.Log(inputIP);
    //}

    //public void SetPlayerName(string inputName)
    //{
    //    playerName = inputName;
    //}

    //public void ConnectToServer()
    //{

    //    string message = "CONNECT|" + playerName;
    //    byte[] data = Encoding.UTF8.GetBytes(message);
    //    Debug.Log("Trying connection");
    //    Debug.Log(serverIP);
    //    Debug.Log(playerName);
    //    //client.Send(data, data.Length, serverEndPoint);
    //}

    public static void Main()
    {
        byte[] data = new byte[1024];
        string input, stringData;
        IPEndPoint ipep = new IPEndPoint(
                        IPAddress.Parse("127.0.0.1"), 9050);

        Socket server = new Socket(AddressFamily.InterNetwork,
                       SocketType.Dgram, ProtocolType.Udp);


        string welcome = "Hello, are you there?";
        data = Encoding.ASCII.GetBytes(welcome);
        server.SendTo(data, data.Length, SocketFlags.None, ipep);

        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        EndPoint Remote = (EndPoint)sender;

        data = new byte[1024];
        int recv = server.ReceiveFrom(data, ref Remote);

        Console.WriteLine("Message received from {0}:", Remote.ToString());
        Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

        while (true)
        {
            input = Console.ReadLine();
            if (input == "exit")
                break;
            server.SendTo(Encoding.ASCII.GetBytes(input), Remote);
            data = new byte[1024];
            recv = server.ReceiveFrom(data, ref Remote);
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            Console.WriteLine(stringData);
        }
        Console.WriteLine("Stopping client");
        server.Close();
    }
}
