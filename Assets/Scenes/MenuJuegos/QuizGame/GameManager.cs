using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
	[SerializeField] private List<OpcionBoton> r_buttonList = null;
	private OpcionBoton opcionboton = null;
	[SerializeField] private AudioClip r_correctSound = null;
	[SerializeField] private AudioClip r_incorrectSound = null;
	[SerializeField] private Color r_correctColor = Color.black;
	[SerializeField] private Color r_incorrectColor = Color.black;
	[SerializeField] private Sprite i_correct = null;
	[SerializeField] private Sprite i_incorrect = null;
	[SerializeField] private GameObject panel = null;
	
	private float r_waitTime = 2f;
	private QuizUI r_quizUI = null;
	private AudioSource r_audioSource = null;
	private int correctAnswers=0;
	private int incorrectAnswers=0;
	public Pregunta question = null;
	private Timer r_timer = null;
	public int n_question = 0;
	private bool acierto = false;
	private QuizDB[] componentesQuizDB = null;
	[SerializeField] private QuizDB nuevaDB = null;
	private string nombre;
	
	
	private void Start()
	{
		nombre = PlayerPrefs.GetString("Nombre", "");
		PlayerPrefs.SetString("Juego", "Quiz");
		n_question = PlayerPrefs.GetInt(nombre+"NPreguntas", 5);
		
		BaseDatos();
		
		r_quizUI = GameObject.FindObjectOfType<QuizUI>();
		r_timer = GameObject.FindObjectOfType<Timer>();
		r_audioSource = GetComponent<AudioSource>();
		
		r_timer.Starts();
	}
	
	private void BaseDatos()
	{
		if (PlayerPrefs.GetString(nombre+"Colores", "") == "" && PlayerPrefs.GetString(nombre + "DatosPreguntas", "") == "") PlayerPrefs.SetString(nombre+"Colores", "Rojo,Amarillo,Azul,Verde,Rosa");
		string nombresColoresString = PlayerPrefs.GetString(nombre+"Colores", "Rojo,Amarillo,Azul,Verde,Rosa");
        string[] nombresColores = nombresColoresString.Split(',');
		if (nombresColoresString != "")
		{
			componentesQuizDB = new QuizDB[nombresColores.Length+1];
			
			for (int i = 0; i < nombresColores.Length; i++) {
			GameObject colorObject = GameObject.Find(nombresColores[i]);
			componentesQuizDB[i+1] = colorObject.GetComponent<QuizDB>();
			}
		}
		else 
		{
			componentesQuizDB = new QuizDB[1];
		}
		componentesQuizDB[0] = nuevaDB.GetComponent<QuizDB>();
	}
	
	public void NextQuestion()
	{
		int random;
		question = null;
		
		if (n_question != 0)
		{
			do {
				random = Random.Range(0, componentesQuizDB.Length);
				question = componentesQuizDB[random].GetRandom();
			} while (question == null);
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
		opcionboton = optionButton;
		r_audioSource.clip = optionButton.Opcion.correct ? r_correctSound : r_incorrectSound;
		//optionButton.SetColor(optionButton.Opcion.correct ? r_correctColor : r_incorrectColor);
		//imagenPanel.sprite = optionButton.Opcion.correct ? i_correct : i_incorrect;
		Image imagenBoton = optionButton.transform.Find("ImagenBoton")?.GetComponent<Image>();
		if (imagenBoton != null)
		{
			imagenBoton.sprite = optionButton.Opcion.correct ? i_correct : i_incorrect;
			imagenBoton.gameObject.SetActive(true);
		}
		panel.SetActive(true);
		
		acierto = optionButton.Opcion.correct;
		if(!acierto){
			for (int n=0; n<r_buttonList.Count ; n++)
			{
				r_waitTime = 4f;
				imagenBoton = r_buttonList[n].transform.Find("ImagenBoton")?.GetComponent<Image>();
				imagenBoton.sprite = r_buttonList[n].Opcion.correct ? i_correct : i_incorrect;
				imagenBoton.gameObject.SetActive(true);
			}
		}

		if (optionButton.Opcion.correct)
		{
			correctAnswers++;
			PlayerPrefs.SetInt(nombre+"CorrectAnswersQUIZ", correctAnswers);
		}
		else
		{
			incorrectAnswers++;
			PlayerPrefs.SetInt(nombre+"IncorrectAnswersQUIZ", incorrectAnswers);
		}
		PlayerPrefs.Save();

		r_audioSource.Play();
		Invoke("NextQuestion", r_waitTime);
		Invoke("QuitaImagen", r_waitTime);
	}
	
	void QuitaImagen()
	{
		if(!acierto){
			for (int n=0; n<r_buttonList.Count ; n++)
			{
				Image imagenBoton = r_buttonList[n].transform.Find("ImagenBoton")?.GetComponent<Image>();
				imagenBoton.gameObject.SetActive(false);
				r_waitTime = 2f;
			}
		}
		else
		{
			Image imagenBoton = opcionboton.transform.Find("ImagenBoton")?.GetComponent<Image>();
			imagenBoton.gameObject.SetActive(false);
		}
	}
	
	void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Animacion");
		PlayerPrefs.DeleteKey("Nivel");
		PlayerPrefs.Save();
    }
}
