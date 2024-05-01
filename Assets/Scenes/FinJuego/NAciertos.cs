using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NAciertos : MonoBehaviour
{
    public Text texto;
	public static string juego;
	private string nombre;
	private string datos;
	private DateTime now;
    private string formattedDateTime;

    void Start()
    {
		DateTime now = DateTime.Now;
		formattedDateTime = now.ToString("dd-MM-yyyy HH:mm:ss");
		nombre = PlayerPrefs.GetString("Nombre", "");
		juego = PlayerPrefs.GetString("Juego");
		
		if (juego == "Quiz")
		{
			Quiz();
		}
		else if (juego == "Simon")
		{
			Simon();
		}
		else if (juego == "Pair")
		{
			Pair();
		}
		PlayerPrefs.DeleteKey("Juego");
    }
	
	void Quiz()
	{
	int correctAnswers = PlayerPrefs.GetInt(nombre+"CorrectAnswersQUIZ", 0);
	int incorrectAnswers = PlayerPrefs.GetInt(nombre+"IncorrectAnswersQUIZ", 0);
	int n_question = PlayerPrefs.GetInt(nombre+"NPreguntas", 5);

    texto.text = "Numero de preguntas: " + n_question + "   Aciertos: " + correctAnswers + "   Fallos: " + incorrectAnswers;
	
	datos = PlayerPrefs.GetString(nombre+"DatosQuiz","") + formattedDateTime + "    " + texto.text + ",";
	PlayerPrefs.SetString(nombre+"DatosQuiz", datos);
	
	PlayerPrefs.SetInt(nombre+"CorrectAnswersQUIZ",0);
	PlayerPrefs.SetInt(nombre+"IncorrectAnswersQUIZ",0);
	}
	
	void Simon()
	{
	int correctAnswers = PlayerPrefs.GetInt(nombre+"CorrectAnswersSimon", 0);
	int secuencias = PlayerPrefs.GetInt(nombre+"NSecuencias", 5);

    texto.text = "Numero de secuencias totales: " + secuencias + "   Correctas: " +correctAnswers;
	
	datos = PlayerPrefs.GetString(nombre+"DatosSimon","") + formattedDateTime + "    " + texto.text + ",";
	PlayerPrefs.SetString(nombre+"DatosSimon", datos);
	
	PlayerPrefs.SetInt(nombre+"CorrectAnswersSimon",0);
	}
	
	void Pair()
	{
	int correctAnswers = PlayerPrefs.GetInt(nombre+"CorrectAnswersPair", 0);
	int attempts = PlayerPrefs.GetInt(nombre+"AttemptsPair", 0);
	string dimensiones = PlayerPrefs.GetString(nombre+"Dimensiones", "4x2");

    texto.text = "Dimensiones: " + dimensiones + "   Aciertos: " + correctAnswers + "   Intentos: " + attempts ;
	
	datos = PlayerPrefs.GetString(nombre+"DatosPair","") + formattedDateTime + "    " + texto.text + ",";
	PlayerPrefs.SetString(nombre+"DatosPair", datos);
	
	PlayerPrefs.SetInt(nombre+"CorrectAnswersPair",0);
	PlayerPrefs.SetInt(nombre+"AttemptsPair",0);
	}
}
