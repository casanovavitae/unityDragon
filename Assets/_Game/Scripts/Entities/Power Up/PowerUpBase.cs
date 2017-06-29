using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider), typeof(AudioSource))]
public class PowerUpBase : MonoBehaviour    
{
    public float DropSpeed = 5; //How fast does it drop?
    public AudioClip Sound; //Sound played when the powerup is picked up

    void Start()
    {
        GetComponent<AudioSource>().playOnAwake = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.position += new Vector3(0, 0, -DropSpeed) * Time.deltaTime;
    }

    //Monobehaviour method, notice the IEnumerator which tells unity this is a coroutine
    IEnumerator OnTriggerEnter(Collider other)
    {
        //Only interact with the paddle
        if (other.name == "Paddle")
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