using UnityEngine;
using System.Collections;

public class CthulliScript : MonoBehaviour {
	Vector2 targetPos;
	float distToTarget;

	public GameObject world;
	public float VolumeSpeed;
	public GameObject player;
	public bool ChasePlayer;
	public float distanceFromPlayer;
	public float speed;
	static public Vector2 XLimit = new Vector2(-8, 8);
	static public Vector2 YLimit = new Vector2(-8, 8);
    private bool didDie = false;
	 
	void Start ()
	{
		SetRandomLocation ();
		ChasePlayer = false;
		StartCoroutine(Walk());
	}

	void Update ()
	{
		if (ChasePlayer) 
		{
			this.transform.GetComponent<SpriteRenderer> ().enabled = true;
			transform.position = Vector2.MoveTowards (transform.position, player.transform.position, speed *Time.deltaTime);
			if (this.GetComponent<AudioSource> ().volume < 1) 
			{
				increaseaudio ();
			}
		}
	}

	public void ActivateCthulli(GameObject newPlayer)
	{
		StopAllCoroutines ();
		player = newPlayer;
		ChasePlayer = true;
		AudioSource audio = GetComponent<AudioSource>();
		audio.Play();
        AudioScript worldAudio = world.GetComponent<AudioScript>();
        worldAudio.freakout = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player" && didDie == false)
        {
            AudioScript worldAudio = world.GetComponent<AudioScript>();
            worldAudio.freakout = true;
            worldAudio.VolumeSpeed *= 2;
            didDie = true;
            player.GetComponent<PlayerScript>().Die();
        }
    }

    void increaseaudio () 
	{
		AudioSource audio = GetComponent<AudioSource>();
		//Debug.Log ("cthulli is on");
		float VolumeIncrease = Time.deltaTime * VolumeSpeed;
		audio.volume += VolumeIncrease;
	}

	private void SetRandomLocation(){
		Vector3 distance = new Vector3 (distanceFromPlayer, distanceFromPlayer);
		int rand = Random.Range (-180, 180);
		transform.position = Quaternion.Euler(0, 0, rand) * distance;
	}

	IEnumerator Walk()
	{
		//anim.SetBool("isWalking", true);
		targetPos = SetRandomPos();
		/* Vector2 direction = (targetPos - (Vector2)transform.position).normalized;
        transform.up = direction;*/
		distToTarget = Vector2.Distance(transform.position, targetPos);

		while (distToTarget >= 2)
		{
			transform.position = Vector2.MoveTowards(transform.position, targetPos, speed*Time.deltaTime);
			if (transform.position.y < targetPos.y)
			{
				//anim.SetBool("Y", true);
			}
			else {
				//anim.SetBool("Y", false);
			}
			if (transform.position.x < targetPos.x)
			{
				GetComponent<SpriteRenderer>().flipX = true;
			}
			else
			{
				GetComponent<SpriteRenderer>().flipX = false;
			}
			distToTarget = Vector3.Distance(transform.position, targetPos);
			yield return null;
		}
		StartCoroutine(Wait());
	}

	IEnumerator Wait()
	{
		//anim.SetBool("isWalking", false);
		yield return new WaitForSeconds(Random.Range(1, 3));
		StartCoroutine(Walk());
	}

	Vector2 SetRandomPos()
	{
		Vector2 pos = new Vector3(Random.Range(XLimit.x, XLimit.y), Random.Range(YLimit.x, YLimit.y));
		return pos;
	}
}
