using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BossController : MonoBehaviour {

	public GameObject player;
	
	private Vector3 startPosition;

	void Start ()
	{
		startPosition = GetComponent<Transform> ().position;
	}

	public void CoAttack ()
	{
		StartCoroutine (Attack ());
	}

	IEnumerator Attack ()
	{
		Vector3 playerPosition = player.GetComponent<Transform> ().position;
		Tween runToPlayer = transform.DOMove (playerPosition, 2.0f, false);
		yield return runToPlayer.WaitForCompletion ();
		
		//do attack stance here
		Tween attacking = transform.DOShakePosition (1.0f, 1, 10, 10, false);
		LifeController.DecreasePlayerLife();
		yield return attacking.WaitForCompletion ();

		Tween returnToPosition = transform.DOMove (startPosition, 2.0f, false);
		yield return returnToPosition.WaitForCompletion ();
		
		//start next wave
	}
}
