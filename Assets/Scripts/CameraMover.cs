using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {

	//The collisionhandler sets our targetY and then we move the camera and the launchers up if we need to.
	public float targetY=0;
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.y = Mathf.Lerp (transform.position.y, targetY, 0.2f); //We get 0.2*first+(1-0.2)*second value so its giving us a value closer to the target each time.
		transform.position = pos;
		Vector2 launchPosition1=GameObject.Find ("Launcher").transform.position;
		Vector2 launchPosition2=GameObject.Find ("Launcher 1").transform.position;
		Vector2 newLaunch1 = new Vector2 (launchPosition1.x, pos.y);
		Vector2 newLaunch2 = new Vector2 (launchPosition2.x, pos.y);
		GameObject.Find ("Launcher").transform.position = newLaunch1;
		GameObject.Find ("Launcher 1").transform.position = newLaunch2;
	}
}
