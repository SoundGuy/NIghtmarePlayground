using UnityEngine;
using System.Collections;

public class CthulliScript : MonoBehaviour {

	public GameObject Player;
	bool ChasePlayer;

	void Start ()
	{
		this.transform.GetComponent<SpriteRenderer> ().enabled = false;
		ChasePlayer = false;
	}
	public void activateCthulli()
	{
		ChasePlayer = true;
	}
	

	void Update ()
	{
		if (ChasePlayer) 
		{
			this.transform.GetComponent<SpriteRenderer> ().enabled = true;
			transform.position = Vector2.MoveTowards (transform.position, Player.transform.position, 1*Time.deltaTime);
		}

	}


}
