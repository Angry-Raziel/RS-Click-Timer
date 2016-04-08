using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
public class MainClock : MonoBehaviour {

	public class ClockTotal{
		public int ClockDigits;
		public int ClockTens;
		public int ClockMinutes;
		public int ClockTenMinutes;
		
		public ClockTotal (int C10Ms, int CMs, int C10s, int Cs){
			C10Ms = ClockTenMinutes;
			CMs = ClockMinutes;
			C10s = ClockTens;
			Cs = ClockDigits;
		}
	}
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
	AudioSource audio;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
	public float clockAlarm;
	public int ClockCompare;
	public float clickClock;
	public AudioClip clockDing;
	public ClockTotal CurrentClock;

	void Start () {
		audio = GetComponent<AudioSource>();
		CurrentClock = new ClockTotal(0,0,0,0);
		ClockCompare = 0;
	}

	void Update () {
		clickClock += (Time.deltaTime);
		if (clickClock>ClockCompare){
			SecUpdate ();
			ClockCompare+=1;
		}

		if ((clickClock>=clockAlarm)& (clickClock<=(clockAlarm+0.5) & !audio.isPlaying)){ //Alarm time limiter
			Debug.Log("Alarm was Triggered");
			audio.PlayOneShot(clockDing, 0.7F);//Play the sound
		}
		
		if(Input.GetKeyDown(KeyCode.Mouse0)& clickClock>clockAlarm){
			Debug.Log ("Clock was Reset");
			ClockCompare=0;
			clickClock=0;
			CurrentClock = new ClockTotal(0,0,0,0);
		}
	}

	void SecUpdate (){
		CurrentClock.ClockDigits = Mathf.RoundToInt (clickClock%10); //Counts digits 
       // clockText.text = "" + CurrentClock.ClockTenMinutes + CurrentClock.ClockMinutes + ":" + CurrentClock.ClockTens + CurrentClock.ClockDigits;
        Make7Seg();
		Debug.Log ("Clock Should Have Digits");
        SetClockText(); //Update the display text
            }

	void SetClockText() {
	 		if (CurrentClock.ClockDigits == 9) {
			CurrentClock.ClockTens+=1;
			Debug.Log("Clock Should Have Tens");
			}

		if (CurrentClock.ClockTens == 6) {
			CurrentClock.ClockMinutes+=1;
			CurrentClock.ClockTens=0;
			Debug.Log("Clock Should Have Minutes");
		}

		if (CurrentClock.ClockMinutes == 9) {
			CurrentClock.ClockTenMinutes+=1;
			CurrentClock.ClockMinutes=0;
			Debug.Log("Clock Should Have Tens of Minutes");
		}
	}
	void Make7Seg(){
	
	}
}