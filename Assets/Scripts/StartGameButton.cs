using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    // This function will be called when the button is clicked
    public void StartGame()
    {
        Debug.Log("TEST");
        // Replace "YourSceneName" with the actual name of the scene you want to load
        SceneManager.LoadScene("SampleScene");
    }
}
