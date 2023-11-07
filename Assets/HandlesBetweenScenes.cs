using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class HandlesBetweenScenes : MonoBehaviour
{
    private ServerConnection serverScript;

    // Start is called before the first frame update
    void Start()
    {
       serverScript = GameObject.Find("NetworkManager").GetComponent<ServerConnection>();

    }
    //public void KillSocketTCP()
    //{
    //    if (serverScript.isTCP)
    //    {
    //        try
    //        {
    //            serverScript.newsockTCP.Shutdown(SocketShutdown.Both);
    //            //serverScript.con.Close();
    //            serverScript.newsockTCP.Close(); // Cierra el socket
    //            serverScript.isTCP = false;

    //            Debug.Log("TCP Socket Killed");
    //        }
    //        catch (Exception e)
    //        {
    //            // Manejar cualquier excepción relacionada con el cierre del socket
    //            Debug.Log("Error al cerrar el socket: " + e.ToString());
    //        }
            
    //    }


    //}
    //public void KillSocketUDP()
    //{
    //    if (serverScript.isUDP)
    //    {
    //        try
    //        {
    //            serverScript.newsockUDP.Shutdown(SocketShutdown.Both);
    //            serverScript.newsockUDP.Close();
    //            serverScript.isUDP = false;

    //            Debug.Log("UDP Socket Killed");
    //        }
    //        catch (Exception e)
    //        {
    //            // Manejar cualquier excepción relacionada con el cierre del socket
    //            Debug.Log("Error al cerrar el socket: " + e.ToString());
    //        }

    //    }
    //}
}
