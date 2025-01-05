using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;  // The original cursor texture
    [SerializeField] private Vector2 customHotspot;    // Optional, set in Inspector for custom hotspot
    private Vector2 cursorHotspot;

    // Start is called before the first frame update
    void Start()
    {
        // If no custom hotspot is defined, default to center of texture
        cursorHotspot = customHotspot == Vector2.zero ?
                        new Vector2(cursorTexture.width / 2, cursorTexture.height / 2) : customHotspot;

        // Set the custom cursor for this scene
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // OnDisable is called when the object is disabled or the scene changes
    private void OnDisable()
    {
        // Restore the system default cursor when leaving the scene or disabling the CursorManager
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset the cursor to the system default when a new scene is loaded
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
