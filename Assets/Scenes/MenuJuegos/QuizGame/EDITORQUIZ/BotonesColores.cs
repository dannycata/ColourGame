using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BotonesColores : MonoBehaviour
{
	[SerializeField] private AudioClip sound = null;
	string hexColor = "#A4FC92";
    private Color colorSeleccionado;
    private Color colorNormal= Color.white;
    private Button boton;
	private Text minmax;
    private string texto;
	public static int totalVariable;
	private string nombre;
	
	private AudioSource audioSource = null;

    private void Start()
    {	
		nombre = PlayerPrefs.GetString("Nombre", "");
		colorSeleccionado = ColorUtility.TryParseHtmlString(hexColor, out Color color) ? color : Color.white;
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
        boton = GetComponent<Button>();
		minmax = GameObject.Find("minmax").GetComponent<Text>();
        Text textoBoton = boton.GetComponentInChildren<Text>();
        texto = textoBoton.text;
		if (PlayerPrefs.GetString(nombre+"Colores", "") == "") PlayerPrefs.SetString(nombre+"Colores", "Rojo,Amarillo,Azul,Verde,Rosa");
        boton.onClick.AddListener(click);
        ActualizarColores();
    }
	
	void Update()
    {	
		string nombresColoresString = PlayerPrefs.GetString(nombre+"Colores", "Rojo,Amarillo,Azul,Verde,Rosa");
		string[] nombresColores = nombresColoresString.Split(',');
		
		totalVariable = Mathf.Min(nombresColores.Length, 8) * 6;
		minmax.text = "Min 1 - Max "+totalVariable;
    }

    private void ActualizarColores()
	{
		string nombresColoresString = PlayerPrefs.GetString(nombre+"Colores", "Rojo,Amarillo,Azul,Verde,Rosa");
		string[] nombresColores = nombresColoresString.Split(',');

		bool colorEncontrado = false;

		foreach (string nombreColor in nombresColores)
		{
			if (texto == nombreColor)
			{
				boton.image.color = colorSeleccionado;
				colorEncontrado = true;
				break;
			}
		}
		if (!colorEncontrado)
		{
			boton.image.color = colorNormal;
		}
	}
	
	private void click()
	{
		audioSource.PlayOneShot(sound);
		if (boton.image.color == colorSeleccionado)
		{
			boton.image.color = colorNormal;
			BorrarColor(texto);
		}
		else
		{
			boton.image.color = colorSeleccionado;
			GuardarColor(texto);
		}
	}
	
	void GuardarColor(string color)
	{
		string nombresColoresString = PlayerPrefs.GetString(nombre+"Colores", "Rojo,Amarillo,Azul,Verde,Rosa");
		string[] nombresColores = nombresColoresString.Split(',');

		if (Array.IndexOf(nombresColores, color) == -1)
		{
			nombresColoresString += (string.IsNullOrEmpty(nombresColoresString) ? "" : ",") + color;

			PlayerPrefs.SetString(nombre+"Colores", nombresColoresString);
			PlayerPrefs.Save();
		}
	}

	
	void BorrarColor(string color)
	{
		string nombresColoresString = PlayerPrefs.GetString(nombre+"Colores", "Rojo,Amarillo,Azul,Verde,Rosa");
		string[] nombresColores = nombresColoresString.Split(',');
		
		int indice = Array.IndexOf(nombresColores, color);
		
		if (indice != -1)
		{
			List<string> nuevaListaNombres = new List<string>(nombresColores);
			nuevaListaNombres.RemoveAt(indice);
			nombresColores = nuevaListaNombres.ToArray();
			
			string nuevaCadenaNombres = string.Join(",", nombresColores);

			PlayerPrefs.SetString(nombre+"Colores", nuevaCadenaNombres);
			PlayerPrefs.Save();
		}
	}
}