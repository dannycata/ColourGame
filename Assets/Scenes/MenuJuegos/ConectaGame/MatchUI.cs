using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MatchUI : MonoBehaviour
{
	[SerializeField] private List<OpcionBotonMatch> r_List = null;
	
	public void Construtc(PreguntaMatch p)
	{
		for (int n=0; n<r_List.Count ; n++)
		{
			for (int m=0; m<p.opciones.Count ; m++)
			{
				r_List[n].Construtc(p.opciones[m]);
			}
		}
	}
}
