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
	
    private QuizDB r_quizDB = null;
	private QuizUI r_quizUI = null;
	private AudioSource r_audioSource = null;
	public GameObject panel = null;
	public Image imagenPanel = null;
	private int correctAnswers = 0;
	private int incorrectAnswers = 0;
	private Pregunta question = null;
	private Timer r_timer = null;
	
	private void Start()
	{
		r_quizDB = GameObject.FindObjectOfType<QuizDB>();
		r_quizUI = GameObject.FindObjectOfType<QuizUI>();
		r_timer = GameObject.FindObjectOfType<Timer>();
		r_audioSource = GetComponent<AudioSource>();
		
		r_timer.Starts();
	}
	
	public bool NextQuestion()
	{
		question = r_quizDB.GetRandom();
		if (question != null)
		{
			r_timer.ResetTimer();
			r_quizUI.Construtc(question, GiveAnswer);
			panel.SetActive(false);
			return true;
		}
		return false;
	}
	
	private void GiveAnswer(OpcionBoton optionButton)
	{
		StartCoroutine(GiveAnswerRoutine(optionButton));
	}
	
	private IEnumerator GiveAnswerRoutine(OpcionBoton optionButton)
	{
		if(r_audioSource.isPlaying)
			r_audioSource.Stop();
		
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
		
		yield return new WaitForSeconds(r_waitTime);
		
		NextQuestion();
	}
}
