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
	AudioSource audio;
	public float clockAlarm;
	public int ClockCall;
	public Text clockText;
	private float clickClock;
	public AudioClip clockDing;
	public ClockTotal CurrentClock;

	void Start () {
		audio = GetComponent<AudioSource>();
		CurrentClock = new ClockTotal(0,0,0,0);
		ClockCall = 0;
		clockText.text= "Begin";
	}

	IEnumerator ClockPause() {
		print ("Waiting for a Second");
		yield return new WaitForSeconds(1);
		print("Waited one Second");
	}

	void Update () {
		clickClock += Time.deltaTime;
		ClockCall=Mathf.FloorToInt(clickClock); //Convert the clock to seconds
		CurrentClock.ClockDigits = Mathf.FloorToInt(ClockCall%10); //Counts digits 
		Debug.Log ("Clock Should Have Digits");
		clockText.text = "" + CurrentClock.ClockTenMinutes + CurrentClock.ClockMinutes + ":" + CurrentClock.ClockTens + CurrentClock.ClockDigits;
		SetClockText (); //Update the display text

		if ((clickClock>=clockAlarm)& (clickClock<=(clockAlarm+0.5) & !audio.isPlaying)){ //Alarm time limiter
			Debug.Log("Alarm was Triggered");
			audio.PlayOneShot(clockDing, 0.7F);//Play the sound
		}
		
		if(Input.GetKeyDown(KeyCode.Mouse0)& clickClock>clockAlarm){
			Debug.Log ("Clock was Reset");
			clickClock=0; //Reset the clock when mouse is clicked
		}
	}

	void SetClockText() {
	 		if (CurrentClock.ClockDigits == 9) {
			StartCoroutine(ClockPause());
			ClockPause();
			CurrentClock.ClockTens+=1;
			Debug.Log("Clock Should Have Tens");
			}

		if (CurrentClock.ClockTens == 9) {
			ClockPause ();
			CurrentClock.ClockMinutes+=1;
			CurrentClock.ClockTens=0;
			Debug.Log("Clock Should Have Minutes");
		}

		if (CurrentClock.ClockMinutes == 9) {
			ClockPause ();
			CurrentClock.ClockTenMinutes+=1;
			CurrentClock.ClockMinutes=0;
			Debug.Log("Clock Should Have Tens of Minutes");
		}
	}
}