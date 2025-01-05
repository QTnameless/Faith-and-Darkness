using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Torch : LightSource
{

    public UnityEvent onTorchLit; // Event triggered to light the torch
    public UnityEvent onTorchDepleted; // Event triggered when the torch burns out

    protected override void Start()
    {
        base.Start();
        TurnLightOff(); // Start with the torch off
    }

    private void Update()
    {
        if (isOn)
        {
            BurnTorch();
        }
    }

    // External method to light the torch
    public void LightTorch()
    {
        // Reset the torch's duration to max duration 
        duration = maxDuration;
        ToggleLight(true); // Turn on the light
         
        onTorchLit?.Invoke(); // Trigger the torch-lit event
    }

    private void BurnTorch()
    {
        if (duration > 0f)
        {
            duration -= drainRate * Time.deltaTime;
            duration = Mathf.Max(duration, 0f);

            // Optional: Gradually reduce light intensity as torch burns
            UpdateLightIntensity(Mathf.Lerp(0f, 1f, duration / maxDuration));
        }
        else if (duration ==0 )
        {
            TurnLightOff();
        }
    }

    private void TurnLightOff()
    {
         ToggleLight(false); // Turn off the light
         onTorchDepleted?.Invoke(); // Trigger the torch-depleted event
        
    }

}
