using UnityEngine;
using System.Collections;
using DG.Tweening;

using System.Collections.Generic;

public class PuzzleController : MonoBehaviour {

	public GameObject[] elements;
	
	[HideInInspector]
	public Dictionary<int,GameObject> elementsDict;
	
	public static IntGameObjAndVector3[,] board;
	
	private  List<PairInt> _matchList;
	
	void Start () 
	{
		_matchList = new List<PairInt>();
		//_matchList.Add(new pairInt (1,2));
		//_matchList.Add(new pairInt (3,4));
		//Debug.Log(_matchList[0].a+","+_matchList[0].b);
		//Debug.Log(_matchList[1].a+","+_matchList[1].b);

		board = new IntGameObjAndVector3[5,4];
		for (int x=0; x<board.GetLength(0); x++)
		{
			for (int y=0; y<board.GetLength(1); y++)
			{
				board[x,y] = new IntGameObjAndVector3(0, null, Vector3.zero);
			}
		}

		elementsDict = new Dictionary<int,GameObject>();
		
		//Debug.Log(elements.GetLength(0));
		for (int count = 0; count < elements.GetLength(0); count++)
		{
			elementsDict.Add(count,elements[count]);
			//Debug.Log(elementsDict[count]);
		}
		
		DropElementFromTop();
		PrintCollumn();
		CheckPuzzleMatch();
		PrintMatchList();
	}

	void PrintCollumn()
	{
		for (int y = 0; y < board.GetLength(1) ; y++)
		{
			Debug.Log("col "+y+": "+board[0,y].elementCode+","+board[1,y].elementCode+","+board[2,y].elementCode+","+board[3,y].elementCode+","+board[4,y].elementCode);
			Debug.Log("col "+y+": "+board[0,y].gameObject.name+","+board[1,y].gameObject.name+","+board[2,y].gameObject.name+","+board[3,y].gameObject.name+","+board[4,y].gameObject.name);
			Debug.Log("col "+y+": "+board[0,y].position+","+board[1,y].position+","+board[2,y].position+","+board[3,y].position+","+board[4,y].position);
		}
	}

	void PrintMatchList()
	{
		for (int count = 0; count < _matchList.Count; count++)
		{
			Debug.Log(_matchList[count].a+","+_matchList[count].b);
		}
	}
	
	void AssignElementInPuzzle()
	{
		for (int y = 0; y < board.GetLength(1) ; y++)
		{
			for (int x = 0; x < board.GetLength(0) ; x++)
			{
				board[x,y].elementCode = Random.Range(1,elementsDict.Count);
			}
		}
	}
	
	void DropElementFromTop()
	{
		for (int x = 0; x < board.GetLength(0) ; x++)
		{
			Vector3 boardPosition = new Vector3(-2+x,-3);	

			//top position before drop
			Vector3 topPosition = new Vector3(-2+x,5);
			int topDropQueue = 0;
			
			for (int y = board.GetLength(1)-1; y >=0 ; y--)
			{
				//Debug.Log(x+","+y);
				//if element in board empty
				if(board[x,y].elementCode == 0)
				{
					//if board is (not the most top) and (top available), drop
					if(y > 0 && board[x,y-1].elementCode != 0)
					{
						//++drop above element function
						
						
						//assign element now = above, above = 0
						board[x,y] = board[x,y-1];
						board[x,y].gameObject = board[x,y-1].gameObject;					

						board[x,y-1].elementCode = 0;
						board[x,y-1].gameObject = null;
					}
					else
					{
						//add entrance
						Vector3 queuePosition = new Vector3(topPosition.x, topPosition.y+topDropQueue);
						topDropQueue +=1;

						int elementCode = Random.Range(1,elementsDict.Count);
						
						GameObject element = Instantiate(elementsDict[elementCode],queuePosition,Quaternion.identity) as GameObject;
						//drop queue element
						Vector3 position = new Vector3(boardPosition.x, boardPosition.y + (board.GetLength(1)-1-y));
						element.transform.DOLocalMove(position,0.5f,false);

						board[x,y].elementCode = elementCode;
						board[x,y].gameObject = element;
						board[x,y].position = position;
						
						//assign element coordinate
						element.GetComponent<ElementController>().boardCoordinate = new PairInt (x,y);
					}
				}

			}
		}
	}
	
	void CheckPuzzleMatch()
	{
		int totalCombo = 0;
		//check vertical
		for (int x = 0; x < board.GetLength(0) ; x++)
		{
			int matchCount = 0;
			int lastElementCode = 0;
			
			for (int y = 0; y < board.GetLength(1) ; y++)
			{
				//if same as last, add matchCount
				if(lastElementCode == board[x,y].elementCode)
				{
					matchCount++;
				}
				else
				{
					//check if matchCount >= 2
					if (matchCount>=2)
					{
						for (int count=0; count <= matchCount; count++)
						{
							//insert to match list
							_matchList.Add(new PairInt (x,y-1-count));
						}
						totalCombo++;
					}

					//reset matchCount
					matchCount = 0;
				}
				//assign last element to now
				lastElementCode = board[x,y].elementCode;
			}
			
			//check matchCount before change collumn
			if (matchCount>=2)
			{
				for (int count=0; count <= matchCount; count++)
				{
					//insert to match list
					_matchList.Add(new PairInt (x,board.GetLength(1)-1 -count));
				}
				totalCombo++;
			}
		}

		//check horizontal
		for (int y = 0; y < board.GetLength(1) ; y++)
		{
			int matchCount = 0;
			int lastElementCode = 0;
			
			for (int x = 0; x < board.GetLength(0) ; x++)
			{
				//if same as last, add matchCount
				if(lastElementCode == board[x,y].elementCode)
				{
					matchCount++;
				}
				else
				{
					//check if matchCount >= 2
					if (matchCount>=2)
					{
						for (int count=0; count <= matchCount; count++)
						{
							//insert to match list
							_matchList.Add(new PairInt (x-1-count,y));
						}
						totalCombo++;
					}

					//reset matchCount
					matchCount = 0;
				}
				//assign last element to now
				lastElementCode = board[x,y].elementCode;
			}
			
			//check matchCount before change row
			if (matchCount>=2)
			{
				for (int count=0; count <= matchCount; count++)
				{
					//insert to match list
					_matchList.Add(new PairInt (board.GetLength(0)-1 -count,y));
				}
				totalCombo++;
			}
		}
		Debug.Log("total combo : "+ totalCombo);
	}

}

public class PairInt
{
	public int a;
	public int b;

	public PairInt(int assigned_a, int assigned_b)
	{
		a = assigned_a;
		b = assigned_b;
	}
}

public class IntGameObjAndVector3
{
	public int elementCode;
	public GameObject gameObject;
	public Vector3 position;
	
	public IntGameObjAndVector3(int assigned_code, GameObject assigned_gameObject, Vector3 assigned_position)
	{
		elementCode = assigned_code;
		gameObject = assigned_gameObject;
		position = assigned_position;
	}
}