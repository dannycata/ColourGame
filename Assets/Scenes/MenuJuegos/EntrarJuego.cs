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
		PlayerPrefs.DeleteKey("CorrectAnswersQUIZ");
		PlayerPrefs.DeleteKey("IncorrectAnswersQUIZ");
		PlayerPrefs.Save();
		audioSource.PlayOneShot(sound);
		Invoke("Escena",0.15f);
    }
	
	private void Escena()
	{
		SceneManager.LoadScene(nombreDeLaNuevaEscena);
	}
}


