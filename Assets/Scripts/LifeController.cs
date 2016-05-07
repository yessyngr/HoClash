using UnityEngine;
using System.Collections;
using DG.Tweening;

public class LifeController : MonoBehaviour 
{
	public GameObject damagedHPBar;
	public GameObject fullHPBar;

	public GameObject bossDamagedHPBar;
	public GameObject bossFullHPBar;

	private static RectTransform _playerDamagedHPBar;
	private static RectTransform _playerFullHPbar;

	private static RectTransform _bossDamagedHPBar;
	private static RectTransform _bossFullHPbar;

	public static float startingPlayerLife;
	private static float _playerLife;
	public static float playerAttackDamage;
	private static float playerDamagePercentage;

	public static float startingBossLife;
	private static float _bossLife;
	public static float bossAttackDamage;
	private static float bossDamagePercentage;	

	void Start () 
	{
		_playerDamagedHPBar = damagedHPBar.GetComponent<RectTransform>();
		_playerFullHPbar = fullHPBar.GetComponent<RectTransform>();

		_bossDamagedHPBar = bossDamagedHPBar.GetComponent<RectTransform>();
		_bossFullHPbar = bossFullHPBar.GetComponent<RectTransform>();
		
		InitializeCharacterData();
		
		//DecreasePlayerLife();
		//DecreaseBossLife();
	}

	void InitializeCharacterData()
	{
		bossAttackDamage = JsonManager.CharacterDatabase().boss.attack;
		playerAttackDamage = JsonManager.CharacterDatabase().player.attack;

		startingPlayerLife = JsonManager.CharacterDatabase().player.HP;
		startingBossLife = JsonManager.CharacterDatabase().boss.HP;

		_playerLife = startingPlayerLife;
		_bossLife = startingBossLife;
		
	}

	public static void DecreasePlayerLife()
	{
		playerDamagePercentage = playerDamagePercentage + bossAttackDamage/startingPlayerLife;
		Vector2 decreasePlayerHPPos = new Vector2(_playerFullHPbar.rect.width * playerDamagePercentage, _playerDamagedHPBar.rect.height);
		_playerDamagedHPBar.DOSizeDelta(decreasePlayerHPPos,1.0f,false);
		_playerLife = _playerLife - bossAttackDamage;
		
		CheckLife();
	}

	public static void DecreaseBossLife()
	{
		bossDamagePercentage = bossDamagePercentage + playerAttackDamage/startingBossLife;
		Vector2 decreaseBossHPPos = new Vector2(_bossFullHPbar.rect.width * bossDamagePercentage, _bossDamagedHPBar.rect.height);
		_bossDamagedHPBar.DOSizeDelta(decreaseBossHPPos,1.0f,false);
		_bossLife = _bossLife - playerAttackDamage;
		
		CheckLife();
	}

	static void CheckLife()
	{
		if (_playerLife <= 0)
		{
			//show GameOver screen
			//Return To Main Menu
			Debug.Log("player dead");
		}
		else if(_bossLife <= 0)
		{
			//either go to next boss or
			//stage finished!
			Debug.Log("stage finished");
		}
	}

}