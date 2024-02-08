using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
	[SerializeField] private AudioClip r_correctSound = null;
	[SerializeField] private AudioClip r_incorrectSound = null;
	[SerializeField] private Color r_correctColor = Color.black;
	[SerializeField] private Color r_incorrectColor = Color.black;
	[SerializeField] private float r_waitTime = 0.0f;
	
    private QuizDB r_quizDB = null;
	private QuizUI r_quizUI = null;
	private AudioSource r_audioSource = null;
	public int correctAnswers = 0;
	public int incorrectAnswers = 0;
	
	private void Start()
	{
		r_quizDB = GameObject.FindObjectOfType<QuizDB>();
		r_quizUI = GameObject.FindObjectOfType<QuizUI>();
		r_audioSource = GetComponent<AudioSource>();
		
		NextQuestion();
	}
	
	private void NextQuestion()
	{
		r_quizUI.Construtc(r_quizDB.GetRandom(), GiveAnswer);
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
		optionButton.SetColor(optionButton.Opcion.correct ? r_correctColor : r_incorrectColor);
		
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
