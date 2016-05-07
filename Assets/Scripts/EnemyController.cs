using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	void OnMouseDown()
	{
		//add score
		TempScore.tempPlayerScore++;
		TempScore.UpdateText();
		this.gameObject.SetActive(false);
		
	}

}
