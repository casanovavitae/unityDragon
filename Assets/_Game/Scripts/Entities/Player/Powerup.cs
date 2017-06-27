using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(BoxCollider), typeof(AudioSource))]
public class Powerup : MonoBehaviour    
{
    public float DropSpeed = 5; 
    public AudioClip Sound; 

    void Start()
    {
     ///   GetComponent<AudioSource>().playOnAwake = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position += new Vector3(0, 0, -DropSpeed) * Time.deltaTime;
    }

    IEnumerator OnTriggerEnter(Collider other)
    {
        
        if (other.name == "Paddle")
        {
            OnPickup();

            //Prevent furthur collisions
            GetComponent<Collider>().enabled = false;
            GetComponent<Renderer>().enabled = false;

            AudioSource audio = GetComponent<AudioSource>();
            //Change the sound pitch if a slowdown powerup has been picked up
            audio.pitch = Time.timeScale;

            //Play audio and wait, without the wait the sound would be cutoff by the destroy
            audio.PlayOneShot(Sound);
            yield return new WaitForSeconds(Sound.length);
        }
    }

    protected virtual void OnPickup()
    {

    }
}