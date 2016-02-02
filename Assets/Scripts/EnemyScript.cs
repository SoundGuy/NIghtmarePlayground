using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    Vector2 targetPos;
    float distToTarget;
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
        targetPos = SetRandomPos();
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
        return pos;
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
}
