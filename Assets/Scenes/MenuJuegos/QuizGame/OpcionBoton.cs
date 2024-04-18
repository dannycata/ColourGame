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
	[SerializeField] public Sprite i_correct = null;
	[SerializeField] public Sprite i_incorrect = null;
	
	private Text r_text = null;
	private Color r_color = Color.black;
	private Timer r_timer = null;

	public Opcion Opcion {get; set;}

	private void Awake()
	{
		Image imagenBoton = transform.Find("ImagenBoton")?.GetComponent<Image>();
		if (imagenBoton != null)
		{
			imagenBoton.gameObject.SetActive(false);
		}
		r_button = GetComponent<Button>();
		r_image = GetComponent<Image>();
		r_timer = GameObject.FindObjectOfType<Timer>();
		r_text = transform.GetChild(0).GetComponent<Text>();
		
		r_color = r_image.color;
	}

	public void Construtc(Opcion opcion, Action<OpcionBoton> callback)
	{
		r_text.text = opcion.text;
		
		r_button.onClick.RemoveAllListeners();
		r_button.enabled = true;
		r_image.color = opcion.color;
		
		Opcion = opcion;
		
		r_button.onClick.AddListener(delegate
		{
			callback(this);
			r_timer.Stop();
		});
	}
	
	public void SetColor(Color c)
	{
		r_button.enabled = false;
		r_image.color = c;
	}
}
