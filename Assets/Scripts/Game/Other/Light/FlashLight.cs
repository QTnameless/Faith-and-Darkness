using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FlashLight: LightSource
{

    [SerializeField] private float flickerThreshold = 5f; // Battery level at which flickering starts
    [SerializeField] private float minFlickerTime = 0.05f; // Minimum time for flicker duration
    [SerializeField] private float maxFlickerTime = 0.2f;  // Maximum time for flicker duration

    private Coroutine flickerCoroutine; // Tracks the flickering coroutine


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip toggleSound;
    [SerializeField] private AudioClip batteryDepletedSound;
    protected override void Start()
    {
        base.Start();
        duration = maxDuration;
        ToggleLight(false); // Start the game with the flashlight off
    }

    private void Update()
    {
        HandleLightToggle();

        DrainBattery();
    }

    // Toggles the flashlight on/off using right mouse button
    private void HandleLightToggle()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse click toggles the flashlight
        {
            PlayToggleSound();

            if (isOn)
            {
                TurnLightOff();
            }
            else if (duration > 0f)
            {
                ToggleLight(true);
            }
        }
    }

    // Drains the flashlight battery over time
    private void DrainBattery()
    {
        if (isOn && duration > 0f)
        {
            duration -= drainRate * Time.deltaTime;
            duration = Mathf.Max(duration, 0f);

            // Turn off the flashlight when the battery is drained
            if (duration <= 0f)
            {
                TurnLightOff();
            }
            // Start flickering when battery is low
            else if (duration <= flickerThreshold && flickerCoroutine == null)
            {
                flickerCoroutine = StartCoroutine(FlickerLight());
            }
        }
    }

    // Adds duration to the flashlight when a battery is picked up
    public void AddBattery(float batteryDuration)
    {
        duration = Mathf.Min(duration + batteryDuration, maxDuration); // Cap duration at maxDuration
        if (duration > flickerThreshold && flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine); // Stop flickering if duration exceeds the threshold
            flickerCoroutine = null;
        }
    }

    // Turns the flashlight off
    private void TurnLightOff()
    {
        ToggleLight(false);

        // Stop the flickering effect
        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
            flickerCoroutine = null;
        }
        if (duration <= 0f)
        {
            PlayBatteryDepletedSound();
        }
    }

    // Coroutine for flickering effect
    private IEnumerator FlickerLight()
    {
        while (isOn && duration > 0f && duration <= flickerThreshold)
        {
            // Randomly toggle the light
            light2D.enabled = !light2D.enabled;

            // Wait for a random flicker duration
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));
        }

        // Ensure the light remains on if the battery hasn't run out
        light2D.enabled = isOn;
        flickerCoroutine = null; // End the flickering coroutine
    }
    private void PlayToggleSound()
    {
        audioSource.PlayOneShot(toggleSound);
    }
    private void PlayBatteryDepletedSound()
    {
        if (audioSource != null && batteryDepletedSound != null)
        {
            audioSource.PlayOneShot(batteryDepletedSound);
        }
    }
}

