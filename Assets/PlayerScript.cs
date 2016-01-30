using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

    [SerializeField] private Sprite Enemy1;
    [SerializeField] private Animation Enemy1A;
    [SerializeField] private Sprite Enemy2;
    [SerializeField] private Animation Enemy2A;
    [SerializeField] private Sprite Enemy3;
    [SerializeField] private Animation Enemy3A;

    
    [SerializeField] float speed;
    static public int fightDelay;
    [SerializeField] Animation[] fights;
    [SerializeField] private Transform kathulu;
    Transform mob;
    Animator anim;
    enum Keys { K,P,R,C};

    public float viewThreshold;
	
	void Start ()
    {
       
	}
	
	
	void Update ()
    {
        MovementControll();
        LightControl();
    }
    void Posses(EnemyScript enemy)
    {
        GetComponent<SpriteRenderer>().sprite = enemy.GetComponent<SpriteRenderer>().sprite;
        /* switch (enemy.type)
         {
             case 1:
                 Debug.Log("AERGARG");
                 GetComponent<SpriteRenderer>().sprite = enemy.GetComponent<SpriteRenderer>().sprite;
                 break;
             case 2:
                 GetComponent<SpriteRenderer>().sprite = enemy.GetComponent<SpriteRenderer>().sprite;
                 Debug.Log("AERGARG");
                 break;
             case 3:
                 GetComponent<SpriteRenderer>().sprite = Enemy3;
                 break;
             default: break;
         }*/
    }
    void MovementControll()
    {
        float localY = Input.GetAxis("Vertical");
        float localX = Input.GetAxis("Horizontal");
        transform.Translate(localX * Time.deltaTime * speed, localY * Time.deltaTime * speed, 0, Space.World);
        anim=GetComponent<Animator>();
        if(localX!=0||localY!=0)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }
        if (localY>0)
        {
            anim.SetBool("Y", true);
        }
        else
        {
            anim.SetBool("Y", false);
        }
        if(localX>0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (localX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Enemy")&&Input.GetButtonDown("Jump"))
        {
            Debug.Log("TROOO");
            Posses(col.GetComponent<EnemyScript>());
        }
    }
    void LightControl()
    {
        Light View = GetComponentInChildren<Light>();
        View.range -= Time.deltaTime*viewThreshold;
        if(View.range<=3.5f)
        {
            View.range = 3.5f;
        }
    }

    IEnumerator Fight()
    {
        yield return new WaitForSeconds(fightDelay);
        yield return StartCoroutine(RandomButtonAction());
    }
    IEnumerator RandomButtonAction()
    {
        return null;
    }
    /*void Kathulu()
    {
        Instantiate(kathulu,new Vector2())
    }*/

}
