using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NAciertos : MonoBehaviour
{
    public Text correctAnswersText;

    void Start()
    {
        int correctAnswers = PlayerPrefs.GetInt("CorrectAnswersQUIZ", 0);
		int incorrectAnswers = PlayerPrefs.GetInt("IncorrectAnswersQUIZ", 0);

        correctAnswersText.text = "Numero de aciertos: " + correctAnswers + "   Numero de fallos: " + incorrectAnswers;
    }
}
