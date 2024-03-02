using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerMatch : MonoBehaviour
{
	private MatchDB matchDBComponent = null;
	private GameObject matchDBFObject = null;
	private MatchUI r_matchUI = null;
	private PreguntaMatch question = null;
	private GameObject Cards;
	private List<Match> matches = new List<Match>();
	
    private void Start()
    {
		PlayerPrefs.SetString("Juego", "Match");
		matchDBFObject = GameObject.Find(PlayerPrefs.GetString("Nivel", "Nivel Facil"));
		matchDBComponent = matchDBFObject.GetComponent<MatchDB>();
		r_matchUI = GameObject.FindObjectOfType<MatchUI>();
		Cards = GameObject.Find("Cards");
		matches.AddRange(Cards.GetComponentsInChildren<Match>());
		FirstQuestion();
    }
	
	public void FirstQuestion()
	{
		question = matchDBComponent.GetRandom();
		Debug.Log("Question:"+question);
		if (question != null)
		{
			r_matchUI.Construtc(question);
		} 
	}
	
	public void NextQuestion()
	{
		question = matchDBComponent.GetRandom();
		Debug.Log("Question:"+question);
		if(question != null)
		{
			matches.AddRange(Cards.GetComponentsInChildren<Match>());
			foreach (Match match in matches)
			{
				match.eraseLine();
			}
			matches.Clear();
			Cards.SetActive(false);
			Invoke("Constructor",0.5f);
		} 
		else
		{
			SceneManager.LoadScene("FinJuego");
		}
	}
	
	private void Constructor()
	{
		r_matchUI.Construtc(question);
		Cards.SetActive(true);
	}
}
