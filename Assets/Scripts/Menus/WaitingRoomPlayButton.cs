using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitingRoomPlayButton : MonoBehaviour
{
    public GameObject playButton;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (ServerConnection.isClientConnected)
        {
            playButton.SetActive(true);
        }
    }
}
