using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnObject : MonoBehaviour
{

	[SerializeField]
	private GameObject _spawnedItems;
	private int _x, _y;
	
	private float popDelay = 1f;
	private int enemyAmount = 5;
	
	[SerializeField]
	private GameObject[] gameObjectArray;


	void Start ()
	{	
		for (int i = 0; i < gameObjectArray.Length; i++) {
			gameObjectArray [i].gameObject.SetActive (false);
		}
		
		CoPopEnemy (popDelay, enemyAmount);
		
	}

	void CoPopEnemy (float delay, int amount)
	{
		GenerateRandomPosition ();
		StartCoroutine (PopEnemy (delay, amount));
	}

	IEnumerator PopEnemy (float delay, int amount)
	{
		gameObjectArray [_x].gameObject.SetActive (true);
		yield return new WaitForSeconds (delay);
		
		//if enemy not tapped
		if (gameObjectArray [_x].gameObject.activeSelf) 
		{
			gameObjectArray [_x].gameObject.SetActive (false);
			TempScore.tempEnemyScore++;
			TempScore.UpdateText();
		} 
		amount--;
		
		//if wave finished
		if (amount <= 0) 
		{
			//compare temp score
			TempScore.CompareTempScore();
			yield return null;
		} 
		else 
		{
			yield return new WaitForSeconds (delay);
			CoPopEnemy (delay, amount);
		}
	}
	
	void Attack()
	{
		TempScore.CompareTempScore();
		if(TempScore.tempPlayerScore > TempScore.tempEnemyScore)
		{
			//LifeController.ReduceBossLife();
		}
		else
		{
			//LifeController.DecreasePlayerLife();
		}	
	}


	Position PositionInstance ()
	{
		Position pos = new Position ();
		return pos;
	}

	void GenerateRandomPosition ()
	{
		Position pos = PositionInstance ();
		
		pos.x = Random.Range (0, gameObjectArray.Length);
		
		_x = pos.x;
	}

	void PopEnemy (int x, int y, float delay)
	{
		Debug.Log (x + "," + y);
	}
	
}

public class GameObjectArray
{
	public GameObject[] gameObjectArray;
}

public class Position
{
	public int x {
		get;
		set;
	}
}
