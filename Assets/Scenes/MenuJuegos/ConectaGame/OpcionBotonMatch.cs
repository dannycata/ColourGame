using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class OpcionBotonMatch : MonoBehaviour
{
	
	private SpriteRenderer staticSprite = null;
	//private Image imagen=null;
	private Color r_color = Color.black;
	private Match match;

	public OpcionMatch OpcionMatch {get; set;}
	
	void Awake()
	{
		//imagen = GetComponent<Image>();
		staticSprite = GetComponent<SpriteRenderer>();
		match = GetComponentInChildren<Match>();
	}

	public void Construtc(OpcionMatch opcion)
	{
		if (opcion.matchID == match.Get_ID()){
			staticSprite.color = opcion.color;
			//imagen.color = opcion.color;
			OpcionMatch = opcion;
		}
	}
	
	public void SetColor(Color c)
	{
		staticSprite.color = c;
	}
}
