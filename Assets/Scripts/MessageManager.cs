using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    // ----------------------------------- Username
    //private TMP_Text usernameText;
    //public TMP_InputField usernameInput;

    // ----------------------------------- IP
    //private TMP_Text ipText;
    //public TMP_InputField ipInput;

    // ----------------------------------- ROOM NAME
    //private TMP_Text roomText;
    //public TMP_InputField roomInput;

    private Button createButtonUDP;

    private void Start()
    {
        createButtonUDP = GameObject.Find("UDPButton").GetComponent<Button>();
    }

    private void Update()
    {
        if(createButtonUDP != null)
        {
            createButtonUDP.onClick.AddListener(SaveUsernameHost);
            createButtonUDP.onClick.AddListener(SaveUsernameClient);
            createButtonUDP.onClick.AddListener(SaveRoomName);
            createButtonUDP.onClick.AddListener(SaveIP);
        }
    }

    public void SaveUsernameHost()
    {
        UpdatedText.HostUsernameString = GameObject.Find("UsernameHostText").GetComponent<TMP_Text>().text;
        //Debug.Log("Nombre del host: " + usernameInput.text);
    }
    public void SaveUsernameClient()
    {
        UpdatedText.ClientUsernameString = GameObject.Find("UsernameClientText").GetComponent<TMP_Text>().text;
        //Debug.Log("Nombre del cliente: " + usernameInput.text);
    }

    public void SaveIP()
    {
        ClientConnection.ipAdress = GameObject.Find("ipText").GetComponent<TMP_Text>().text;
        //Debug.Log("IP: " + ipInput.text);
    }
    public void SaveRoomName()
    {
        UpdatedText.roomNameString = GameObject.Find("RoomNameText").GetComponent<TMP_Text>().text;
        //Debug.Log("Nombre de la sala: " + roomInput.text);
    }
}
