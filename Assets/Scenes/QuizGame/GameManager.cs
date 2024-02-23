using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
	[SerializeField] private AudioClip r_correctSound = null;
	[SerializeField] private AudioClip r_incorrectSound = null;
	[SerializeField] private Color r_correctColor = Color.black;
	[SerializeField] private Color r_incorrectColor = Color.black;
	[SerializeField] private float r_waitTime = 0.0f;
	[SerializeField] private Sprite i_correct = null;
	[SerializeField] private Sprite i_incorrect = null;
	[SerializeField] private GameObject panel = null;
	
	private QuizDB quizDBComponent = null;
	private GameObject quizDBFObject = null;
	private QuizUI r_quizUI = null;
	private AudioSource r_audioSource = null;
	public Image imagenPanel = null;
	private int correctAnswers = 0;
	private int incorrectAnswers = 0;
	public Pregunta question = null;
	private Timer r_timer = null;
	public int n_question = 0;
	
	
	private void Start()
	{
		n_question = PlayerPrefs.GetInt("NPreguntas", 5);
		quizDBFObject = GameObject.Find(PlayerPrefs.GetString("Nivel", "Nivel Facil"));
		quizDBComponent = quizDBFObject.GetComponent<QuizDB>();
		r_quizUI = GameObject.FindObjectOfType<QuizUI>();
		r_timer = GameObject.FindObjectOfType<Timer>();
		r_audioSource = GetComponent<AudioSource>();
		
		r_timer.Starts();
	}
	
	public void NextQuestion()
	{
		question = quizDBComponent.GetRandom();
		if (question != null && n_question != 0)
		{
			r_timer.ResetTimer();
			r_quizUI.Construtc(question, GiveAnswer);
			panel.SetActive(false);
			n_question--;
		} else{
			r_timer.Stop();
			SceneManager.LoadScene("FinJuego");
		}
	}
	
	public void GiveAnswer(OpcionBoton optionButton)
	{
		if (r_audioSource.isPlaying)
		{
			r_audioSource.Stop();
		}
		r_audioSource.clip = optionButton.Opcion.correct ? r_correctSound : r_incorrectSound;
		//optionButton.SetColor(optionButton.Opcion.correct ? r_correctColor : r_incorrectColor);
		imagenPanel.sprite = optionButton.Opcion.correct ? i_correct : i_incorrect;
		panel.SetActive(true);

		if (optionButton.Opcion.correct)
		{
			correctAnswers++;
			PlayerPrefs.SetInt("CorrectAnswersQUIZ", correctAnswers);
		}
		else
		{
			incorrectAnswers++;
			PlayerPrefs.SetInt("IncorrectAnswersQUIZ", incorrectAnswers);
		}
		PlayerPrefs.Save();

		r_audioSource.Play();
		Invoke("NextQuestion", r_waitTime);
	}
}
