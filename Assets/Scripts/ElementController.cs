using UnityEngine;
using System.Collections;

public class ElementController : MonoBehaviour {

	[HideInInspector]	
	public PairInt boardCoordinate;
	[HideInInspector]
	public Vector3 _position;
	[HideInInspector]
	public int _element;

	private bool _clicked;
	
	private Vector3 screenPoint;
	private Vector3 offset;
	
	void Start()
	{
		_clicked = false;
	
		//take value from PuzzleController
		_position = PuzzleController.board[boardCoordinate.a,boardCoordinate.b].position;
		_element = PuzzleController.board[boardCoordinate.a,boardCoordinate.b].elementCode;
	}
	
	void OnMouseDown()
	{
		//Position Controller
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.GetComponent<Transform>().position);
		
		offset = gameObject.GetComponent<Transform>().position - Camera.main.ScreenToWorldPoint(
				 new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag()
	{
		_clicked = true;
		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
    	
		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
    	transform.position = curPosition;
	}

	 void OnMouseUp()
 	{
		_clicked = false;
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("enter collider");
		
		if (_clicked)
		{
			_position = PuzzleController.board[boardCoordinate.a,boardCoordinate.b].position;
			_element = PuzzleController.board[boardCoordinate.a,boardCoordinate.b].elementCode;

			//temp from other (puzzleCont)
			PairInt tempCoor = other.gameObject.GetComponent<ElementController>().boardCoordinate;
			//int tempElement = PuzzleController.board[tempCoor.a, tempCoor.b].elementCode;
			//Vector3 tempPosition = PuzzleController.board[tempCoor.a, tempCoor.b].position;
			
			//assign other -> this value
			other.GetComponent<ElementController>()._position = PuzzleController.board[boardCoordinate.a,boardCoordinate.b].position;
			other.GetComponent<ElementController>()._element = PuzzleController.board[boardCoordinate.a,boardCoordinate.b].elementCode;

			//assign this -> temp value
			_position = PuzzleController.board[tempCoor.a,tempCoor.b].position;
			_element = PuzzleController.board[tempCoor.a,tempCoor.b].elementCode;

			//move pos
			other.GetComponent<Transform>().position = other.GetComponent<ElementController>()._position;

			//move coord
			other.gameObject.GetComponent<ElementController>().boardCoordinate = boardCoordinate;
			boardCoordinate = tempCoor;
			
			//save to puzzleCont
			PuzzleController.board[boardCoordinate.a,boardCoordinate.b].position = _position;
			PuzzleController.board[boardCoordinate.a,boardCoordinate.b].elementCode = _element;

			PuzzleController.board[tempCoor.a,tempCoor.b].position = other.GetComponent<ElementController>()._position;
			PuzzleController.board[tempCoor.a,tempCoor.b].elementCode = other.GetComponent<ElementController>()._element;


			Debug.Log("[4] [3] : " + PuzzleController.board[4,3].elementCode);
			Debug.Log("[3] [3] : " + PuzzleController.board[3,3].elementCode);
		}
	}	
}
