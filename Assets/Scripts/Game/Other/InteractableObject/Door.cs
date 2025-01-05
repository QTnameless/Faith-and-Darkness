using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Door : Clickable
{
    public PlayerMovement playerMovement;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float attackDisableBriefly = 0.2f;
    // UnityEvent to assign actions via Unity Inspector
    public UnityEvent OnDoorClicked;


    protected override void Start()
    {
        base.Start();
        playerMovement = FindObjectOfType<PlayerMovement>();

    }

    private void PlayDoorOpenSound()
    {
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();
        tempAudioSource.clip = audioClip;
        tempAudioSource.Play();

        // Destroy the temporary GameObject after the audio finishes playing
        Destroy(tempAudio, audioClip.length);
    }
    // Overriding the abstract HandleClick method
    protected override void HandleClick()
    {
        PlayDoorOpenSound();
        playerMovement.EnableAttack(false);


        if (OnDoorClicked != null)
        {
            OnDoorClicked.Invoke(); // Trigger the event
        }
        else
        {
            Debug.LogWarning("No event assigned to OnDoorClicked.");
        }

        CoroutineHelper.Instance.RunCoroutine(HandleAfterPickup());
    }


    private IEnumerator HandleAfterPickup()
    {

        // Re-enable attack after cooldown
        yield return new WaitForSeconds(attackDisableBriefly);
        playerMovement.EnableAttack(true);


    }
}
