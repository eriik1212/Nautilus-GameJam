using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitingRoomPlayButton : MonoBehaviour
{
    private ServerConnection serverScript;
    public GameObject playButton;

    private void Start()
    {
        serverScript = GameObject.Find("NetworkManagerServer").GetComponent<ServerConnection>();
        playButton = gameObject;
    }

    private void Update()
    {
        if (serverScript.isClientConnected)
        {
            playButton.SetActive(true);
        }
    }
}
