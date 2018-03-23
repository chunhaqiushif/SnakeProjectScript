using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MoveStartButton4Test : MonoBehaviour {
	GridMove m_grid_move;
	SubMove m_sub_move;
	// Use this for initialization
	void Start () {
		//m_grid_move = GameObject.Find ("head").GetComponent<GridMove> ();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		if (GUI.Button(new Rect(0,10,100,30),"Start")) {
			GameObject.Find ("head").SendMessage("Move");
			GameObject.Find ("body_00").BroadcastMessage("Move");
			Debug.Log ("Move button has been clicked");
			Destroy (this.gameObject);
		}
	}
}
