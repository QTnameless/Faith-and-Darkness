using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CampFire : Clickable
{
    public Torch playerTorch; // Reference to the player's torch

    protected override void Start()
    {
        base.Start();
        // Initialize campfire-specific components
        playerTorch = FindObjectOfType<Torch>();
    }

    protected override void HandleClick()
    {
        // Logic to light the torch when the campfire is clicked
        if (playerTorch != null)
        {
            playerTorch.LightTorch();
        }
    }
}
