using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Windows;

public class ClientConnection : MonoBehaviour
{
    // --------------------- TCP
    Socket socketTCP;
    IPEndPoint ipepTCP;

    // --------------------- UDP
    Socket socketUDP;
    IPEndPoint ipepUDP;

    // --------------------- BOTH
    //byte[] data;


    public void ClientConnectionTCP()
    {
        socketTCP = new Socket(AddressFamily.InterNetwork,
                       SocketType.Stream, ProtocolType.Tcp);

        ipepTCP = new IPEndPoint(
                        IPAddress.Parse("10.0.53.17"), 9050); //IP del servidor

        Thread TCP_Thread = new Thread(JoinRoom_TCP);
        TCP_Thread.Start();
    }
    private void JoinRoom_TCP()
    {
        socketTCP.Connect(ipepTCP);

        string message = "I am connected!";

        socketTCP.Send(Encoding.ASCII.GetBytes(message));
        //Debug.Log(message);

    }

    public void ClientConnectionUDP()
    {
        socketUDP = new Socket(AddressFamily.InterNetwork,
                       SocketType.Dgram, ProtocolType.Udp);

        ipepUDP = new IPEndPoint(
               IPAddress.Parse("10.0.53.17"), 9050);

        byte[] data = new byte[1024];
        string welcome = "Hello, are you there?";
        data = Encoding.ASCII.GetBytes(welcome);
        socketUDP.SendTo(data, data.Length, SocketFlags.None, ipepUDP);

        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        EndPoint Remote = (EndPoint)sender;

        data = new byte[1024];
        int recv = socketUDP.ReceiveFrom(data, ref Remote);

    }


}
