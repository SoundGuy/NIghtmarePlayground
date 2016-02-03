using UnityEngine;
using System.Collections;

public class FlameScript : MonoBehaviour {
	Animator anim;
	private int flameLevel;
	private string flameParameter = "Flame level";

	// Use this for initialization
	void Start () {
		anim=GetComponent<Animator>();
		flameLevel = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void IncreaseFlame(){
		flameLevel++;
		anim.SetInteger(flameParameter, flameLevel);
	}

	public void DistinguishFlame(){
		flameLevel = 0;
		anim.SetInteger(flameParameter, 0);
	}

	public void Succeed(){
		IncreaseFlame();
        StartCoroutine(Finish());
	}

	private IEnumerator Finish(){
        yield return new WaitForSeconds(1);
        DistinguishFlame();
	}
}
