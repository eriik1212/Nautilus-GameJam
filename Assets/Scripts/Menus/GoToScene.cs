using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GoToScene : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            GoToSceneFunct(1);
        }
    }
    public void GoToSceneFunct(int numScene)
    {
        SceneManager.LoadScene(numScene);
    }
}
