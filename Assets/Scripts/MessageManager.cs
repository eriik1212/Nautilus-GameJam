using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    // ----------------------------------- Username HOST
    private TMP_Text usernameHostText;
    //public TMP_InputField usernameInput;

    // ----------------------------------- Username CLIENT
    private TMP_Text usernameClientText;
    //public TMP_InputField usernameInput;

    // ----------------------------------- IP
    private TMP_Text ipText;
    //public TMP_InputField ipInput;

    // ----------------------------------- ROOM NAME
    private TMP_Text roomText;
    //public TMP_InputField roomInput;

    private Button createButtonUDP;

    private void Start()
    {
        GameObject usernameHostObject = GameObject.Find("UsernameHostText");
        if (usernameHostObject != null)
            usernameHostText = usernameHostObject.GetComponent<TMP_Text>();

        GameObject usernameClientObject = GameObject.Find("UsernameClientText");
        if (usernameClientObject != null)
            usernameClientText = usernameClientObject.GetComponent<TMP_Text>();

        GameObject ipObject = GameObject.Find("ipText");
        if (ipObject != null)
            ipText = ipObject.GetComponent<TMP_Text>();

        GameObject roomObject = GameObject.Find("RoomNameText");
        if (roomObject != null)
            roomText = roomObject.GetComponent<TMP_Text>();

        GameObject udpButtonObject = GameObject.Find("UDPButton");
        if (udpButtonObject != null)
            createButtonUDP = udpButtonObject.GetComponent<Button>();
    }

    private void Update()
    {
        if(createButtonUDP != null)
        {
            if(usernameHostText != null)
                createButtonUDP.onClick.AddListener(SaveUsernameHost);

            if(usernameClientText != null)
                createButtonUDP.onClick.AddListener(SaveUsernameClient);

            if(ipText != null)
                createButtonUDP.onClick.AddListener(SaveRoomName);

            if(roomText != null)
                createButtonUDP.onClick.AddListener(SaveIP);
        }
    }

    public void SaveUsernameHost()
    {
        UpdatedText.HostUsernameString = usernameHostText.text;
        //Debug.Log("Nombre del host: " + usernameInput.text);
    }
    public void SaveUsernameClient()
    {
        UpdatedText.ClientUsernameString = usernameClientText.text;
        //Debug.Log("Nombre del cliente: " + usernameInput.text);
    }

    public void SaveIP()
    {
        ClientConnection.ipAdress = ipText.text;
        //Debug.Log("IP: " + ipInput.text);
    }
    public void SaveRoomName()
    {
        UpdatedText.roomNameString = roomText.text;
        //Debug.Log("Nombre de la sala: " + roomInput.text);
    }
}
