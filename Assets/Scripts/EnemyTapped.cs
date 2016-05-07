using UnityEngine;
using System.Collections;

public class EnemyTapped : MonoBehaviour {

	void OnMouseDown()
	{
		TempScore.tempPlayerScore++;
		TempScore.UpdateText();
		gameObject.SetActive(false);
	}
}
