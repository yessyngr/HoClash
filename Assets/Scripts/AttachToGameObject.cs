using UnityEngine;
using System.Collections;

public class AttachToGameObject : MonoBehaviour {

	[SerializeField]
	private GameObject Following;

	void Start () {

		Vector2 sourcePosition = Following.GetComponent<Transform>().position;
		//this.GetComponent<Transform>().position = sourcePosition;
		this.GetComponent<Transform>().position = Camera.main.WorldToViewportPoint(sourcePosition);

		Debug.Log(Camera.main.WorldToViewportPoint(sourcePosition));
	}
	
}
