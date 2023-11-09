using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class DisplayServerIp : MonoBehaviour
{
    private TMP_Text ipText;

    // Start is called before the first frame update
    void Start()
    {
        ipText = GetComponent<TMP_Text>();

        string ipAddress = GetLocalIPAddress();
        Debug.Log("IP Address: " + ipAddress);
    }

    string GetLocalIPAddress()
    {
        string localIP = "";
        IPHostEntry host;
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }

    private void Update()
    {
        ipText.text = "IP: " + GetLocalIPAddress();
    }
}
