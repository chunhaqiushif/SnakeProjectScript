using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMove : MonoBehaviour {
	public float m_speed;
	float m_inputSpeed = 0;

	Vector3 m_move_vector;
	Vector3 m_direction = new Vector3 ();

	private const float HITCHECK_HEIGHT = 0.5f;
	private const int HITCHECK_LAYER_MASK = 1 << 0;

	//public delegate void PointAddEventHandler(Vector3 point);
	//public static PointAddEventHandler PointAddEvent;

	float m_input2turn;

    private Vector3 m_next_pos = Vector3.zero;
    private Vector3 m_current_grid = Vector3.zero;

    float m_inputV = 0;

<<<<<<< HEAD
<<<<<<< HEAD
    public bool isHeadFlag = false;
    //public bool isFirstNode = false;


    // Use this for initialization
    void Start () {
        PlayerController.ChangeDirectionPoint += SetTheTurnPoint;

=======
=======
>>>>>>> parent of 3bfbc9d... Update
	// Use this for initialization
	void Start () {
		m_radius = 0.5f;
		m_angle = 90.0f;
<<<<<<< HEAD
>>>>>>> parent of 3bfbc9d... Update
=======
>>>>>>> parent of 3bfbc9d... Update
		m_speed = 2f;

		m_move_vector = Vector3.zero;
		m_direction = Vector3.forward;

<<<<<<< HEAD
    }
=======
		m_inputSpeed = m_speed;
		isTurning = false;
		

	}
>>>>>>> parent of 3bfbc9d... Update

    //---------------------状态-----------------------
	public void OnGameStart(){
		m_move_vector = Vector3.zero;
	}

	public void OnStageStart(){
		m_move_vector = Vector3.zero;
	}
    //----------------------实时更新---------------------------

	void Update(){

        //速度计算
        m_inputSpeed = Mathf.Lerp(m_speed * 0.5f, m_speed * 2.0f, (Input.GetAxisRaw("Vertical") + 1) * 0.5f);

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

    //-------------------功能------------------------------------
<<<<<<< HEAD
<<<<<<< HEAD

    public void Move(float t){

        //下一个移动位置
        Vector3 pos = transform.position;
=======
=======
>>>>>>> parent of 3bfbc9d... Update
	public void Move(float t){

		//下一个移动位置
		Vector3 pos = transform.position;
>>>>>>> parent of 3bfbc9d... Update
		pos += m_direction * m_inputSpeed * t;

        //检查是否通过网格
        bool across = false;                                                                                                                                                                                                                                                    

		//比较整数化的坐标，false时判断为跨越了网格
		if ((int)pos.x != (int)transform.position.x) {
			across = true;
		}
		if ((int)pos.z != (int)transform.position.z) {
			across = true;
		}

		Vector3 near_grid = new Vector3 (Mathf.Round (pos.x), pos.y, Mathf.Round (pos.z));
        m_current_grid = near_grid;

        //当前位置正前方坐标，判断前方是否有障碍物
		Vector3 forward_pos = pos + m_direction * 0.5f;

        if (isHeadFlag)
        {
            Vector3[] positionPage = new Vector3[2] { pos, near_grid };

<<<<<<< HEAD
<<<<<<< HEAD
            //发送消息并调用OnGrid()方法
            SendMessage("OnGrid", positionPage);
        }
        else if (near_grid == m_turnPointPosition)
            if (across || (pos-near_grid).magnitude < 0.00005f)
        {
			Vector3 direction_save = m_direction;

            if (near_grid == m_turnPointPosition)
            {
                SetDirection(m_turnPointDirection);
                PointList.RemoveRange(0, 2);
            }
=======
            Vector3[] positionPage = new Vector3[2] {pos, near_grid};

			//发送消息并调用OnGrid()方法
			SendMessage ("OnGrid", positionPage);
>>>>>>> parent of 3bfbc9d... Update
=======
            Vector3[] positionPage = new Vector3[2] {pos, near_grid};

			//发送消息并调用OnGrid()方法
			SendMessage ("OnGrid", positionPage);
>>>>>>> parent of 3bfbc9d... Update

			if (Vector3.Dot(direction_save, m_direction)<0.00005f) {
				pos = near_grid + m_direction * 0.001f;
			}

		}
        m_move_vector = (pos - transform.position) / t;
        transform.position = pos;
<<<<<<< HEAD
        
    }

    //public void OnGrid(Vector3 pos, Vector3 near_grid)
    //{
    //    Vector3 direction = new Vector3();
    //    direction = GetDirection();
    //    if (direction == Vector3.zero)
    //    {
    //        return;
    //    }
    //}
=======



    }

<<<<<<< HEAD
>>>>>>> parent of 3bfbc9d... Update
=======
>>>>>>> parent of 3bfbc9d... Update
    //---------------------接口------------------------------
    public float GetSpeed() {
        return m_inputSpeed;
    }

	public void SetDirection(Vector3 v){
		m_direction = v;
	}

	public Vector3 GetDirection(){
		return m_direction;
	}

    public Vector3 GetNearGrid() {      
        return m_current_grid;
    }

<<<<<<< HEAD
<<<<<<< HEAD
    public void SetTheTurnPoint(Vector3 direction)
    {
        Vector3 temp = m_direction;
        m_direction = direction;
        if (temp != m_direction)
        {
            SaveTheTurnPoint(m_direction);
        }
    }

    public void SaveTheTurnPoint(Vector3 direction)
    {
        Vector3[] temp = { m_current_grid, direction };
        PointList.AddRange(temp);
    }

=======
>>>>>>> parent of 3bfbc9d... Update
=======
>>>>>>> parent of 3bfbc9d... Update
	public bool IsRuning(){
		if ((m_move_vector.magnitude) > 0.1f)
			return true;
		return false;
	}

}