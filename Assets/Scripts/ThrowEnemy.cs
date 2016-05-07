using UnityEngine;
using System.Collections;
using DG.Tweening;

using System.Collections.Generic;

public class ThrowEnemy : MonoBehaviour {

	[SerializeField]
	private GameObject[] enemies;
	[SerializeField]
	private int totalEnemyEachWave;
	private int _enemyLeftInWaveCount;
	
	private Vector3 maxWorldPoint;
	private List<GameObject> offScreenEnemiesList;

	void Start () 
	{
		offScreenEnemiesList = new List<GameObject>();
		for (int count = 0; count < enemies.Length; count++) 
		{
			offScreenEnemiesList.Add(enemies[count]);
		}
		
		Vector3 screenDisplay = new Vector3(Camera.main.pixelWidth,Camera.main.pixelHeight);
		maxWorldPoint = Camera.main.ScreenToWorldPoint(screenDisplay);
		
		_enemyLeftInWaveCount = totalEnemyEachWave;

		//StartThrowing();
	}

	void StartThrowing()
	{
		CoThrowingEnemy();
	}
	
	void CoThrowingEnemy()
	{	
		StartCoroutine(ThrowingEnemy());
		_enemyLeftInWaveCount--;
	}
	
	IEnumerator ThrowingEnemy()
	{
		GameObject enemy = offScreenEnemiesList[GetEnemyCode()];
		enemy.transform.position = transform.position;
		offScreenEnemiesList.Remove(enemy);
		Debug.Log(_enemyLeftInWaveCount);

		//throw more enemies when enemies in waves is available
		if(_enemyLeftInWaveCount > 0)
		{
			float randomSec = Random.Range(1.3f,2.0f);
			yield return new WaitForSeconds(randomSec);
			CoThrowingEnemy();

			enemy.SetActive(true);
			float randomXrange = Random.Range(-maxWorldPoint.x, maxWorldPoint.x);

			Vector3 jumpMaxPosition = new Vector3(randomXrange , -2.5f);
			Tween enemyJumpTween = enemy.transform.DOJump(jumpMaxPosition,1.0f,1,1.0f,false);
			yield return enemyJumpTween.WaitForCompletion();

			Vector3 fallingPosition = new Vector3(enemy.GetComponent<Transform>().position.x+randomXrange, 
										GetComponent<Transform>().position.y);
			Tween falling = enemy.transform.DOJump(fallingPosition,1.0f,1,1.0f,false);
			yield return falling.WaitForCompletion();
			
			//if enemy missed by player
			if(enemy.activeSelf)
			{
				if(enemy.tag!="bomb")
				{
					TempScore.tempEnemyScore+=2;
					TempScore.UpdateText();	
				}
				enemy.SetActive(false);
			}
			
			offScreenEnemiesList.Add(enemy);
		}
		else
		{
			
			yield return new WaitForSeconds(2.0f);
			Debug.Log("zero");
			TempScore.CompareTempScore();
		}
	}

	int GetEnemyCode()
	{
		int randomNum = Random.Range(0,offScreenEnemiesList.Count);
		return randomNum;
	}
	
}
