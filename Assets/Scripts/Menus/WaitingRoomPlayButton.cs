using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitingRoomPlayButton : MonoBehaviour
{
    public ServerConnection serverScript;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            /*if (serverScript.isClientConnected)
            {
                SceneManager.LoadScene("IntroScene");
            }*/
        }
    }
}
