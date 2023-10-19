using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEditor.VersionControl;
using UnityEngine;
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


    public void ClientConnectionTCP()
    {
        socketTCP = new Socket(AddressFamily.InterNetwork,
                       SocketType.Stream, ProtocolType.Tcp);

        ipepTCP = new IPEndPoint(
                        IPAddress.Parse(ipAdress), 9050); //IP del servidor

        Thread TCP_Thread = new Thread(JoinRoom_TCP);
        TCP_Thread.Start();
    }
    private void JoinRoom_TCP()
    {
        socketTCP.Connect(ipepTCP);

        string message = "I am connected through TCP!";

        socketTCP.Send(Encoding.ASCII.GetBytes(message));
        //Debug.Log(message);

    }

    public void ClientConnectionUDP()
    {
        socketUDP = new Socket(AddressFamily.InterNetwork,
                       SocketType.Dgram, ProtocolType.Udp);

         ipepUDP = new IPEndPoint(
               IPAddress.Parse(ipAdress), 9050);

        //byte[] data = new byte[1024];
        //string welcome = "Hello, are you there? I am connected through UDP!";
        //data = Encoding.ASCII.GetBytes(welcome);
        //socketUDP.SendTo(data, data.Length, SocketFlags.None, ipepUDP);

        //IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        //EndPoint Remote = (EndPoint)sender;

        //data = new byte[1024];
        //int recv = socketUDP.ReceiveFrom(data, ref Remote);

        StartCoroutine(JoinRoom_UDP());
    }

    IEnumerator JoinRoom_UDP()
    {
        socketUDP.Connect(ipepUDP);

        yield return new WaitForSeconds(0.3f);

        if (socketUDP.Connected)
        {
            Debug.Log("Room Joined!");

            byte[] data = new byte[1024];
            string welcome = "Hello, are you there? I am connected through UDP!";
            data = Encoding.ASCII.GetBytes(welcome);
            socketUDP.SendTo(data, data.Length, SocketFlags.None, ipepUDP);

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint Remote = (EndPoint)sender;

            data = new byte[1024];
            int recv = socketUDP.ReceiveFrom(data, ref Remote);

            Debug.Log("You have connected to IP: " + Remote.ToString() + "IMPORTANT MESSAGE: " + Encoding.ASCII.GetString(data, 0, recv));

            //socketUDP.Send(Encoding.ASCII.GetBytes(welcome));
        }
        else
        {
            Debug.Log("Imposible to join.");
        }
    }
}
