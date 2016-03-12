using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour {

	public LineRenderer dragLine;

	SpringJoint2D springJoint=null; //We add a spring joint so that we can grab the block from any position and not only from its center
	Rigidbody2D currentObject=null; //This is the object we have currently selected if not null

	//Use FixedUpdate for any physics stuff, and use Update for any user interaction since FixedUpdate might miss those events
	void FixedUpdate() {
		if (currentObject != null) {
			Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			Vector2 mousePos2D = new Vector2(mousePos3D.x, mousePos3D.y);
			if(springJoint!=null){
				dragLine.enabled=true; //Whenever there is a spring joint active, we need a drag line to be enabled
				springJoint.connectedAnchor=mousePos2D; //connectedAnchor is connected to nothing so its global coordinates
				Vector3 worldSpringAnchor = currentObject.transform.TransformPoint(springJoint.anchor); // This gives us global coordinates of the spring anchor
				dragLine.SetPosition(0, new Vector3(worldSpringAnchor.x,worldSpringAnchor.y,-1)); //We put z to -1 so that its in the front of all the other objects.
				dragLine.SetPosition(1, new Vector3(springJoint.connectedAnchor.x,springJoint.connectedAnchor.y,-1));
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) { //0 is for first mouse button, 1 is for 2nd mouse button
			Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( Input.mousePosition );
			Vector2 mousePos2D = new Vector2(mousePos3D.x, mousePos3D.y);
			RaycastHit2D hit = Physics2D.Raycast(mousePos2D,Vector2.zero); //To check which object we are selecting
			if(hit && hit.collider!=null)
			{
				if(hit.collider.GetComponent<Rigidbody2D>()!=null && hit.collider.GetComponent<Rigidbody2D>().tag!="base")
				{
					currentObject=hit.collider.GetComponent<Rigidbody2D>(); //We just store the rigid body component of that game object we selected
					springJoint=currentObject.gameObject.AddComponent<SpringJoint2D>();
					Vector3 localHitPoint = currentObject.transform.InverseTransformPoint(hit.point); //We convert the global position to a local position on the object as we are anchoring the spring to the object.
					springJoint.anchor = localHitPoint;
					springJoint.connectedBody=null; //The spring is not connected to another object but just the world
					springJoint.enableCollision=true; //Here the spring is connected to the world, so we still want it to collide with the rest of the world
					springJoint.frequency=2f;
					springJoint.dampingRatio=5f;
					springJoint.distance=0.25f;
				}
			}
		}
		if (Input.GetMouseButtonUp (0)) {
			currentObject=null;
			Destroy(springJoint);
			springJoint=null;
			dragLine.enabled=false;
		}
	}
}
