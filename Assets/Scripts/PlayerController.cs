using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PlayerController : MonoBehaviour {
	
	public GameObject boss;
	
	private Vector3 startPosition;
	
	void Start()
	{
		startPosition = GetComponent<Transform>().position;
	}
	
	public void CoAttack()
	{
		StartCoroutine(Attack());
	}

	IEnumerator Attack()
	{
		Vector3 bossPosition = boss.GetComponent<Transform>().position;
		Tween runToBoss = transform.DOMove(bossPosition,2.0f,false);
		yield return runToBoss.WaitForCompletion();
		
		//do attack stance here
		Tween attacking = transform.DOShakePosition(1.0f,1,10,10,false);
		LifeController.DecreaseBossLife();
		yield return attacking.WaitForCompletion();

		Tween returnToPosition = transform.DOMove(startPosition,2.0f,false);
		yield return returnToPosition.WaitForCompletion();

		//start next wave
	}
}
