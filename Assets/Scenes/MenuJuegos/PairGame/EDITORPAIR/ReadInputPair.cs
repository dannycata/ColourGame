using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadInputPair : MonoBehaviour
{
    private float velocidad;
	private int secuencias;
    private InputField inputFieldVelocidad;
	private InputField inputFieldSecuencia;
    private Text v_warning;
	private Text r_warning;

    void Start()
    {
        inputFieldVelocidad = GameObject.Find("InputFieldVelocidad").GetComponent<InputField>();
		inputFieldVelocidad.placeholder.GetComponent<Text>().text = PlayerPrefs.GetFloat("VelocidadPair", 40f).ToString();
        v_warning = GameObject.Find("v_warning").GetComponent<Text>();
        v_warning.enabled = false;
		r_warning = GameObject.Find("r_warning").GetComponent<Text>();
        r_warning.enabled = true;

        inputFieldVelocidad.onEndEdit.AddListener(Read);
    }
	
	void Update()
	{
		string dimensiones = PlayerPrefs.GetString("Dimensiones", "4x2");
		if (dimensiones == "4x2")
		{
			r_warning.text = "Recomendado 40 o mas";
			
		}
		else if (dimensiones == "6x2")
		{
			r_warning.text = "Recomendado 60 o mas";
			
		}
		else if (dimensiones == "6x3")
		{
			r_warning.text = "Recomendado 90 o mas";
			
		}
	}

    void Read(string inputValue)
    {
        if (!string.IsNullOrEmpty(inputValue))
		{
			if (float.TryParse(inputValue, out velocidad))
			{
				v_warning.enabled = false;
				r_warning.enabled = true;
				PlayerPrefs.SetFloat("VelocidadPair", velocidad);
			}
			else
			{
				r_warning.enabled = false;
				v_warning.enabled = true;
			}
		}
    }
}
