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
using UnityEngine.tvOS;


public class ServerConnection : MonoBehaviour
{

    bool joinAble;

    Socket newsock;
    Socket con;


    // Start is called before the first frame update
    void Start()
    {
        bool joinAble = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateServerTCP()
    {

        newsock = new
           Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);

        newsock.Bind(ipep);
        newsock.Listen(10);

        Debug.Log("Waiting for a client...");

        Thread threadServerTCP = new Thread(ReceiveClientTCP);
        threadServerTCP.Start();




    }

    void ReceiveClientTCP()
    {

        if (joinAble)
        {
            con = newsock.Accept();
            Debug.Log("Connected!");
            joinAble = false;
        }

        else
        {
            con.Shutdown(SocketShutdown.Both);
            Debug.Log("Not able to connect.");
        }
            
    }
}
