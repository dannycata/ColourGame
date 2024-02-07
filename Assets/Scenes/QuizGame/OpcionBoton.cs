using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class OpcionBoton : MonoBehaviour
{
	
	private Button r_button = null;
	private Image r_image = null;
	private Text r_text = null;
	private Color r_color = Color.black;

	public Opcion Opcion {get; set;}

	private void Awake()
	{
		r_button = GetComponent<Button>();
		r_image = GetComponent<Image>();
		r_text = transform.GetChild(0).GetComponent<Text>();
		
		r_color = r_image.color;
	}

	public void Construtc(Opcion opcion, Action<OpcionBoton> callback)
	{
		r_text.text = opcion.text;
		
		r_button.onClick.RemoveAllListeners();
		r_button.enabled = true;
		r_image.color = r_color;
		
		Opcion = opcion;
		
		r_button.onClick.AddListener(delegate
		{
			callback(this);
		});
	}
	
	public void SetColor(Color c)
	{
		r_button.enabled = false;
		r_image.color = c;
	}
}
