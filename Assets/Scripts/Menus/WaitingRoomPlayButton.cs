using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitingRoomPlayButton : MonoBehaviour
{
    private ServerConnection serverScript;

    private void Start()
    {
        serverScript = GameObject.Find("NetworkManager").GetComponent<ServerConnection>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (serverScript.isClientConnected)
            {
                SceneManager.LoadScene("IntroScene");
            }
        }
    }
}
