using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TempScore : MonoBehaviour
{
	public GameObject player;
	public GameObject boss;

	private static PlayerController _playerCont;
	private static BossController _bossCont;
	
	public static int tempPlayerScore;
	public static int tempEnemyScore;
	
	public GameObject tempPlayerText;
	public GameObject tempEnemyText;
	
	private static Text _tempPlayerText;
	private static Text _tempEnemyText;

	void Start()
	{
		_playerCont = player.GetComponent<PlayerController>();
		_bossCont = boss.GetComponent<BossController>();	

		_tempPlayerText = tempPlayerText.GetComponent<Text>();
		_tempEnemyText = tempEnemyText.GetComponent<Text>();
	}

	public static void ClearTempScore ()
	{
		tempPlayerScore = 0;
		tempEnemyScore = 0;
	}

	public static void UpdateText()
	{
		_tempPlayerText.GetComponent<Text>().text = tempPlayerScore.ToString();
		_tempEnemyText.GetComponent<Text>().text = tempEnemyScore.ToString();
	}
	

	public static void CompareTempScore ()
	{
		if (tempPlayerScore >= tempEnemyScore) {
			_playerCont.CoAttack();

		} else {
			_bossCont.CoAttack();
		}
	}

}
