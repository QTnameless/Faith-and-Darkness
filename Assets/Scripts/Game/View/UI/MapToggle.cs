using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ToggleMap : MonoBehaviour
{
    private Image mapImage; // Reference to the Image component
    private bool isMapActive; // Keeps track of the map state
    private InputAction toggleMapAction; // InputAction for toggling the map
    private AudioSource audioSource; // AudioSource for playing sound

    [SerializeField] private AudioClip sheetFlipSound; // Sound to play when toggling the map

    void Awake()
    {
        // Initialize the InputAction and bind it to the 'M' key
        toggleMapAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/m");
    }

    void OnEnable()
    {
        // Enable the InputAction and subscribe to its event
        toggleMapAction.Enable();
        toggleMapAction.performed += OnToggleMap;
    }

    void OnDisable()
    {
        // Disable the InputAction and unsubscribe from its event
        toggleMapAction.performed -= OnToggleMap;
        toggleMapAction.Disable();
    }

    void Start()
    {
        // Get the Image component attached to this GameObject
        mapImage = GetComponent<Image>();
        audioSource = GetComponent<AudioSource>();

        if (mapImage == null)
        {
            Debug.LogError("Image component not found on this GameObject! Please attach this script to the GameObject with the map Image.");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on this GameObject! Please attach an AudioSource component.");
            return;
        }

        // Start with the map hidden by setting its enabled state to false
        mapImage.enabled = false;
        isMapActive = false;
        Debug.Log("Map initialized and hidden.");
    }

    private void OnToggleMap(InputAction.CallbackContext context)
    {
        if (mapImage == null || audioSource == null) return;

        // Toggle the enabled state of the Image component
        isMapActive = !isMapActive;
        mapImage.enabled = isMapActive;

        // Play the sheet-flipping sound
        if (sheetFlipSound != null)
        {
            audioSource.PlayOneShot(sheetFlipSound);
        }

        // Pause or resume the game time
        Time.timeScale = isMapActive ? 0f : 1f;

        Debug.Log($"Map toggled. Current state: {(isMapActive ? "Visible, Time Paused" : "Hidden, Time Resumed")}");
    }
}