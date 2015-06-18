using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

	static public FollowCam S; // followCam Singleton

	//  fields set in Inspector

	public float easing = 0.05f;
	public Vector2 minXY;

	public bool ________________________________;

	//fields set dynamically
	public GameObject poi; //point of interest
	public float camZ; // desired Z position of the camera

	void Awake(){
		S = this;
		camZ = this.transform.position.z;
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 destination;

		if (poi == null) 
		{
			destination = Vector3.zero;
		} else {
			destination = poi.transform.position;
			if (poi.tag == "Projectile") {
				if (poi.rigidbody.IsSleeping()) {
					poi = null;
					return;
				}
			}

		}
		//destination = poi.transform.position;
		destination = Vector3.Lerp(transform.position, destination, easing);
		destination.x = Mathf.Max (minXY.x, destination.x);
		destination.y = Mathf.Max (minXY.y, destination.y);

		destination.z = camZ;
		transform.position=destination;
		this.camera.orthographicSize=destination.y + 10;
		
	}
}
