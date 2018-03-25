using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeHeadMove : MonoBehaviour {
    public GameObject m_parentNode;
    public GridMove m_parentNode_GridMove;

    // Use this for initialization
    void Start () {
        m_parentNode_GridMove = m_parentNode.GetComponent<GridMove>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = m_parentNode.transform.position;
    }
}
