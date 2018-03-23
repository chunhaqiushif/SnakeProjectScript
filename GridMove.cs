using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMove : MonoBehaviour {
	public float m_speed;
	public float m_angle;		    			
	public float m_radius;					
	float m_rotationTime;
	public bool isTurning; 
	float m_inputH;					
	float m_inputV;                     
   
	float m_inputSpeed;

	public delegate void PointAddEventHandler(Vector3 point);
	public static PointAddEventHandler PointAddEvent;

	float m_input2turn;
	private PlayerController m_player_controller;

	Vector3 m_next_pos = Vector3.zero;

	public int directionCount = 0;
	Vector3[] directions = new Vector3[4] {
		Vector3.forward,
		Vector3.right,
		Vector3.back,
		Vector3.left
	};

	private float m_wait4inputTimer = 0;

	// Use this for initialization
	void Awake () {
		m_radius = 0.5f;
		m_angle = 90.0f;
		m_speed = 2f;
		m_inputSpeed = m_speed;
		m_rotationTime = Mathf.Sqrt(2 * m_radius * m_radius) / m_speed;
		isTurning = false;
		m_player_controller = transform.GetComponent<PlayerController>();
	}

	void Update(){
		//输入获取
		m_inputH = Input.GetAxisRaw("Horizontal");
		m_inputV = Input.GetAxisRaw("Vertical");

	}

	void FixedUpdate () {
		//速度计算
		//m_inputSpeed = Mathf.Lerp(m_speed, m_speed * 3.0f, (m_inputV + 1) * 0.5f);

	}
		

	public void Move(){
		if (m_inputH != 0) {
			m_input2turn = m_inputH;
			Turn (m_input2turn);
			return;
		}
		//Vector3 direction = GetMoveDirection ();

		m_next_pos = transform.position + GetMoveDirection ();

		iTween.MoveTo (gameObject, iTween.Hash (
			"position",m_next_pos,
			"easeType", "linear",
			"loopType", "none",
			"speed", m_inputSpeed,
			"onstart","SaveTheNextPoint",
			"onupdate","Wait4Input",
			"oncomplete","MoveOverState"
		));
	}

	private void SaveTheNextPoint(){
		m_input2turn = 0;
		m_wait4inputTimer = 0;
		PointAddEvent(m_next_pos);
	}

	private void MoveOverState(){
		//again?
		if (m_input2turn != 0) {
			Turn (m_input2turn);
			return;
		}
		Move ();
	}

	private void Wait4Input(){
		if (m_wait4inputTimer < 1f && m_inputH != 0) {
			m_input2turn = m_inputH;
			m_wait4inputTimer += Time.deltaTime;
		} 
	}

	public void Turn(float inputDirction){
		if (!isTurning) {
			isTurning = true; 
			//change the direction to face now
			Vector3 direction = GetMoveDirection ();

			m_next_pos = transform.position;

			if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z)) {
				if (direction.x > direction.z) {
					m_next_pos.x += direction.x * 0.5f; 
					m_next_pos.z += -inputDirction * 0.5f;
				} else {
					m_next_pos.x += direction.x * 0.5f; 
					m_next_pos.z += inputDirction * 0.5f;
				}

			} else {
				if (direction.x > direction.z) {
					m_next_pos.z += direction.z * 0.5f; 
					m_next_pos.x += -inputDirction * 0.5f;

				} else {
					m_next_pos.z += direction.z * 0.5f; 
					m_next_pos.x += inputDirction * 0.5f;
				}
			}
		
			//CAUTION! this is turnng function for 90°
			iTween.RotateAdd(gameObject, iTween.Hash(
				"z", inputDirction * m_angle, 
				"easeType", "linear",
				"loopType", "none",
				"time", m_rotationTime,
				"onstart", "SaveTheNextPoint",
				"oncomplete","TurnOverState"
			));
			iTween.MoveTo(gameObject, iTween.Hash(
				"position",m_next_pos,
				"easeType", "linear",
				"loopType", "none",
				"speed", m_inputSpeed
			));
			TurnMoveDirection (inputDirction);
		}
	}
		
	void TurnOverState(){
		isTurning = false;
		Move ();
	}

	public float GetInputSpeed(){
		return m_inputSpeed;
	}

	void TurnMoveDirection(float inputDirction){
		if (inputDirction != 0) {
			directionCount = (directionCount + 4 + (int)inputDirction) % 4;
		}
	}

	Vector3 GetMoveDirection(){
		return directions [directionCount];
	}
}