using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManagement : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Serializer.playButtonPressed)
        {
            SceneManager.LoadScene("LevelScene");
        }
    }
}
