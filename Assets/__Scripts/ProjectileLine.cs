using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileLine : MonoBehaviour {

	static public ProjectileLine S; //singleton

	//fields set in the Unity Inspector pane
	public float	minDist = 0.1f;
	public bool 	_____________________________;

	// fields set dynamically
	public LineRenderer   line;
	private GameObject    _poi;
	public List<Vector3>  points;
	public GameObject	  sshotGO;	


	void Awake(){
		S = this;
		line = GetComponent<LineRenderer> ();
		line.enabled = false;
		points = new List<Vector3>();
		sshotGO =GameObject.Find("SlingShot");


	}


	public GameObject poi {
		get {
			return _poi;
		}

		set {
			_poi = value;
			if (_poi != null) {
				line.enabled = false;
				points = new List<Vector3>();
				AddPoint();
			} // end if
		} // end set
	} // end poi property


	// used to clear the line directly
	public void Clear(){
		_poi = null;
		line.enabled = false;
		points = new List<Vector3>();
	}


	public void AddPoint() {
		Vector3 pt = _poi.transform.position;
		// check to make sure we've moved far enough, don't do anything if too close
		if (points.Count > 0 && (pt - lastPoint).magnitude < minDist) {
			return;
		}

		// two possibilities - first point requires more work
		if (points.Count == 0) {
			Vector3 launchPos = SlingShot.S.launchPoint.transform.position;
			Vector3 launchPosDiff = pt - launchPos;
			//_it adds an extra bit of line to aid aiming later
			points.Add (pt + launchPosDiff);
			points.Add (pt);
			line.SetVertexCount (2);
			// Sets the first two points
			line.SetPosition (0, points [0]);
			line.SetPosition (1, points [1]);
			// Enables the LineRenderer
			line.enabled = true;
		} else {
			// Normal behavior to add a point
			points.Add (pt);
			line.SetVertexCount(points.Count);
			line.SetPosition(points.Count-1, lastPoint);
			line.enabled = true;
		}
	}

	// property returns the location of the most recently added point
	public Vector3 lastPoint {
		get {
			if (points == null){
				// if there are no points, return Vector3.zero
				return (Vector3.zero);
			}
			return (points[points.Count-1]);
		}
	}

	void FixedUpdate() {
		if (poi == null) {
			// if there is no poi, search for one
			if (FollowCam.S.poi != null) {
				if (FollowCam.S.poi.tag == "Projectile") {
					poi = FollowCam.S.poi;
				} else {
					return; // we didn't find a poi
				}
			} else {
				return;  // poi = null
			}
		}

		// if we get here, there must be a poi
		AddPoint ();

		if(poi.rigidbody.IsSleeping()) {
			//if projectile is no longer moving
			poi = null;
		}


	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
