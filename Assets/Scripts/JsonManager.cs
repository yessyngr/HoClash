using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;


public class JsonManager : MonoBehaviour 
{
	public TextAsset characterDatabaseFile;
	public TextAsset dialogueDatabaseFile;
		
	private static TextAsset _characterDatabaseFile;
	private static TextAsset _dialogueDatabaseFile;

	void Start () 
	{
		_characterDatabaseFile = characterDatabaseFile;
		_dialogueDatabaseFile = dialogueDatabaseFile;

		Debug.Log(CharacterDatabase().player.name);
		Debug.Log(DialogueDatabase().stages[1].stage[2].text);
	}
	
	public static CharacterData CharacterDatabase()
	{
		string characterDatabaseString = _characterDatabaseFile.text.ToString();
		return JsonUtility.FromJson<CharacterData>(characterDatabaseString);
	}

	public static DialogueData DialogueDatabase()
	{
		string dialogueDatabaseString = _dialogueDatabaseFile.text.ToString();
		return JsonUtility.FromJson<DialogueData>(dialogueDatabaseString);
	}
}


//Dialogue Database
[System.Serializable]
public class DialogueData
{
	public List<Stages> stages;
}

[System.Serializable]
public class Stages
{
	public List<Stage> stage;	
}

[System.Serializable]
public class Stage
{
	public int id;
	public string speaker;
	public string text;
}


//Character Database
[System.Serializable]
public class CharacterData
{
	public Player player;
	public Boss boss;
}

[System.Serializable]
public class Player
{
	public string name;
	public float HP;
	public float attack;

}

[System.Serializable]
public class Boss
{
	public string name;
	public float HP;
	public float attack;

}