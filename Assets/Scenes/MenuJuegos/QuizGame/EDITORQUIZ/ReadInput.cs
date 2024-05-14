using System.Collections;
using System;
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
	private string nombre;
	private string[] datos;
	private string[] colores;
	private int maximo;
	[SerializeField] Text minmax;
	[SerializeField] Toggle toggleButton;

    void Start()
    {
		nombre = PlayerPrefs.GetString("Nombre", "");
        inputFieldTiempo = GameObject.Find("InputFieldTiempo").GetComponent<InputField>();
		inputFieldPregunta = GameObject.Find("InputFieldPregunta").GetComponent<InputField>();
		inputFieldTiempo.placeholder.GetComponent<Text>().text = PlayerPrefs.GetFloat(nombre+"VariableTiempo", 5f).ToString();
        t_warning = GameObject.Find("t_warning").GetComponent<Text>();
		q_warning = GameObject.Find("q_warning").GetComponent<Text>();
        t_warning.enabled = false;
		q_warning.enabled = false;
		toggleButton.isOn = PlayerPrefs.GetInt(nombre+"TiempoInvisible", 0) == 1;
		toggleButton.onValueChanged.AddListener(delegate { OnToggleChanged(toggleButton); });
		
		string datosString = PlayerPrefs.GetString(nombre+"DatosPreguntas","");
        datos = datosString.Split(',');
		string datoscolores = PlayerPrefs.GetString(nombre+"Colores", "");
		colores = datoscolores.Split(',');
		if (datosString != "" && datoscolores != "") maximo = (colores.Length * 6) + datos.Length;
		else if (datosString != "" && datoscolores == "") maximo = datos.Length;
		else maximo = (colores.Length * 12);
		minmax.text = "Min 1 - Max "+maximo;
		
		if (PlayerPrefs.GetInt(nombre+"NPreguntas", 5) <= maximo)
		{
			inputFieldPregunta.placeholder.GetComponent<Text>().text = PlayerPrefs.GetInt(nombre+"NPreguntas", 5).ToString();
		}
		else
		{
			inputFieldPregunta.placeholder.GetComponent<Text>().text = maximo.ToString();
			PlayerPrefs.SetInt(nombre+"NPreguntas", maximo);
		}

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
				PlayerPrefs.SetFloat(nombre+"VariableTiempo", tiempo);
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
				if (preguntas >= 1 && preguntas <= maximo) {
					PlayerPrefs.SetInt(nombre+"NPreguntas", preguntas);
				} else q_warning.enabled = true;
			}
			else
			{
				q_warning.text = "Error: Inserta un numero";
				q_warning.enabled = true;
			}
		}
    }
	
	public void OnToggleChanged(Toggle changedToggle)
    {
		int toggleState=0;
		if (changedToggle.isOn) toggleState = 1;
        PlayerPrefs.SetInt(nombre+"TiempoInvisible", toggleState);
    }
}
