using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class VibrationManager : MonoBehaviour 
{
	public InputField vibrationIntensity;

	private long milliseconds;

	public void Vibrate ()
	{
		milliseconds = Convert.ToInt64 (vibrationIntensity.text.ToString ());

		StartCoroutine (VibrationTest ());
	}

	IEnumerator VibrationTest ()
	{
		Vibration.Vibrate (milliseconds);

		yield return null;
	}

	public void Vibrate10 ()
	{
		StartCoroutine (VibrationWait (10));
	}

	IEnumerator VibrationWait (long milliseconds)
	{
		Vibration.Vibrate (milliseconds);

		yield return new WaitForSeconds (milliseconds * 0.001f);

		Vibration.Cancel ();
	}
}
