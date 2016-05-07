using UnityEngine;
using System.Collections;
using DG.Tweening;

public class TweeningTrial : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		Vector3 pos = new Vector3(3.0f,1.0f);

	//transform.DOJump(pos,1.0f,3,1.0f,false);

		transform.DOLookAt(pos,1.0f,AxisConstraint.None,default(Vector3));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
