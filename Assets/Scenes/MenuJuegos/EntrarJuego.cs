using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EntrarJuego : MonoBehaviour
{
	[SerializeField] private AudioClip sound = null;
    private Button boton;
    public string nombreDeLaNuevaEscena = "QuizGame";
	private string texto = null;
	
	private AudioSource audioSource = null;

    private void Start()
    {
        boton = GetComponent<Button>();
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
        if (boton != null)
        {
            boton.onClick.AddListener(OnClickPlay);
        }
    }
	
    private void OnClickPlay()
    {
		Text textoBoton = boton.GetComponentInChildren<Text>();
        texto = textoBoton.text;
		if (texto == "Quiz"){
			Invoke("Quiz",0.25f);
		} else if (texto == "Simon"){
			Invoke("Simon",0.15f);
		} else if (texto == "Parejas"){
			Invoke("Parejas",0.15f);
		}
    }
	
	private void Quiz()
	{
		PlayerPrefs.DeleteKey("CorrectAnswersQUIZ");
		PlayerPrefs.DeleteKey("IncorrectAnswersQUIZ");
		PlayerPrefs.Save();
		audioSource.PlayOneShot(sound);
		Invoke("Escena",0.15f);
	}
	
	private void Simon()
	{
		PlayerPrefs.DeleteKey("CorrectAnswersSimon");
		audioSource.PlayOneShot(sound);
		Invoke("Escena",0.15f);
	}
	
	private void Parejas()
	{
		PlayerPrefs.DeleteKey("CorrectAnswersPair");
		string nombreEscenaActual = SceneManager.GetActiveScene().name;
		if (nombreEscenaActual == "MenuJuegos") nombreDeLaNuevaEscena = "PairGame"+ PlayerPrefs.GetString("Dimensiones", "4x2");
		audioSource.PlayOneShot(sound);
		Invoke("Escena",0.15f);
	}
	
	private void Escena()
	{
		SceneManager.LoadScene(nombreDeLaNuevaEscena);
	}
}


