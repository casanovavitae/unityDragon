using UnityEngine;

// Make sure there is always an AudioSource component on the GameObject where this script is added.
[RequireComponent(typeof(AudioSource))]

   public class PlaySoundOnHit_Simple : MonoBehaviour
{
    //Make the AudioClip configurable in the editor
    public AudioClip Sound;

    //OnCollisionEnter will only be called when one of the colliders has a rigidbody
    void OnCollisionEnter(Collision c)
    {
        AudioSource audio = GetComponent<AudioSource>();

        //Change the sound pitch if a slowdown powerup has been picked up
        audio.pitch = Time.timeScale;

        //Play it once for this collision hit
        audio.PlayOneShot(Sound);
    }
}

