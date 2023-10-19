using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    // ----------------------------------- Username
    public TMP_Text usernameText;
    public TMP_InputField usernameInput;

    // ----------------------------------- IP
    public TMP_Text ipText;
    public TMP_InputField ipInput;

    // ----------------------------------- ROOM NAME
    public TMP_Text roomText;
    public TMP_InputField roomInput;

    public void SaveUsernameHost()
    {
        UpdatedText.HostUsernameString = usernameText.text;
        Debug.Log("Nombre de usuario: " + usernameInput.text);
    }
    public void SaveUsernameClient()
    {
        UpdatedText.ClientUsernameString = usernameText.text;
        Debug.Log("Nombre de usuario: " + usernameInput.text);
    }

    public void SaveIP()
    {
        ClientConnection.ipAdress = ipInput.text;

        Debug.Log("Nombre de usuario: " + ipInput.text);
    }
    public void SaveRoomName()
    {
        UpdatedText.roomNameString = roomInput.text;
        Debug.Log("Nombre de usuario: " + roomInput.text);
    }
}
