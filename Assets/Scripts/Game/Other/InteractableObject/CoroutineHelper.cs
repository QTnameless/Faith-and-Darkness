using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    private static CoroutineHelper instance;
    private List<IEnumerator> activeCoroutines = new List<IEnumerator>();

    // Singleton instance
    public static CoroutineHelper Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject helperObject = new GameObject("CoroutineHelper");
                instance = helperObject.AddComponent<CoroutineHelper>();
                DontDestroyOnLoad(helperObject); // Keep it across scenes
            }
            return instance;
        }
    }

    // Public method to start a coroutine
    public void RunCoroutine(IEnumerator coroutine)
    {
        activeCoroutines.Add(coroutine);
        StartCoroutine(TrackedCoroutine(coroutine));
    }

    // Tracks and removes completed coroutines
    private IEnumerator TrackedCoroutine(IEnumerator coroutine)
    {
        yield return StartCoroutine(coroutine);
        activeCoroutines.Remove(coroutine);

        // Destroy the helper if no active coroutines exist
        if (activeCoroutines.Count == 0)
        {
            Destroy(gameObject);
            instance = null; // Reset the singleton instance
        }
    }
}