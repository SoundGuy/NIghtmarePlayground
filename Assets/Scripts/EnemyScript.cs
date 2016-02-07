using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    Vector2 targetPos;
    float distToTarget;
	[SerializeField] float walkDistance;
    [SerializeField] float enemySpeed;
    [SerializeField] Animation[] fights;
    public Animator anim;
    static public Vector2 XLimit = new Vector2(-45, 45);
    static public Vector2 YLimit = new Vector2(-45, 45);

    [SerializeField]
    private int toughness;
    private int currentTough;

    public int type;
    public bool isFighting;

	void Start ()
    {
        currentTough = toughness;
        anim = GetComponent<Animator>();
        isFighting = false;
        StartCoroutine(Walk());
	}

    IEnumerator Walk()
    {
        anim.SetBool("isWalking", true);
		targetPos = SetRandomLocation();
       /* Vector2 direction = (targetPos - (Vector2)transform.position).normalized;
        transform.up = direction;*/
        distToTarget = Vector2.Distance(transform.position, targetPos);
       
        while (distToTarget >= 2)
        {
			transform.position = Vector2.MoveTowards(transform.position, targetPos, enemySpeed*Time.deltaTime);
            if (transform.position.y < targetPos.y)
            {
                anim.SetBool("Y", true);
            }
            else {
                anim.SetBool("Y", false);
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
        anim.SetBool("isWalking", false);
        yield return new WaitForSeconds(Random.Range(1, 3));
        StartCoroutine(Walk());
    }

    IEnumerator Fight()
    {
        StopAllCoroutines();
        yield return new WaitForSeconds(PlayerScript.fightDelay);
        
    }

    Vector2 SetRandomPos()
    {
        Vector2 pos = new Vector3(Random.Range(XLimit.x, XLimit.y), Random.Range(YLimit.x, YLimit.y));
		pos = pos / 5;
        return pos;
    }

	private Vector3 SetRandomLocation(){
		Vector3 distance = new Vector3 (walkDistance, walkDistance);
		int rand = Random.Range (-180, 180);
		distance = Quaternion.Euler(0, 0, rand) * distance;
		distance.x += transform.position.x;
		distance.y += transform.position.y;
		return distance;
	}

    public void Stop()
    {
        StopAllCoroutines();
        anim.SetBool("isWalking", false);

    }

    public void Free() {
        StartCoroutine(Walk());
        isFighting = false;
    }

    public bool Hit()
    {
        currentTough--;
        if (currentTough <= 0)
        {
            return true;
        }

        return false;
    }

	void OnTriggerStay2D(Collider2D other){
		// If other is layer dependant
		// If Im lower && other Layer greater - Accend layer order (SpriteRenderer>().sortingOrder +1)
		if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy") 
		{
			Debug.Log ("reached conditions");
			if(GetMinYValue(other.gameObject) > GetMinYValue(this.gameObject) &&
				other.gameObject.GetComponent<SpriteRenderer> ().sortingOrder >= gameObject.GetComponent<SpriteRenderer> ().sortingOrder) 
			{
				Debug.Log(GetMinYValue(other.gameObject));
				Debug.Log (GetMinYValue (this.gameObject));
				this.GetComponent<SpriteRenderer> ().sortingOrder += 1;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		// Set this layer to 0
		Debug.Log("exit?");
		this.GetComponent<SpriteRenderer> ().sortingOrder = 0;
	}

	private float GetMinYValue(GameObject o){
		float minY = o.GetComponent<SpriteRenderer> ().sprite.bounds.min.y;
		float yLocation = o.transform.position.y + minY;
		return yLocation;
	}
}
