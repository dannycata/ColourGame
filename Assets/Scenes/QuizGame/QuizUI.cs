using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private Text r_question = null;
	[SerializeField] private Image r_questioncolor = null;
	[SerializeField] private List<OpcionBoton> r_buttonList = null;
	
	public void Construtc(Pregunta p, Action<OpcionBoton> callback)
	{
		r_question.text = p.text;
		
		for (int n=0; n<r_buttonList.Count ; n++)
		{
			r_buttonList[n].Construtc(p.opciones[n], callback);
		}
		
		if (r_questioncolor != null)
		{
			r_questioncolor.color = p.color;
		}
	}
}
