using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class Main : MonoBehaviour 
{
	[Header("Excel Data")]
	public DML excelData;

	public List<string> personnagesList;
	public List<string> actionsList;
	public List<string> contextesList;

	public Text personnageText;
	public Text actionText;
	public Text contexteText;

	[Header("Timer")]
	public Slider timerSlider;
	public Animator barreFond;
	public Text timerText;
	public Slider timerChoiceSlider;
	public float chosenTime;
	public float timerCurrentTime;


	[Header("Buttons")]
	public GameObject playButton;
	public GameObject pauseButton;
	public GameObject nouveauGrosButton;
	public GameObject nouveauButton;
	public GameObject quitterButton;

	public GameObject flecheGauche;
	public GameObject flecheDroite;

	public InputField bonusField;

	[Header("Screen Shake")]
	public RectTransform menu;
	public RectTransform sprites;
	public float shakeDuration = 0.5f;
	public Vector3 shakeStrenth = new Vector3 (1, 1, 0);
	public int shakeVibrato = 100;
	public float shakeRandomness = 45;

	[Header("Vibration Durations")]
	public long simpleInput = 10;
	public long wrongInput = 250;
	public long lastsSeconds = 400;
	public long theLastSecond = 2000;

	private bool shaking;

	private bool pause = false;
	private bool timerFinished = false;

	private bool vibrating;

	private bool five = false;
	private bool four = false;
	private bool three = false;
	private bool two = false;
	private bool one = false;
	private bool zero = false;

	// Use this for initialization
	void Start () 
	{
		DOTween.Init();

		GetExcelData ();

		chosenTime = 30;

		barreFond.StartPlayback ();
		barreFond.Play ("Barre Fond Animation", 0, 0.999f);
	}

	void GetExcelData ()
	{
		for (int i = 0; i < excelData.dataArray.Length; i++)
		{
			if(excelData.dataArray [i].Personnage != "")
				personnagesList.Add (excelData.dataArray [i].Personnage);
		}

		for (int i = 0; i < excelData.dataArray.Length; i++)
		{
			if(excelData.dataArray [i].Action != "")
				actionsList.Add (excelData.dataArray [i].Action);
		}

		for (int i = 0; i < excelData.dataArray.Length; i++)
		{
			if(excelData.dataArray [i].Contexte != "")
				contextesList.Add (excelData.dataArray [i].Contexte);
		}
	}

	void Update ()
	{
		if (timerCurrentTime > 0)
		{
			timerFinished = false;
		}
		else
		{
			timerFinished = true;

			playButton.SetActive (false);
			pauseButton.SetActive (false);

			nouveauGrosButton.SetActive (true);
			nouveauButton.SetActive (false);
		}

		if(timerCurrentTime <= 0)
		{
			ActivateFleches ();
		}

		if(shaking)
		{
			shaking = false;
			CameraShaking();
		}

		if(pauseButton.activeSelf == true)
			LastSeconds ();
	}

	void LastSeconds ()
	{
		switch (timerText.text)
		{
		case "0:05":
			
			if(!five)
			{
				five = true;
				Vibration.Vibrate (lastsSeconds);
				CameraShaking ();
			}

			break;
		case "0:04":
			if(!four)
			{
				four = true;
				Vibration.Vibrate (lastsSeconds);
				CameraShaking ();
			}

			break;
		case "0:03":
			if(!three)
			{
				three = true;
				Vibration.Vibrate (lastsSeconds);
				CameraShaking ();
			}

			break;
		case "0:02":
			if(!two)
			{
				two = true;
				Vibration.Vibrate (lastsSeconds);
				CameraShaking ();
			}

			break;
		case "0:01":
			if(!one)
			{
				one = true;
				Vibration.Vibrate (lastsSeconds);
				CameraShaking ();
			}

			break;
		case "0:00":
			if(!zero)
			{
				zero = true;
				Vibration.Vibrate (theLastSecond);
				CameraShaking ();
			}

			break;
		}
	}

	void ActivateFleches ()
	{
		if(chosenTime == 30)
		{
			flecheDroite.SetActive (true);
			flecheGauche.SetActive (false);
		}

		else if(chosenTime == 300)
		{
			flecheGauche.SetActive (true);
			flecheDroite.SetActive (false);
		}

		else
		{
			flecheDroite.SetActive (true);
			flecheGauche.SetActive (true);
		}
	}

	IEnumerator Timer ()
	{
		int minutes = (int) timerCurrentTime / 60; //Divide the guiTime by sixty to get the minutes.
		float seconds = timerCurrentTime % 60; //Use the euclidean division for the seconds.

		//update the label value
		timerText.text = string.Format ("{0:0}:{1:00}", minutes, seconds);

		//timerSlider.value = timerCurrentTime * 100 / chosenTime;
		if((timerCurrentTime / chosenTime) < 1)
			barreFond.Play ("Barre Fond Animation", 0, (timerCurrentTime / chosenTime));

		yield return new WaitForSeconds (0.1f);

		timerCurrentTime -= 0.1f;

		if (timerCurrentTime >= 0 && pause == false)
			StartCoroutine (Timer ());

	}

	public void TimeChoice ()
	{
		switch (timerChoiceSlider.value.ToString())
		{
		case "1":
			chosenTime = 30;
			break;
		case "2":
			chosenTime = 30 * 2;
			break;
		case "3":
			chosenTime = 30 * 3;
			break;
		case "4":
			chosenTime = 30 * 4;
			break;
		case "5":
			chosenTime = 30 * 5;
			break;
		case "6":
			chosenTime = 30 * 6;
			break;
		case "7":
			chosenTime = 30 * 7;
			break;
		case "8":
			chosenTime = 30 * 8;
			break;
		case "9":
			chosenTime = 30 * 9;
			break;
		case "10":
			chosenTime = 30 * 10;
			break;
		}

		int minutes = (int) chosenTime / 60; //Divide the guiTime by sixty to get the minutes.
		float seconds = chosenTime % 60; //Use the euclidean division for the seconds.

		//update the label value
		timerText.text = string.Format ("{0:0}:{1:00}", minutes, seconds);
	}

	public void TimerUp ()
	{
		if(timerText.text == "0:00")
		{
			int minutes2 = (int) chosenTime / 60; //Divide the guiTime by sixty to get the minutes.
			float seconds2 = chosenTime % 60; //Use the euclidean division for the seconds.

			//update the label value
			timerText.text = string.Format ("{0:0}:{1:00}", minutes2, seconds2);
		}

		chosenTime += 30;

		int minutes = (int) chosenTime / 60; //Divide the guiTime by sixty to get the minutes.
		float seconds = chosenTime % 60; //Use the euclidean division for the seconds.

		//update the label value
		timerText.text = string.Format ("{0:0}:{1:00}", minutes, seconds);

		ActivateFleches ();

		timerCurrentTime = chosenTime;
	}

	public void TimerDown ()
	{
		if(timerText.text == "0:00")
		{
			int minutes2 = (int) chosenTime / 60; //Divide the guiTime by sixty to get the minutes.
			float seconds2 = chosenTime % 60; //Use the euclidean division for the seconds.

			//update the label value
			timerText.text = string.Format ("{0:0}:{1:00}", minutes2, seconds2);
		}
			

		chosenTime -= 30;

		int minutes = (int) chosenTime / 60; //Divide the guiTime by sixty to get the minutes.
		float seconds = chosenTime % 60; //Use the euclidean division for the seconds.

		//update the label value
		timerText.text = string.Format ("{0:0}:{1:00}", minutes, seconds);

		ActivateFleches ();

		timerCurrentTime = chosenTime;
	}

	public void RandomWords ()
	{
		string personnageTemp;
		string actionTemp;
		string contexteTemp;

		do
		{
			personnageTemp = personnagesList [UnityEngine.Random.Range (0, personnagesList.Count)];
		}
		while(personnageTemp == personnageText.text);

		do
		{
			actionTemp = actionsList [UnityEngine.Random.Range (0, actionsList.Count)];
		}
		while(actionTemp == actionText.text);

		do
		{
			contexteTemp = contextesList [UnityEngine.Random.Range (0, contextesList.Count)];
		}
		while(contexteTemp == contexteText.text);


		personnageText.text = personnageTemp;

		actionText.text = actionTemp;

		contexteText.text = contexteTemp;
	}

	public void Pause ()
	{
		pause = true;

		playButton.SetActive (true);
		pauseButton.SetActive (false);

		quitterButton.SetActive (true);
		nouveauButton.SetActive (true);

		CameraShaking ();
	}

	public void Play ()
	{
		pause = false;

		playButton.SetActive (false);
		pauseButton.SetActive (true);

		flecheDroite.SetActive (false);
		flecheGauche.SetActive (false);

		quitterButton.SetActive (false);
		nouveauButton.SetActive (false);

		StartCoroutine (Timer ());

		CameraShaking ();
	}

	public void New ()
	{
		timerCurrentTime = chosenTime;

		bonusField.text = "";

		if(nouveauGrosButton.activeSelf == true)
		{
			nouveauGrosButton.SetActive (false);
			nouveauButton.SetActive (true);
		}

		playButton.SetActive (true);
		pauseButton.SetActive (false);

		ActivateFleches ();

		int minutes = (int) chosenTime / 60; //Divide the guiTime by sixty to get the minutes.
		float seconds = chosenTime % 60; //Use the euclidean division for the seconds.

		//update the label value
		timerText.text = string.Format ("{0:0}:{1:00}", minutes, seconds);

		//timerSlider.value = 100;
		barreFond.Play ("Barre Fond Animation", 0, 0.999f);

		RandomWords ();

		CameraShaking ();
	}

	public void Quit ()
	{
		Application.Quit ();
	}

	public void VibrateSimple ()
	{
		Vibration.Vibrate (simpleInput);
	}

	public void CameraShaking ()
	{
		shaking = false;

		if(!DOTween.IsTweening("Shake"))
		{
			menu.DOShakePosition(shakeDuration, shakeStrenth, shakeVibrato, shakeRandomness).SetId("Shake");
			sprites.DOShakePosition(shakeDuration, shakeStrenth, shakeVibrato, shakeRandomness).SetId("Shake");
		}
	}

}

