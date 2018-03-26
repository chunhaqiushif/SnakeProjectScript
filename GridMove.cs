using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMove : MonoBehaviour {

    //移动速度
	public float m_speed;
	float m_inputSpeed = 0;

    //移动方向与储存转弯点
	Vector3 m_move_vector;
	Vector3 m_direction;
    Vector3 m_turnPointPosition = Vector3.zero;
    Vector3 m_turnPointDirection = Vector3.zero;
    List<Vector3> PointList = new List<Vector3>();

    //撞击检查
    private const float HITCHECK_HEIGHT = 0.5f;
	private const int HITCHECK_LAYER_MASK = 1 << 0;

    //计算移动
    private Vector3 m_next_pos = Vector3.zero;
    private Vector3 m_current_grid = Vector3.zero;

    //蛇头标记
    public bool isHeadFlag = false;

    //-----------------------初始化--------------------------
	void Start ()
    {
		m_speed = 2f;
		m_move_vector = Vector3.zero;
		m_direction = Vector3.forward;

        PlayerController.SaveTheTurnPoint += SaveTheTurnPoint;
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
        if (Time.deltaTime <= 0.1f)
        {
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

        //比较整数化的坐标，false时“!=”判断为跨越了网格
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
        
        if (across || (pos-near_grid).magnitude < 0.00005f)
        {
			Vector3 direction_save = m_direction;

            if (isHeadFlag)
            {
                Vector3[] positionPage = new Vector3[2] { pos, near_grid };

                //发送消息并调用OnGrid()方法
                SendMessage("OnGrid", positionPage);
            }
            else if (near_grid == m_turnPointPosition)
            {
                SetDirection(m_turnPointDirection);
                PointList.RemoveRange(0, 2);
            }
            else
            {
                print(m_turnPointPosition);
            }

			if (Vector3.Dot(direction_save, m_direction)<0.00005f)
            {
				pos = near_grid + m_direction * 0.001f;
			}

		}
        m_move_vector = (pos - transform.position) / t;
        transform.position = pos;

        
    }

    //---------------------接口------------------------------

	public void SetDirection(Vector3 v){
		m_direction = v;
	}

	public Vector3 GetDirection(){
		return m_direction;
	}

    public void SaveTheTurnPoint(Vector3 position, Vector3 direction)
    {
        Vector3[] temp = { position, direction };
        PointList.AddRange(temp);
        if (PointList.Count != 0)
        {
            m_turnPointPosition = position;
            m_turnPointDirection = direction;
        }
    }


	public bool IsRuning(){
		if ((m_move_vector.magnitude) > 0.1f)
			return true;
		return false;
	}

}