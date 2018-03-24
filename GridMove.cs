using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMove : MonoBehaviour {
	public float m_speed;
	public float m_angle;
	public float m_radius;
	public bool isTurning;
	float m_inputSpeed;

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

	// Use this for initialization
	void Start () {
		m_radius = 0.5f;
		m_angle = 90.0f;
		m_speed = 2f;

		m_move_vector = Vector3.zero;
		m_direction = Vector3.forward;

		m_inputSpeed = m_speed;
		isTurning = false;
		

	}

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
	public void Move(float t){

		//下一个移动位置
		Vector3 pos = transform.position;
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

		if (across || (pos-near_grid).magnitude < 0.00005f) {
			Vector3 direction_save = m_direction;

            Vector3[] positionPage = new Vector3[2] {pos, near_grid};

			//发送消息并调用OnGrid()方法
			SendMessage ("OnGrid", positionPage);

			if (Vector3.Dot(direction_save, m_direction)<0.00005f) {
				pos = near_grid + m_direction * 0.001f;
			}

		}
        m_move_vector = (pos - transform.position) / t;
        transform.position = pos;


    }

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

	public bool IsRuning(){
		if ((m_move_vector.magnitude) > 0.1f)
			return true;
		return false;
	}

}