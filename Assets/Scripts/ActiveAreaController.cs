using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class ActiveAreaController : MonoBehaviour {
	
	public GameObject pad;

	[HideInInspector]
	public List<GameObject> notesInArea;

	void Start()
	{
		//notesInArea = new List<GameObject>();
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "notes")
		{
			notesInArea.Add(coll.gameObject);
			Debug.Log("in notesInArea :" + notesInArea.Count);

			pad.GetComponent<PadController>().notes = notesInArea;
			Debug.Log("updated :" + pad.GetComponent<PadController>().notes.Count);
		}
		
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "notes")
		{
			//remove first note
			notesInArea.RemoveAt(0);

			//show miss comment
			PadController padController = pad.GetComponent<PadController>();
			padController.CoShowComment("miss!");
		}
	}
	
	
}
