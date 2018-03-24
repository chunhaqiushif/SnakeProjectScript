using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMove : MonoBehaviour {
	public float m_speed;
	public float m_angle;		    			
	public float m_radius;					
	float m_rotationTime;
	public bool isTurning;
	float m_inputSpeed;

	Vector3 m_move_vector;
	Vector3 m_direction = new Vector3 ();

	private const float HITCHECK_HEIGHT = 0.5f;
	private const int HITCHECK_LAYER_MASK = 1 << 0;

	public delegate void PointAddEventHandler(Vector3 point);
	public static PointAddEventHandler PointAddEvent;

	float m_input2turn;
	//private PlayerController m_player_controller;

	Vector3 m_next_pos = Vector3.zero;



	//private float m_wait4inputTimer = 0;

	// Use this for initialization
	void Start () {
		m_radius = 0.5f;
		m_angle = 90.0f;
		m_speed = 2f;

		m_move_vector = Vector3.zero;
		m_direction = Vector3.forward;

		m_inputSpeed = m_speed;
		m_rotationTime = Mathf.Sqrt(2 * m_radius * m_radius) / m_speed;
		isTurning = false;
		//m_player_controller = transform.GetComponent<PlayerController>();

	}

	public void OnGameStart(){
		m_move_vector = Vector3.zero;
	}

	public void OnStageStart(){
		m_move_vector = Vector3.zero;
	}

	void Update(){

		//分段处理移动
		if (Time.deltaTime <= 0.1f) {
			Move (Time.deltaTime);
		} else {
			int n = (int)(Time.deltaTime / 0.1f) + 1;
			for (int i = 0; i < n; i++) {
				Move (Time.deltaTime / (float)n);
			}
		}
	}

	void FixedUpdate () {
		//速度计算
		//m_inputSpeed = Mathf.Lerp(m_speed, m_speed * 3.0f, (m_inputV + 1) * 0.5f);

	}
		

	public void Move(float t){
//		if (m_inputH != 0) {
//			m_input2turn = m_inputH;
//			Turn (m_input2turn);
//			return;
//		}

		//下一个移动位置
		Vector3 pos = transform.position;
		pos += m_direction * m_speed * t;

		//检查是否通过网格
		bool across = false;                                                                                                                                                                                                                                                    

		//比较整数化的坐标，false时判断为为跨越了网格
		if ((int)pos.x != (int)transform.position.x) {
			across = true;
		}
		if ((int)pos.z != (int)transform.position.z) {
			across = true;
		}

		Vector3 near_grid = new Vector3 (Mathf.Round (pos.x), pos.y, Mathf.Round (pos.z));

		Vector3 forward_pos = pos + m_direction * 0.5f;

		if (across || (pos-near_grid).magnitude < 0.00005f) {
			Vector3 direction_save = m_direction;

			//发送消息并调用OnGrid()方法
			SendMessage ("OnGrid", pos);

			if (Vector3.Dot(direction_save, m_direction)<0.00005f) {
				pos = near_grid + m_direction * 0.001f;
			}

		}
        m_move_vector = (pos - transform.position) / t;
        transform.position = pos;

        //m_next_pos = transform.position + GetMoveDirection ();
        //		iTween.MoveTo (gameObject, iTween.Hash (
        //			"position",m_next_pos,
        //			"easeType", "linear",
        //			"loopType", "none",
        //			"speed", m_inputSpeed,
        //			"onstart","SaveTheNextPoint",
        //			"onupdate","Wait4Input",
        //			"oncomplete","MoveOverState"
        //		));
    }
	public void SetDirection(Vector3 v){
		m_direction = v;
	}

	public Vector3 GetDirection(){
		return m_direction;
	}

    public Vector3 GetWorldDirection() {
        Vector3 direction = m_direction;
        
        return direction;
    }

	public bool IsRuning(){
		if ((m_move_vector.magnitude) > 0.1f)
			return true;
		return false;
	}

//	private void SaveTheNextPoint(){
//		m_input2turn = 0;
//		m_wait4inputTimer = 0;
//		PointAddEvent(m_next_pos);
//	}
//
//	private void MoveOverState(){
//		//again?
//		if (m_input2turn != 0) {
//			Turn (m_input2turn);
//			return;
//		}
//		Move ();
//	}
//
//	private void Wait4Input(){
//		if (m_wait4inputTimer < 1f && m_inputH != 0) {
//			m_input2turn = m_inputH;
//			m_wait4inputTimer += Time.deltaTime;
//		} 
//	}
//
//	public void Turn(float inputDirction){
//		if (!isTurning) {
//			isTurning = true; 
//			//change the direction to face now
//			Vector3 direction = GetMoveDirection ();
//
//			m_next_pos = transform.position;
//
//			if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z)) {
//				if (direction.x > direction.z) {
//					m_next_pos.x += direction.x * 0.5f; 
//					m_next_pos.z += -inputDirction * 0.5f;
//				} else {
//					m_next_pos.x += direction.x * 0.5f; 
//					m_next_pos.z += inputDirction * 0.5f;
//				}
//
//			} else {
//				if (direction.x > direction.z) {
//					m_next_pos.z += direction.z * 0.5f; 
//					m_next_pos.x += -inputDirction * 0.5f;
//
//				} else {
//					m_next_pos.z += direction.z * 0.5f; 
//					m_next_pos.x += inputDirction * 0.5f;
//				}
//			}
//		
//			//CAUTION! this is turnng function for 90°
//			iTween.RotateAdd(gameObject, iTween.Hash(
//				"z", inputDirction * m_angle, 
//				"easeType", "linear",
//				"loopType", "none",
//				"time", m_rotationTime,
//				"onstart", "SaveTheNextPoint",
//				"oncomplete","TurnOverState"
//			));
//			iTween.MoveTo(gameObject, iTween.Hash(
//				"position",m_next_pos,
//				"easeType", "linear",
//				"loopType", "none",
//				"speed", m_inputSpeed
//			));
//			TurnMoveDirection (inputDirction);
//		}
//	}
//		
//	void TurnOverState(){
//		isTurning = false;
//		Move ();
//	}
//
//	public float GetInputSpeed(){
//		return m_inputSpeed;
//	}
//

}