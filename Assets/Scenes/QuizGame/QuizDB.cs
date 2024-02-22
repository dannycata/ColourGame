using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizDB : MonoBehaviour
{
    [SerializeField] private List<Pregunta> r_questionlist = null;
	
	private List<Pregunta> r_backup = null;
	
	private void Awake()
	{
		r_backup = r_questionlist.ToList();
	}
	
	public Pregunta GetRandom(bool remove = true)
	{
		if(r_questionlist.Count == 0)
		{
			//SceneManager.LoadScene("FinJuego");
			return null;
		}
			
		
		int index = Random.Range(0, r_questionlist.Count);
		
		if(!remove)
			return r_questionlist[index];
		
		Pregunta p = r_questionlist[index];
		r_questionlist.RemoveAt(index);
		
		return p;
	}
}
