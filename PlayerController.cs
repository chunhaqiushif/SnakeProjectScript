using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Vector3 m_lastInput = Vector3.zero;
	//private float m_lastInputTime = 0;
	private GridMove m_grid_move;

    // Use this for initialization
    void Start () {
		m_grid_move = GetComponent<GridMove> ();
    }

	void Update(){
   
    }

    void FixedUpdate () {
 
    }


}