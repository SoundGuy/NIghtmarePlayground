﻿using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    
    [SerializeField] float speed;
    static public int fightDelay;
    [SerializeField] public Transform Runes;
    [SerializeField] public Transform kathulu;
    [SerializeField] public Transform Flame;
    EnemyScript currentEnemy;
	public bool isFreaked;


    Animator anim;
    enum Keys { K,P,R,C};
    [SerializeField]
    RuntimeAnimatorController enemy1Controller;
    [SerializeField]
    RuntimeAnimatorController enemy2Controller;
    [SerializeField]
    RuntimeAnimatorController enemy3Controller;
    [SerializeField]
    RuntimeAnimatorController enemy4Controller;
    Animator animator;
    [SerializeField]
    public GameObject g;
    private FightHandler fightHandler;
    private CthulliScript cthuluScript;
	//private AudioScript audioscript;
    Light View;

    public float viewThreshold;
	
	void Start ()
    {
        View = GetComponentInChildren<Light>();
        fightDelay = 1;
        fightHandler = g.GetComponent<FightHandler>();
        cthuluScript = kathulu.GetComponent<CthulliScript>();
    }

    void Update ()
    {
        MovementControll();
        LightControl();
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
        if (col.CompareTag("Enemy")){
            currentEnemy = col.GetComponent<EnemyScript>();
            if (Input.GetButtonDown("Jump") && (!currentEnemy.isFighting))
            {
                Debug.Log("TROOO");
                fightHandler.InitiateBattle(currentEnemy, this);
				fightHandler.DisplayInRitual ();
            }
        }
    }
    void LightControl()
    {
		
        View.range -= Time.deltaTime*viewThreshold;
        SetAmbient();
        if(View.range<=3.6f)
        {
            View.range = 3.6f;
            FreakOut();
        }
        else if (View.range >= 14){
            View.range = 14;
        
		}
    }
    
    private void SetAmbient()
    {
        if (RenderSettings.ambientIntensity >= 0)
        {
            RenderSettings.ambientIntensity -= Time.deltaTime * viewThreshold * 0.3f;
        }
    }

    private void FreakOut()
    {
		CthulliScript ktonce = kathulu.GetComponent<CthulliScript> ();
		if (ktonce.ChasePlayer == false)
		{
			cthuluScript.ActivateCthulli (this.gameObject);
		}
    }

    public void StartBattle()
    {
    }

    public void Posses(EnemyScript enemy)
    {
        Destroy(enemy.gameObject);
        View.range += 3;
        animator = GetComponent<Animator>();
        switch (enemy.type)
        {
            case 1:
                animator.runtimeAnimatorController = enemy1Controller;
                break;
            case 2:
                animator.runtimeAnimatorController = enemy2Controller;
                break;
            case 3:
                animator.runtimeAnimatorController = enemy3Controller;
                break;
        }
    }

    public void FailPosses()
    {
        View.range -= 1;
    }

    public void Die(){
        PlayDieSound();
        GetComponent<SpriteRenderer>().enabled = false;
        View.enabled = false;
        Destroy(this);
        ShowCredits();
    }

    private void PlayDieSound()
    {

        AudioSource audio = GetComponent<AudioSource>();
        audio.Play();
    }

    private void ShowCredits()
    {

    }
    /*void Kathulu()
    {
        Instantiate(kathulu,new Vector2())
    }*/

}
