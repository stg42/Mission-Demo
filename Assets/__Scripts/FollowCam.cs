using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {
	static public FollowCam S;


	public float			easing = 0.05f;
	public Vector2			minXY;
	public bool			_________________;

	public GameObject		poi;
	public float			camZ;


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
		if (poi == null) {
			destination = Vector3.zero;
		} else {
			destination = poi.transform.position;
			if(poi.tag == "Projectile"){
				if(poi.rigidbody.IsSleeping() ){
					poi = null;
					return;
				}
			}
		}


		destination.x = Mathf.Max ( minXY.x, destination.x );
		destination.y = Mathf.Max ( minXY.y, destination.y );
		destination = Vector3.Lerp (transform.position, destination, easing);
		destination.z = camZ;
		transform.position = destination;
		this.camera.orthographicSize = destination.y + 10;
	}
}
