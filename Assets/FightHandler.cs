using UnityEngine;
using System.Collections;

public class FightHandler : MonoBehaviour {
	public PlayerScript player;
	public EnemyScript enemy;
	private int numberOfButtons = 3;
	char[] Buttons = {'n', 'm', 'p', 'v' };

	public double guessTime = 5;
	public bool isFighting = false;
	private int tries;
    private bool canTry = false;

	private double currentGuessTime;
	private char currentButton;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (isFighting && canTry) {
            identifyButton();
			if (currentGuessTime > 0) {
				currentGuessTime -= Time.deltaTime;
				if (currentGuessTime <= 0) {
					FailGuess ();
				}
			}
		}
        //Debug.Log(currentGuessTime);
	}

    private void identifyButton()
    {
        if(Input.GetKeyDown(KeyCode.M)) {
            Guess(1);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            Guess(2);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            Guess(3);
        }
    }

    public void InitiateBattle(EnemyScript newEnemy, PlayerScript p)
    {
        if (isFighting == false)
        {
            isFighting = true;
            player = p;
            enemy = newEnemy;
            enemy.isFighting = true;
            enemy.Stop();
            player.StartBattle();
            //enemy.isEngaged = true;
            tries = 3;
            //Player.engage
            StartTurn();
        }
	}

    private void StartTurn()
    {
        
        player.Runes.GetComponent<Animator>().SetInteger("Letter Index", 0);
        /*
        if (player.Runes.GetComponent<Animator>().GetInteger("Outcome") == -1)
        {
            player.Runes.GetComponent<Animator>().SetInteger("Outcome", 0);
        }*/
        canTry = true;
        currentGuessTime = guessTime;
        int buttonNum = RandomizeButton();
        currentButton = Buttons[buttonNum];
        DisplayButton(buttonNum);
    }

    public void Guess(int guessedButton)
    {
        char x = Buttons[guessedButton];
        if (isFighting == true && currentGuessTime > 0)
        {
            canTry = false;
            if (currentButton == x)
            {
                SucceedGuess();
            }
            else {
                FailGuess();
            }
        }
    }

    private void FailGuess(){
        canTry = false;
		tries--;
        player.FailPosses();
        if (tries <= 0)
        {
            FailBattle();
        }
        else {
            DislayFailedGuess();
            StartTurn();
        }
	}

	private void SucceedGuess(){
        canTry = false;
        if (enemy.Hit() == true)
        {
            WinBattle();
        }
        else {
            DisplaySucceededGuess();
            StartTurn();
        }
	}


	private int RandomizeButton(){
		int x = Random.Range (1, 4);
        Debug.Log(x);
		return x;
	}

	private void WinBattle(){
        isFighting = false;
        DisplayWonBattle(); // Perhaps halt is required
        player.Posses(enemy);
        // Playr. not fighting
        //enemy.isEngaged = false;
        //Player.Possess(foe)
        ///
    }

    private void DisplayButton(int button){
        player.Runes.GetComponent<Animator>().SetInteger("Letter Index", button);
        ///
    }

    public void FailBattle(){
        isFighting = false;
        enemy.Free();
        //player.FailPosses();
        DisplayFailedBattle();
        ///
    }

    private void DislayFailedGuess(){
        player.Runes.GetComponent<Animator>().SetInteger("Outcome", -1);
        //player.Runes.GetComponent<Animator>().SetInteger("Letter Index", 0);

        ///
    }

    private void DisplaySucceededGuess (){
        //player.Runes.GetComponent<Animator>().SetInteger("Outcome", 1);
        player.Runes.GetComponent<Animator>().SetTrigger("Do");



        ///
    }

    private void DisplayFailedBattle(){
        player.Runes.GetComponent<Animator>().SetInteger("Letter Index", 0);
        player.Runes.GetComponent<Animator>().SetInteger("Outcome", -1);


        ///
    }

    private void DisplayWonBattle() {
        player.Runes.GetComponent<Animator>().SetInteger("Letter Index", 0);
        player.Runes.GetComponent<Animator>().SetInteger("Outcome", 0);
        //player.Runes.GetComponent<Animator>().SetTrigger("Do");

    }
}
