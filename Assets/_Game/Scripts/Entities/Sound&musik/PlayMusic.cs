using UnityEngine;

//Make sure there is always an AudioSource component on the GameObject where this script is added.
[RequireComponent(typeof(AudioSource))]
public class PlayMusic : MonoBehaviour
{
    //Make the AudioClip configurable in the editor
    public AudioClip BackgroundMusic;

    //Cache the audio component for performance reasons (mostly on mobile)
    private AudioSource _localAudio;

    //Start is called one time when the scene has been loaded
    void Start()
    {
        _localAudio = this.GetComponent<AudioSource>(); //this.audio is the same as this.GetComponent<AudioSource>() which is bad to do on mobile every frame

        _localAudio.loop = true;
        _localAudio.clip = BackgroundMusic;
        _localAudio.Play();
    }

    void Update()
    {
        _localAudio.pitch = Time.timeScale;
    }
} 