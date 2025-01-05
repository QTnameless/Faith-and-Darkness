using UnityEngine;
using System.Collections;

public class Battery : Clickable
    {
        public PlayerMovement playerMovement;
        public FlashLight playerFlashlight;
       
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private float attackDisableBriefly  = 0.2f;
   

    protected override void Start()
        {
            base.Start();
            playerFlashlight = FindObjectOfType<FlashLight>();
            playerMovement = FindObjectOfType<PlayerMovement>();
           
        }

        private void PlayPickupSound()
        {
        GameObject tempAudio = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();
        tempAudioSource.clip = audioClip;
        tempAudioSource.Play();

        // Destroy the temporary GameObject after the audio finishes playing
        Destroy(tempAudio, audioClip.length);
    }


    protected override void HandleClick()
    {
        PlayPickupSound();


        // Add battery to the flashlight
        playerFlashlight.AddBattery(20);

        // Temporarily disable player's attack
        playerMovement.EnableAttack(false) ;

        Cursor.SetCursor(defaultCursor, Hotspot, CursorMode.Auto);
        hoverCursorOn = false;
        // Start coroutine to handle cooldown and destruction
        CoroutineHelper.Instance.RunCoroutine(HandleAfterPickup());
        Destroy(gameObject);
    }

    private IEnumerator HandleAfterPickup()
    {
        
        // Re-enable attack after cooldown
        yield return new WaitForSeconds(attackDisableBriefly);
        playerMovement.EnableAttack(true);

        
    }
}

