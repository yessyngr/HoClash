using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class NoteSpawnController : MonoBehaviour {
	
	public List<GameObject> notesTypeList;
	
	public static List<GameObject> queueList;

	private int totalSpawn;
	private float delayEachNotes;

	void Start () 
	{
		delayEachNotes = 1.0f;
		totalSpawn = 8;
		queueList = new List<GameObject>();

		InsertSpawnQueue();
		CoActiveQueue();
	}
			
	void InsertSpawnQueue()
	{	
		//assign total each type
		int totalAttack = Random.Range(0,totalSpawn);
		int totalRecovery = totalSpawn-totalAttack;
		
		for (int counter = 0; counter < totalSpawn ; counter++)
		{
			int code;
			
			//fill queue
			if(totalAttack == 0)
			{
				code = 1;
			}
			else if(totalRecovery == 0)
			{
				code = 0;
			} 
			else 
			{
				code = Random.Range(0,notesTypeList.Count);
				if(code == 0)
				{
					totalAttack--;
				} 
				else 
				{
					totalRecovery--;
				}
			}
			
			Vector3 spawnPosition = new Vector3(-5.0f,-2.5f);
			GameObject spawnedNotes = Instantiate(notesTypeList[code],spawnPosition,Quaternion.identity) as GameObject;
			queueList.Add(spawnedNotes);
		}
	}
	
	void CoActiveQueue()
	{
		StartCoroutine(ActiveQueue());
	}

	IEnumerator ActiveQueue()
	{
		for (int counter = 0; counter < queueList.Count ; counter++)
		{
			queueList[counter].SetActive(true);
			yield return new WaitForSeconds(delayEachNotes);
		}
	}
}
