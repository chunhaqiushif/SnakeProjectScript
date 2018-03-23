using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SubMove : MonoBehaviour
{
	private GridMove Grid_move;
	//private GameObject[] Body_Bone = new GameObject[3];
	private List<Vector3> PointList = new List<Vector3>();
	private float m_speed;

	// Use this for initialization
	void Start ()
	{
		Grid_move = GameObject.Find("head").GetComponent<GridMove> ();

		GridMove.PointAddEvent += this.PointAdd;
		PointList.Add (Vector3.zero);
		m_speed = Grid_move.m_speed;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//PointList = Grid_move.GetPointList;
	}

	void FixedUpdate(){

	}

	void PointAdd(Vector3 point){
		PointList.Add (point);
		Debug.Log("Get the point");
	}

	void Move(){
		iTween.MoveTo (gameObject, iTween.Hash (
			"position", PointList[0],
			"easeType", "linear",
			"loopType", "none",
			"speed", m_speed,
			"oncomplete","CountAddOne"
		));
		PointList.RemoveAt (0);
	}
	void CountAddOne(){
		Move ();
	}
}

