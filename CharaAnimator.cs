using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaAnimator : MonoBehaviour {
    protected GridMove m_move;

	// Use this for initialization
	void Start () {
        m_move = GetComponent<GridMove>();
    }


    // Update is called once per frame
    public virtual void Update () {
        Quaternion targetRotation = Quaternion.LookRotation(m_move.GetDirection());
        float t = 1.0f - Mathf.Pow(0.75f, Time.deltaTime * 30f);
        transform.localRotation = MathUtil.Slerp(transform.localRotation, targetRotation, t);
	}
}
