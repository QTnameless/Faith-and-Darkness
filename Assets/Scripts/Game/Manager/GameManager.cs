using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        // Subscribe to the Escape key event
        EventManager.OnEscapePressed += HandleEscapePressed;
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        EventManager.OnEscapePressed -= HandleEscapePressed;
    }

    // Method to handle the Escape key press
    private void HandleEscapePressed()
    {
        LoadMainMenu();
    }

    // Load the main menu scene
    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Ensure "MainMenu" is the name of your main menu scene
    }

}
