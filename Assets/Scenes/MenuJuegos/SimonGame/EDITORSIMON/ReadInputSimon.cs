using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadInputSimon : MonoBehaviour
{
    private float velocidad;
	private int secuencias;
	private float destellos;
    private InputField inputFieldVelocidad;
	private InputField inputFieldSecuencia;
	private InputField inputFieldDestello;
    private Text v_warning;
	private Text s_warning;
	private Text d_warning;
	private string nombre;
	[SerializeField] private Color rojo;
	[SerializeField] private Color gris;

    void Start()
    {
		nombre = PlayerPrefs.GetString("Nombre", "");
        inputFieldVelocidad = GameObject.Find("InputFieldVelocidad").GetComponent<InputField>();
		inputFieldSecuencia = GameObject.Find("InputFieldSecuencia").GetComponent<InputField>();
		inputFieldDestello = GameObject.Find("InputFieldDestello").GetComponent<InputField>();
		inputFieldVelocidad.placeholder.GetComponent<Text>().text = PlayerPrefs.GetFloat(nombre+"VelocidadSimon", 1f).ToString();
		inputFieldSecuencia.placeholder.GetComponent<Text>().text = PlayerPrefs.GetInt(nombre+"NSecuencias", 5).ToString();
		inputFieldDestello.placeholder.GetComponent<Text>().text = PlayerPrefs.GetFloat(nombre+"TiempoDestello", 0.25f).ToString();
        v_warning = GameObject.Find("v_warning").GetComponent<Text>();
		s_warning = GameObject.Find("s_warning").GetComponent<Text>();
		d_warning = GameObject.Find("d_warning").GetComponent<Text>();
        v_warning.enabled = false;
		s_warning.enabled = false;
		d_warning.enabled = false;

        inputFieldVelocidad.onEndEdit.AddListener(Read);
		inputFieldSecuencia.onEndEdit.AddListener(Read2);
		inputFieldDestello.onEndEdit.AddListener(Read3);
    }

    void Read(string inputValue)
    {
        if (!string.IsNullOrEmpty(inputValue))
		{
			if (float.TryParse(inputValue, out velocidad))
			{
				v_warning.enabled = false;
				if(velocidad >= 0.75f)
				{
					PlayerPrefs.SetFloat(nombre+"VelocidadSimon", velocidad);
					if (PlayerPrefs.GetFloat(nombre+"TiempoDestello", 0.25f) > (velocidad-0.5f))
					{
						PlayerPrefs.SetFloat(nombre+"TiempoDestello", velocidad-0.5f);
						inputFieldDestello.placeholder.GetComponent<Text>().text  = (velocidad - 0.5f).ToString();
						inputFieldDestello.text  = (velocidad - 0.5f).ToString();
						d_warning.text = "Modificado: Debe haber una distancia de 0.5";
						d_warning.color = gris;
						d_warning.enabled = true;
					}
				}
				else
				{
					v_warning.text = "Error: Tiempo debe ser mayor a 0.75";
					PlayerPrefs.SetFloat(nombre+"VelocidadSimon", 1f);
					v_warning.color = rojo;
					v_warning.enabled = true;
				}
			}
			else
			{
				v_warning.text = "Error: Inserta un numero";
				v_warning.color = rojo;
				v_warning.enabled = true;
			}
		}
    }
	
	void Read2(string inputValue)
    {
		if (!string.IsNullOrEmpty(inputValue))
		{
			if (int.TryParse(inputValue, out int result))
			{
				secuencias = result;
				s_warning.text = "Error: Inserta un numero mayor";
				s_warning.enabled = false;
				if (secuencias >= 1) {
					PlayerPrefs.SetInt(nombre+"NSecuencias", secuencias);
				} else s_warning.enabled = true;
			}
			else
			{
				s_warning.text = "Error: Inserta un numero";
				s_warning.enabled = true;
			}
		}
    }
	
	void Read3(string inputValue)
    {
		if (!string.IsNullOrEmpty(inputValue))
		{
			if (float.TryParse(inputValue, out destellos))
			{
				d_warning.enabled = false;
				PlayerPrefs.SetFloat(nombre+"TiempoDestello", destellos);
				if(destellos > 0)
				{
					if (PlayerPrefs.GetFloat(nombre+"VelocidadSimon", 1f) < (destellos+0.5))
					{
						PlayerPrefs.SetFloat(nombre+"VelocidadSimon", destellos+0.5f);
						inputFieldVelocidad.placeholder.GetComponent<Text>().text = (destellos + 0.5f).ToString();
						inputFieldVelocidad.text  = (destellos + 0.5f).ToString();
						v_warning.text = "Modificado: Debe haber una distancia de 0.5";
						v_warning.color = gris;
						v_warning.enabled = true;
					}
				}
				else
				{
					d_warning.text = "Error: Tiempo debe ser mayor a 0";
					PlayerPrefs.SetFloat(nombre+"TiempoDestello", 0.25f);
					d_warning.color = rojo;
					d_warning.enabled = true;
				}
			}
			else
			{
				d_warning.text = "Error: Inserta un numero";
				d_warning.color = rojo;
				d_warning.enabled = true;
			}
		}
    }
}
