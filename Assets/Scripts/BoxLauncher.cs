using UnityEngine;
using System.Collections;

public class BoxLauncher : MonoBehaviour {

	public GameObject[] ObjectPrefabs;

	public float fireDelay = 3f;
	public float timeForNextFire =1f;
	public float fireVelocity = 10f;

	// Update is called once per frame
	void FixedUpdate () {
		if (GameObject.Find ("Death Trigger").GetComponent<LoseTrigger>().hasLost==false) {
			timeForNextFire -= Time.deltaTime; //This is our timer
			if (timeForNextFire <= 0) {
				timeForNextFire = fireDelay;
				GameObject curBox = (GameObject)Instantiate (ObjectPrefabs [Random.Range (0, ObjectPrefabs.Length)], this.transform.position, this.transform.rotation);
				curBox.GetComponent<Rigidbody2D> ().velocity = transform.rotation * new Vector2 (0, fireVelocity); //So it shoots in the direction our launcer is facing
				GameObject.Find("Main Camera").GetComponent<ScoreManager>().score += 1;
			}
		}
	}
}