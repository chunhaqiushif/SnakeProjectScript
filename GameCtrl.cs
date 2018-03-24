using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCtrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameStart ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GameStart(){
		OnStageStart ();
	}

	public void OnStageStart(){
		GameObject.Find ("head").SendMessage ("OnStageStart");
	}
}
