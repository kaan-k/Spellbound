using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu; // Reference to the Main Menu GameObject

    void Start()
    {
        // Hide the main menu at the start
        mainMenu.SetActive(false);
    }

    void Update()
    {
        // Check if any key is pressed
        if (Input.anyKeyDown)
        {
            // Show the main menu
            ShowMainMenu();
        }
    }

    // Function to show the main menu
    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
    }
}
