using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SubGridMove : MonoBehaviour
{
	private GridMove m_grid_move;
	private List<Vector3> PointList = new List<Vector3>();
	private float m_speed = 0.0f;

    private Vector3 m_direction;
    private Vector3 m_turnPointPosition;

	// Use this for initialization
	void Start ()
	{
		m_grid_move = GameObject.Find("head").GetComponent<GridMove> ();
        PlayerController.SaveTheTurnPoint += GetTheNextTurnPoint;
		
	}
	//-----------------------实时更新-----------------------------
	// Update is called once per frame
	void Update ()
	{
        //获取移动速度（或许可以用委托写？）
        m_speed = m_grid_move.GetSpeed();

        //分段处理移动
        if (Time.deltaTime <= 0.1f)
        {
            Move(Time.deltaTime);
        }
        else
        {
            int n = (int)(Time.deltaTime / 0.1f) + 1;
            for (int i = 0; i < n; i++)
            {
                Move(Time.deltaTime / (float)n);
            }
        }
    }
    //---------------------状态--------------------------
    //-------------------功能------------------------

    private void Move(float t) {
        //下一个移动位置
        Vector3 pos = transform.position;
        pos += m_direction * m_speed * t;

        //检查是否通过网格
        bool across = false;

        //比较整数化的坐标，false时判断为跨越了网格
        if ((int)pos.x != (int)transform.position.x)
        {
            across = true;
        }
        if ((int)pos.z != (int)transform.position.z)
        {
            across = true;
        }

        Vector3 near_grid = new Vector3(Mathf.Round(pos.x), pos.y, Mathf.Round(pos.z));

    }

    private void GetTheNextTurnPoint(Vector3 position,Vector3 direction) {
        PointList.Add(position);
        PointList.Add(direction);
    }
    //void PointAdd(Vector3 point){
    //	PointList.Add (point);
    //	Debug.Log("Get the point");
    //}

    //void Move(){
    //	iTween.MoveTo (gameObject, iTween.Hash (
    //		"position", PointList[0],
    //		"easeType", "linear",
    //		"loopType", "none",
    //		"speed", m_speed,
    //		"oncomplete","CountAddOne"
    //	));
    //	PointList.RemoveAt (0);
    //}
    //void CountAddOne(){
    //	Move ();
    //}
}

