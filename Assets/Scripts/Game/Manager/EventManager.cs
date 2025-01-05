using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    // Define an event for the Escape key
    public static event Action OnEscapePressed;

    void Update()
    {
        // Detect the Escape key press
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscapePressed?.Invoke(); // Trigger the event
        }
    }
}
