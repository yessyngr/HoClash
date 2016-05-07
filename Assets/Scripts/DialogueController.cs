using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {


	void Start () 
	{
				
		//string textString = JsonManager.DialogueDatabase().tutorial.id03.text;
		//CoAnimateText(textString);
	}
	
	void CoAnimateText(string text)
	{
		StartCoroutine(AnimateText(text));
	}

	IEnumerator AnimateText(string strComplete)
	{
		int i = 0;
		string str = "";
		
		while (i < strComplete.Length) {
			str += strComplete [i++];
			gameObject.GetComponent<Text>().text = str;
			yield return new WaitForSeconds (0.5F);
		}
		
		
	}


}