using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider), typeof(AudioSource))]
public class PowerUpBase : MonoBehaviour    
{
    public float DropSpeed = 5; 
    public AudioClip Sound; 

    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position += new Vector3(0, 0, -DropSpeed) * Time.deltaTime;
    }
    
    IEnumerator OnTriggerEnter(Collider other)
    {
        //Only interact with the player 
        if (other.name == "Player")
        {
            //Notify the derived powerups that its being picked up
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

    //Every powerup which derives from this class should implement this method!
    //Protected means this method is private and only visible to derived classes
    protected virtual void OnPickup()
    {
     
    }
}