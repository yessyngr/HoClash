using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

using System.Collections.Generic;

public class PadController : MonoBehaviour {

	public GameObject line;
	public GameObject comment;
	public GameObject activeArea;
	
	[HideInInspector]	
	public List<GameObject> notes;
	[HideInInspector]
	public int playedNotes;

	private float _distance;
	[HideInInspector]
	public int noteCount;

	private bool _checkingMiss;
	private bool _isOnAnimation;

	private float missDistance;

	void Start()
	{
		_checkingMiss = true;
		_isOnAnimation = false;

		missDistance = 1.0f;

		//notes interacting counted by _tapCount
		//_notes = NoteSpawnController.queueList[noteCount];
		notes = new List<GameObject>();
		
	}
	

	 void OnMouseDown ()
    {	
		if(!_isOnAnimation)
		{
			CoLineAnimation();
			
			if(notes.Count > 0)
			{
				_distance = Mathf.Abs(notes[0].transform.position.x - line.transform.position.x);
				Scoring(_distance);
				
				notes[0].SetActive(false);
				notes.RemoveAt(0);
				playedNotes++;
				CheckFinishWave();
			}
		}
	}

	public void CheckFinishWave()
	{
		if (playedNotes == NoteSpawnController.queueList.Count)
		{
			Debug.Log("Wave ended, attacking");
			LifeController.DecreaseBossLife();
		}
	}
	

	void Scoring(float distance)
	{
		if(distance <0.15f)
		{
			CoShowComment ("Perfect");
		}
		else if(distance <0.4f)
		{
			CoShowComment ("Great");
		}
		else if(distance <0.8f)
		{
			CoShowComment ("Cool");
		}
		else if(distance <1.2f)
		{
			CoShowComment ("Bad");
		}
		else
		{
			CoShowComment ("Miss");
		}
	}

	void CoLineAnimation()
	{
		StartCoroutine(LineAnimation());
	}

	IEnumerator LineAnimation()
	{
		Vector3 zoomInScale = 1.2f*line.gameObject.transform.localScale;
		Tween zoomingIn = line.gameObject.transform.DOScale(zoomInScale,0.2f);
		yield return zoomingIn.WaitForCompletion();

		Vector3 zoomOutScale = line.gameObject.transform.localScale/1.2f;
		Tween zoomingOut = line.gameObject.transform.DOScale(zoomOutScale,0.2f);
		yield return zoomingOut.WaitForCompletion ();
	}
	

	public void CoShowComment(string commentString)
	{
		StartCoroutine(ShowComment(commentString));
	}
	
	IEnumerator ShowComment(string commentString)
	{
		_isOnAnimation = true;
		comment.gameObject.GetComponent<Text>().text = commentString;
		comment.gameObject.SetActive(true);		

		Vector3 zoomInScale = 1.5f*comment.gameObject.transform.localScale;
		Tween zoomingIn = comment.gameObject.transform.DOScale(zoomInScale,0.2f);
		yield return zoomingIn.WaitForCompletion ();
		
		Vector3 zoomOutScale = comment.gameObject.transform.localScale/1.5f;
		Tween zoomingOut = comment.gameObject.transform.DOScale(zoomOutScale,0.2f);
		yield return zoomingOut.WaitForCompletion ();
		
		comment.gameObject.SetActive(false);
		_isOnAnimation = false;
	}
}
