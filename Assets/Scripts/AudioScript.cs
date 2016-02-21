using UnityEngine;
using System.Collections;

public class AudioScript : MonoBehaviour {

	public bool freakout; 
	public float VolumeSpeed;
	public AudioClip audio1;
	// Use this for initialization
	void Start () {
		AudioSource audio = GetComponent<AudioSource>();
		audio.clip = audio1;
		audio.Play ();


	}



	void FadeoutAudio () {
		AudioSource audio = GetComponent<AudioSource>();


			float VolumeDecrease = Time.deltaTime * VolumeSpeed;

			audio.volume -= VolumeDecrease;

	}

	void freakStart () {
	freakout = true;
	}


	
	// Update is called once per frame
	void Update () {

		if ( freakout == true  && GetComponent<AudioSource>().volume != 0) 
		{
		FadeoutAudio ();
			//Debug.Log ("im on");
		}
    }
}
