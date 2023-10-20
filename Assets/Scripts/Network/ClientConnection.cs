using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.tvOS;
using UnityEngine.Windows;
using static UnityEngine.InputSystem.InputRemoting;

public class ClientConnection : MonoBehaviour
{
    // --------------------- TCP
    Socket socketTCP;
    IPEndPoint ipepTCP;

    // --------------------- UDP
    Socket socketUDP;
    IPEndPoint ipepUDP;

    // --------------------- IP
    static public string ipAdress = "";

    private static ClientConnection instance;

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

    public void ClientConnectionTCP()
    {
        socketTCP = new Socket(AddressFamily.InterNetwork,
                       SocketType.Stream, ProtocolType.Tcp);

        ipepTCP = new IPEndPoint(
                        IPAddress.Parse(ipAdress), 9050); //IP del servidor

        StartCoroutine(JoinRoom_TCP());
    }
    IEnumerator JoinRoom_TCP()
    {
        byte[] data = new byte[2048];
        socketTCP.Connect(ipepTCP);

        yield return new WaitForSeconds(0.3f);

        // ------------------------------------------------------------------ SEND
        string clientUsername = UpdatedText.ClientUsernameString;
        socketTCP.Send(Encoding.ASCII.GetBytes(clientUsername));

        // ------------------------------------------------------------------ RECEIVE
        Debug.Log("Socket conectado a IP: " +
            socketTCP.RemoteEndPoint.ToString());

        int bytesRec = socketTCP.Receive(data);
        string hostMessage = Encoding.ASCII.GetString(data, 0, bytesRec);

        Debug.Log("SERVER NAME: " + hostMessage);

        //socketTCP.Shutdown(SocketShutdown.Both);
        //socketTCP.Close();

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

            // ------------------------------------------------------------------ SEND
            byte[] data = new byte[1024];
            string clientUsername = UpdatedText.ClientUsernameString;
            data = Encoding.ASCII.GetBytes(clientUsername);
            socketUDP.SendTo(data, data.Length, SocketFlags.None, ipepUDP);


            // ------------------------------------------------------------------ RECEIVE
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)sender;

            data = new byte[1024];
            int recv = socketUDP.ReceiveFrom(data, ref Remote);

            Debug.Log("You have connected to IP: " + Remote.ToString() + " SERVER NAME: " + Encoding.ASCII.GetString(data, 0, recv));

        }
        else
        {
            Debug.Log("Imposible to join.");
        }
    }

}
