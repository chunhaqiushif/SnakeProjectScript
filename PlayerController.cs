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
    float input = 0;
    float inputRaw = 0;
    
    // Use this for initialization
    void Start () {
		m_grid_move = GetComponent<GridMove> ();
    }

	void Update(){
		//m_updateFunc();

        input = Input.GetAxis("Horizontal");
        inputRaw = Input.GetAxisRaw("Horizontal");

        GetMoveDirection();
    }
    //--------------------阶段------------------------

    void OnStageStart()
    {
        ChangeState("State_Normal", State_NormalInit);

        m_lastInput = Vector3.zero;
        m_lastInputTime = 0.0f;
    }

    //--------------------功能------------------------

    public delegate void ChangeDirectionPointHandler(Vector3 direction);
    public static ChangeDirectionPointHandler ChangeDirectionPoint;

    Vector3 TurnMoveDirection(float inputDirction)
    {
        if (inputDirction != 0)
        {
            directionCount = (directionCount + 4 + (int)inputDirction) % 4;
        }
        return directions[directionCount];
    }

    Vector3 GetMoveDirection()
    {
        Vector3 direction = new Vector3();
        float absInput = Mathf.Abs(input);

        if (absInput < THRESHOLD)
        {
            if (m_lastInputTime < 0.2f)
            {
                m_lastInputTime += Time.deltaTime;
                direction = m_lastInput;
                //				absInput = Mathf.Abs (input);
            }
        }
        else
        {
            m_lastInputTime = 0;
            m_lastInput = TurnMoveDirection(inputRaw);
        }

        if (absInput < 0.1f)
        {
            return Vector3.zero;
        }

        direction = directions[directionCount];
        ChangeDirectionPoint(direction);
        return direction;
    }

    public void OnGrid(Vector3[] positionPage)
    {
        Vector3 pos = positionPage[0];
        Vector3 near_grid = positionPage[1];
<<<<<<< HEAD
<<<<<<< HEAD

        Vector3 direction = new Vector3();
        direction = GetMoveDirection();
        if (direction == Vector3.zero)
        {
            return;
        }
=======
=======
>>>>>>> parent of 3bfbc9d... Update
		Vector3 direction = new Vector3();
		direction = GetMoveDirection ();
		if (direction == Vector3.zero) {
			return;
		}

        m_grid_move.SetDirection (direction);
        SaveTheTurnPoint(near_grid, direction);
>>>>>>> parent of 3bfbc9d... Update
    }
        //public void OnGrid()
        //{
        //    Vector3 direction = new Vector3();
        //    direction = GetMoveDirection();
        //    if (direction == Vector3.zero)
        //    {
        //        return;
        //    }

        //    m_grid_move.SetDirection(direction);
        //}

    //-----------------------状态功能--------------------------------
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
	//------------------状态初始化--------------------------
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