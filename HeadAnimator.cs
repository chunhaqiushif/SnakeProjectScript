using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAnimator : MonoBehaviour {
    protected GridMove m_move;
    protected NodeHeadMove m_nodeHeadMove;

	// Use this for initialization
	void Start () {
        m_move = GetComponent<GridMove>();
        if (m_move == null)
        {
            m_nodeHeadMove = GetComponent<NodeHeadMove>();
            m_move = m_nodeHeadMove.m_parentNode_GridMove.GetComponent<GridMove>();
        }
    }


    // Update is called once per frame
    public virtual void Update () {
        Quaternion targetRotation = Quaternion.LookRotation(m_move.GetDirection());
        float t = 1.0f - Mathf.Pow(0.75f, Time.deltaTime * 30f);
        transform.localRotation = MathUtil.Slerp(transform.localRotation, targetRotation, t);
	}
}
