using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdatedText : MonoBehaviour
{
    // ----------------------------------- Room Name
    public TMP_Text roomNameText; 
    static public string roomNameString;

    // ----------------------------------- Username 1
    public TMP_Text HostUsernameText;
    static public string HostUsernameString;

    // ----------------------------------- Username 2
    public TMP_Text ClientUsernameText;
    static public string ClientUsernameString;

    private void OnEnable()
    {
        UpdateText();
    }

    private void Update()
    {
        UpdateText();
    }
    public void UpdateText()
    {
        if (Serializer.roomNameXML != null)
            roomNameText.text = Serializer.roomNameXML;
        else 
            roomNameText.text = roomNameString;

        if (Serializer.hostNameXML != null)
            HostUsernameText.text = Serializer.hostNameXML;
        else
            HostUsernameText.text = HostUsernameString;

        if (Serializer.clientNameXML != null)
            ClientUsernameText.text = Serializer.clientNameXML;
        else
            ClientUsernameText.text = ClientUsernameString;

    }
}
