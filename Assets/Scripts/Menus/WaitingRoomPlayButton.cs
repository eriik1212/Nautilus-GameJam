using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitingRoomPlayButton : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            GoToSceneFunct(2);
        }
    }
    public void GoToSceneFunct(int numScene)
    {
        SceneManager.LoadScene(numScene);
    }
}

//BUENA