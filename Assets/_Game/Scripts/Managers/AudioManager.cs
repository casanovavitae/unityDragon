using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//has helper methods as well as sound settings. Each object has its own sounds and should get 
public class AudioManager : MonoBehaviour {

	public float musicVolume;
	float m_soundVolume = 0;
	public float soundVolume {
		get
		{
			return m_soundVolume;
		}
		set
		{
			m_soundVolume = value;
			AudioListener.volume = value;
		}
	}
	


	void Awake() {
		Global.audioManager = this;
		
	}

	// Use this for initialization
	void Start () {
		AudioListener.volume = soundVolume;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator DestroyWithSound(GameObject caller, AudioSource sourceToPlayFrom) {
		sourceToPlayFrom.Play();
		
		yield return new WaitForSeconds(sourceToPlayFrom.clip.length);

		Destroy(caller);
	}
}
