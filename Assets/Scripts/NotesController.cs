using UnityEngine;
using System.Collections;
using DG.Tweening;

public class NotesController : MonoBehaviour {

	private Vector3 startPosition;
	
	void Start () 
	{
		//CoMoveHorizontal();
		startPosition = transform.position;
		
	}

	void Update ()
	{
		Vector3 deactivePosition = new Vector3(5.0f, startPosition.y);
		int speed = 2;
		transform.position = Vector3.MoveTowards(transform.position, deactivePosition, Time.deltaTime * speed);
		
		//pull back and deactive after reach deactivePosition
		if(transform.position.x >= deactivePosition.x)
		{
			transform.position = startPosition;
			gameObject.SetActive(false);
		}
	}
	
}
