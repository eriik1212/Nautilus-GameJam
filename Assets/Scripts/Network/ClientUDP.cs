/*using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;*/

//public class UDPClient : MonoBehaviour

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


using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UDPClient : MonoBehaviour
{
    void Start()
    {
        Task.Run(() => StartClient());
    }

    private void StartClient()
    {
        byte[] data = new byte[1024];
        string input, stringData;
        IPEndPoint ipep = new IPEndPoint(
                        IPAddress.Parse("10.0.103.47"), 9050);

        Socket server = new Socket(AddressFamily.InterNetwork,
                       SocketType.Dgram, ProtocolType.Udp);

        string welcome = "Hello, are you there?";
        data = Encoding.ASCII.GetBytes(welcome);
        server.SendTo(data, data.Length, SocketFlags.None, ipep);

        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        EndPoint Remote = (EndPoint)sender;

        data = new byte[1024];
        int recv = server.ReceiveFrom(data, ref Remote);

        if (recv > 0)
        {
            Console.WriteLine("Message received from {0}:", Remote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
        }
        else
            Debug.Log("No message received");

        while (true)
        {
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                if (input == "exit")
                    break;
                server.SendTo(Encoding.ASCII.GetBytes(input), ipep);
                data = new byte[1024];
                recv = server.ReceiveFrom(data, ref Remote);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                Console.WriteLine(stringData);
            }
        }
        Console.WriteLine("Stopping client");
        server.Close();
    }
}

