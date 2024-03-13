using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MatchDB : MonoBehaviour
{
    [SerializeField] private List<PreguntaMatch> r_questionlist = null;
	
	private List<PreguntaMatch> r_backup = null;
	
	private void Awake()
	{
		r_backup = r_questionlist.ToList();
	}
	
	public PreguntaMatch GetRandom(bool remove = true)
	{
		if(r_questionlist.Count == 0)
		{
			return null;
		}
			
		int index = Random.Range(0, r_questionlist.Count);
		
		if(!remove)
			return r_questionlist[index];
		
		PreguntaMatch p = r_questionlist[index];
		r_questionlist.RemoveAt(index);
		
		return p;
	}
}
