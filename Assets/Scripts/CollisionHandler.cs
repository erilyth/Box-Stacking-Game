using UnityEngine;
using System.Collections;

public class CollisionHandler : MonoBehaviour {

	//To move the camera upwards or downwards as needed
	void OnCollisionEnter2D(){
		Vector3 cameraPos = Camera.main.transform.position;
		if (cameraPos.y < transform.position.y) {
			Camera.main.GetComponent<CameraMover>().targetY=transform.position.y;
		}
	}
	
}
