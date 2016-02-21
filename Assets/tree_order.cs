using UnityEngine;
using System.Collections;

public class tree_order : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerStay2D(Collider2D other){
		// If other is layer dependant
		// If Im lower && other Layer greater - Accend layer order (SpriteRenderer>().sortingOrder +1)
		if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy") 
		{
			//Debug.Log ("reached conditions");
			if(GetMinYValue(other.gameObject) > GetMinYValue(this.gameObject) &&
				other.gameObject.GetComponent<SpriteRenderer> ().sortingOrder >= gameObject.GetComponent<SpriteRenderer> ().sortingOrder) 
			{
				//Debug.Log(GetMinYValue(other.gameObject));
				//Debug.Log (GetMinYValue (this.gameObject));
				this.GetComponent<SpriteRenderer> ().sortingOrder += 1;
			}
		}
	}

	private float GetMinYValue(GameObject o){
		float minY = o.GetComponent<SpriteRenderer> ().sprite.bounds.min.y;
		float yLocation = o.transform.position.y + minY;
		return yLocation;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
