using UnityEngine;
using System.Collections;

public class CleanerController : MonoBehaviour {

	public GameObject pad;
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "notes")
		{
			Debug.Log("Out of Range!");
			
			PadController padController = pad.GetComponent<PadController>();
			padController.noteCount++;
			padController.CoShowComment("miss!");
			Debug.Log(pad.GetComponent<PadController>().noteCount);
		}
		
	}
	
}
