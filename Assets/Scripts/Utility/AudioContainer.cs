using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioContainer : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    AudioSource audioSource;

    void Awake() => audioSource = GetComponent<AudioSource>();
    
    public void PlayClip()
    {
        int choice = Random.Range(0, audioClips.Length);
        audioSource.PlayOneShot(audioClips[choice]);
    }
}
