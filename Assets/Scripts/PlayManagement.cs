using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManagement : MonoBehaviour
{
    static public bool playPres = false;

    // Update is called once per frame
    void Update()
    {
        if(Serializer.playButtonPressed)
        {
            SceneManager.LoadScene("LevelScene");
        }
    }

    public void SetPlay()
    {
        playPres = true;
    }
}
