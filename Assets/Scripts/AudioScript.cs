using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(wait(1f));
    }

    IEnumerator wait(float wait)
    {
        audioSource.Play();
        yield return new WaitForSeconds(wait);
        audioSource.PlayOneShot(audioClip);
    }
}
