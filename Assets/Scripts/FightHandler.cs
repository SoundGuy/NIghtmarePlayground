using UnityEngine;
using System.Collections;

public class FightHandler : MonoBehaviour {
	public PlayerScript player;
	public EnemyScript enemy;
    public FlameScript flame;

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

    public void cca(string title) {
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
			DisplayInRitual();
            StartTurn();
        }
	}

    private void StartTurn()
    {
        
        //player.Runes.GetComponent<Animator>().SetInteger("Letter Index", 0);
        /*
        if (player.Runes.GetComponent<Animator>().GetInteger("Outcome") == -1)
        {
            player.Runes.GetComponent<Animator>().SetInteger("Outcome", 0);
        }*/
        canTry = true;
        currentGuessTime = guessTime;
        int buttonNum = RandomizeButton();
        currentButton = Buttons[buttonNum];
        Debug.Log("Current button is " + currentButton);
        DisplayButton(currentButton.ToString());
    }

    public void Guess(int guessedButton)
    {
        Debug.Log("guessed button is" + guessedButton);
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
			this.player.GetComponent<Animator> ().SetBool ("in_ritual", false);
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
			this.player.GetComponent<Animator> ().SetBool ("in_ritual", false);

        }
        else {
            DisplaySucceededGuess();
            StartTurn();
        }
	}


	private int RandomizeButton(){
		int x = Random.Range (1, 4);
        Debug.Log("randomized button is " + x + "char: "+ Buttons[x]);
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

    public void FailBattle(){
        isFighting = false;
        enemy.Free();
        //player.FailPosses();
        DisplayFailedBattle();
        ///
    }

    private void DisplayButton(string button)
    {
        
        Debug.Log("display button is " + button);
        player.Runes.GetComponent<Animator>().SetTrigger(button);
    }

    private void DislayFailedGuess(){
        player.Runes.GetComponent<Animator>().SetTrigger("Fail");
    }

    private void DisplaySucceededGuess (){
        player.Runes.GetComponent<Animator>().SetTrigger("Do");
        flame.IncreaseFlame();
    }

    private void DisplayFailedBattle(){
        player.Runes.GetComponent<Animator>().SetTrigger("Fail");
        flame.DistinguishFlame();
    }

    private void DisplayWonBattle() {
        player.Runes.GetComponent<Animator>().SetTrigger("Do");
        flame.Succeed();
    }
	public void DisplayInRitual() {
		player.GetComponent<Animator> ().SetBool ("in_ritual", true);
		Debug.Log ("in ritual");
		}
}
