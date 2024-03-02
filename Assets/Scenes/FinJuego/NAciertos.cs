using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NAciertos : MonoBehaviour
{
    public Text texto;

    void Start()
    {
		string juego = PlayerPrefs.GetString("Juego");
		Debug.Log(""+juego);
		
		if (juego == "Quiz")
		{
			Quiz();
		}
		else if (juego == "Simon")
		{
			Simon();
		}
		PlayerPrefs.DeleteKey("Juego");
    }
	
	void Quiz()
	{
	int correctAnswers = PlayerPrefs.GetInt("CorrectAnswersQUIZ", 0);
	int incorrectAnswers = PlayerPrefs.GetInt("IncorrectAnswersQUIZ", 0);
	int n_question = PlayerPrefs.GetInt("NPreguntas", 5);

    texto.text = "Numero de aciertos: " + correctAnswers + "   Numero de fallos: " + incorrectAnswers + "   Numero de preguntas: " + n_question;
	}
	
	void Simon()
	{
	int correctAnswers = PlayerPrefs.GetInt("CorrectAnswersSimon", 0);

    texto.text = "Numero de secuencias correctas: " + correctAnswers;
	}
}
