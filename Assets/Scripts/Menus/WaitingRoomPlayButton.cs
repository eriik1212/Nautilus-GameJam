using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitingRoomPlayButton : MonoBehaviour
{
    private ServerConnection serverScript;
    private GameObject playButton;

    private void Start()
    {
        serverScript = GameObject.Find("NetworkManager").GetComponent<ServerConnection>();
        playButton = gameObject;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (serverScript.isClientConnected)
            {
                playButton.SetActive(true);
            }
        }
    }
}
