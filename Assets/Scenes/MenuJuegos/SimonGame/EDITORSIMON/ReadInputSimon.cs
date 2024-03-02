using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadInputSimon : MonoBehaviour
{
    private float velocidad;
	private int secuencias;
    private InputField inputFieldVelocidad;
	private InputField inputFieldSecuencia;
    private Text v_warning;
	private Text s_warning;

    void Start()
    {
        inputFieldVelocidad = GameObject.Find("InputFieldVelocidad").GetComponent<InputField>();
		inputFieldSecuencia = GameObject.Find("InputFieldSecuencia").GetComponent<InputField>();
		inputFieldVelocidad.placeholder.GetComponent<Text>().text = PlayerPrefs.GetFloat("VelocidadSimon", 0.4f).ToString();
		inputFieldSecuencia.placeholder.GetComponent<Text>().text = PlayerPrefs.GetInt("NSecuencias", 5).ToString();
        v_warning = GameObject.Find("v_warning").GetComponent<Text>();
		s_warning = GameObject.Find("s_warning").GetComponent<Text>();
        v_warning.enabled = false;
		s_warning.enabled = false;

        inputFieldVelocidad.onEndEdit.AddListener(Read);
		inputFieldSecuencia.onEndEdit.AddListener(Read2);
    }

    void Read(string inputValue)
    {
        if (!string.IsNullOrEmpty(inputValue))
		{
			if (float.TryParse(inputValue, out velocidad))
			{
				v_warning.enabled = false;
				PlayerPrefs.SetFloat("VelocidadSimon", velocidad);
			}
			else
			{
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
					PlayerPrefs.SetInt("NSecuencias", secuencias);
				} else s_warning.enabled = true;
			}
			else
			{
				s_warning.enabled = true;
			}
		}
    }
}
