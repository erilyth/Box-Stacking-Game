using UnityEngine;
using System.Collections;

public class LoseTrigger : MonoBehaviour {

	public GUISkin mySkin;
	public bool hasLost=false;
	// Use this for initialization
	void OnTriggerEnter2D(){
		hasLost = true;
	}

	void OnGUI(){
		if (hasLost) {
			GUI.skin=mySkin;
			GUI.Label (new Rect(Screen.width/2-50,Screen.height/2-25,100,50), "Game Over");
			if(GUI.Button (new Rect(Screen.width/2-50,Screen.height/2+25,100,50), "Restart")){
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
}
