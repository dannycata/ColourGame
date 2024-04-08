using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadInput : MonoBehaviour
{
    private float tiempo;
	private int preguntas;
    private InputField inputFieldTiempo;
	private InputField inputFieldPregunta;
    private Text t_warning;
	private Text q_warning;

    void Start()
    {
        inputFieldTiempo = GameObject.Find("InputFieldTiempo").GetComponent<InputField>();
		inputFieldPregunta = GameObject.Find("InputFieldPregunta").GetComponent<InputField>();
		inputFieldTiempo.placeholder.GetComponent<Text>().text = PlayerPrefs.GetFloat("VariableTiempo", 5f).ToString();
		inputFieldPregunta.placeholder.GetComponent<Text>().text = PlayerPrefs.GetInt("NPreguntas", 5).ToString();
        t_warning = GameObject.Find("t_warning").GetComponent<Text>();
		q_warning = GameObject.Find("q_warning").GetComponent<Text>();
        t_warning.enabled = false;
		q_warning.enabled = false;

        inputFieldTiempo.onEndEdit.AddListener(Read);
		inputFieldPregunta.onEndEdit.AddListener(Read2);
    }

    void Read(string inputValue)
    {
        if (!string.IsNullOrEmpty(inputValue))
		{
			if (float.TryParse(inputValue, out tiempo))
			{
				t_warning.enabled = false;
				PlayerPrefs.SetFloat("VariableTiempo", tiempo);
			}
			else
			{
				t_warning.enabled = true;
			}
		}
    }
	
	void Read2(string inputValue)
    {
		if (!string.IsNullOrEmpty(inputValue))
		{
			if (int.TryParse(inputValue, out int result))
			{
				preguntas = result;
				q_warning.text = "Error: Fuera de rango";
				q_warning.enabled = false;
				if (preguntas >= 1 && preguntas <= BotonesColores.totalVariable) {
					PlayerPrefs.SetInt("NPreguntas", preguntas);
				} else q_warning.enabled = true;
			}
			else
			{
				q_warning.text = "Error: Inserta un numero";
				q_warning.enabled = true;
			}
		}
    }
}
