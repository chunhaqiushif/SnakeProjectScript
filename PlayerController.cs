using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private GridMove m_grid_move;

	public float THRESHOLD = 0.1f;
	private Vector3 m_lastInput = Vector3.zero;
	private float m_lastInputTime = 0;

	private int directionCount = 0;
	Vector3[] directions = new Vector3[4] {
		Vector3.forward,
		Vector3.right,
		Vector3.back,
		Vector3.left
	};

    // Use this for initialization
    void Start () {
		m_grid_move = GetComponent<GridMove> ();
    }

	void OnStageStart(){
		ChangeState ("State_Normal", State_NormalInit);

		m_lastInput = Vector3.zero;
		m_lastInputTime = 0.0f;
	}

	void Update(){
		m_updateFunc();
    }

    void FixedUpdate () {
 
    }

	Vector3 TurnMoveDirection(float inputDirction){
		if (inputDirction != 0) {
			directionCount = (directionCount + 4 + (int)inputDirction) % 4;
		}
		return directions [directionCount];
	}

	Vector3 GetMoveDirection(){
		float input = Input.GetAxis ("Horizontal");
		float inputRaw = Input.GetAxisRaw ("Horizontal");
		float absInput = Mathf.Abs (input);
		Vector3 direction = new Vector3 ();

		if (absInput < THRESHOLD) {
			if (m_lastInputTime < 0.2f) {
				m_lastInputTime += Time.deltaTime;
				direction = m_lastInput;
//				absInput = Mathf.Abs (input);
			}
		} else {
			m_lastInputTime = 0;
			m_lastInput = TurnMoveDirection (inputRaw);
		}

        if (absInput < 0.1f)
        {
            return Vector3.zero;
        }

        direction = directions[directionCount];
		return direction;
	}

	public void OnGrid(Vector3 newPos){
		Vector3 direction = new Vector3();
		direction = GetMoveDirection ();
		if (direction == Vector3.zero) {
			return;
		}
		m_grid_move.SetDirection (direction);
	}

	//-------------------------------------------------------
	delegate void STATE_FUNC();
	private string m_currentStateName;
	STATE_FUNC m_stateEndFunc;

	delegate void ENCOUNT_FUNC(Transform o);
	private ENCOUNT_FUNC m_encountFunc;
	private STATE_FUNC m_updateFunc;

	private void SetDefaultFunc(){
		m_stateEndFunc = null;
		//m_encountFunc = Encount_Normal;
		m_updateFunc = Update_Normal;
	}

	private void ChangeState(string newState, STATE_FUNC init){
		if (m_currentStateName == newState) {
			return;
		}
		StopCoroutine (m_currentStateName);

		if (m_stateEndFunc != null) {
			m_stateEndFunc ();
		}
		SetDefaultFunc ();

		m_currentStateName = newState;

		if (init != null) {
			init ();
		}
		StartCoroutine (m_currentStateName);
	}
	//--------------------------------------------
	private void State_NormalInit(){
		m_updateFunc = Update_Normal;
	}

	IEnumerator State_Normal(){
		yield return null;
	}

	private void Update_Normal(){
		Vector3 direction = GetMoveDirection ();
		if (direction == Vector3.zero) {
			return;
		}
		//方向转换
		m_grid_move.SetDirection (direction);
	}
}