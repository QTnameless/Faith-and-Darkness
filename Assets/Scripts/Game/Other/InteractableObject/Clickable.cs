using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Clickable : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D hoverCursor;
    protected Vector2 Hotspot;
    public Transform player;

    public float detectionRadius = 2;
    protected bool hoverCursorOn = false;

    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        Hotspot = new Vector2(defaultCursor.width / 2f, defaultCursor.height / 2f);
    }

    private void OnMouseEnter()
    {
        if (IsPlayerClose())
        {
            Cursor.SetCursor(hoverCursor, Hotspot, CursorMode.Auto);
            hoverCursorOn = true;
        }
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(defaultCursor, Hotspot, CursorMode.Auto);
        hoverCursorOn = false;
    }

    private void OnMouseDown()
    {
        if (hoverCursorOn)
        {
            HandleClick();
        
        }
    }

    protected bool IsPlayerClose()
    {
        return Vector2.Distance(player.position, transform.position) < detectionRadius;
    }

    // Abstract method forces derived classes to implement  click behavior
    protected abstract void HandleClick();
}