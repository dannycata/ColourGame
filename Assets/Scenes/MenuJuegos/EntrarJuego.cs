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
	
	IEnumerator CambiarEscena(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nombreDeLaNuevaEscena);
    }
	
    private void OnClickPlay()
    {
		PlayerPrefs.DeleteKey("CorrectAnswersQUIZ");
		PlayerPrefs.DeleteKey("IncorrectAnswersQUIZ");
		PlayerPrefs.Save();
		audioSource.PlayOneShot(sound);
		StartCoroutine(CambiarEscena(0.1f));
    }
}


