using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class SoundTests: MonoBehaviour {
	public AudioClip boop;
	AudioSource audio;
	
	void Start() {
		audio = GetComponent<AudioSource>();
		//boop = (AudioClip) Resources.Load("Assets/computer-boop.wav");
	}

	void Update() {
	if( Input.GetKeyDown( KeyCode.Mouse0 )){
		Debug.Log( "Mouse 0 Was Pressed." );
		audio.PlayOneShot(boop, 0.7F);
		}
	}
}