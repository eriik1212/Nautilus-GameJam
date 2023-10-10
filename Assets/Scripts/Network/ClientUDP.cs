using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;

public class UDPClient : MonoBehaviour
{
    private UdpClient client;
    private IPEndPoint serverEndPoint;

    static public InputField ipInputField;
    static public InputField usernameInputField;

    private string serverIP;
    private string playerName;


    private void Start()
    {
        client = new UdpClient();
        //serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
    }

    public void SetIP(string inputIP)
    {
        serverIP = inputIP;
        Debug.Log(inputIP);
    }

    public void SetPlayerName(string inputName)
    {
        playerName = inputName;
    }

    public void ConnectToServer()
    {
        
        string message = "CONNECT|" + playerName;
        byte[] data = Encoding.UTF8.GetBytes(message);
        Debug.Log("Trying connection");
        Debug.Log(serverIP);
        Debug.Log(playerName);
        //client.Send(data, data.Length, serverEndPoint);
    }
}
