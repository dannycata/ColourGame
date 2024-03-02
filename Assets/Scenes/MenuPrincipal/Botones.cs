using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Botones : MonoBehaviour
{
	[SerializeField] private AudioClip sound = null;
    private Button boton;
	private string texto = null;
    public string nombreDeLaNuevaEscena = "QuizGame";
	string nombreEscenaActual = null;
	
	private AudioSource audioSource = null;

    private void Start()
    {
        boton = GetComponent<Button>();
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
        if (boton != null)
        {
			nombreEscenaActual=SceneManager.GetActiveScene().name;
            boton.onClick.AddListener(OnClickBoton);
        }
    }

    private void OnClickBoton()
    {
		Text textoBoton = boton.GetComponentInChildren<Text>();
        texto = textoBoton.text;
		audioSource.PlayOneShot(sound);
		if (texto == "Salir"){
			Invoke("Salir",0.25f);
		}
		else Invoke("Escena",0.15f);
    }
	
	private void Salir()
	{
		Application.Quit();
	}
	
	private void Escena()
	{
		SceneManager.LoadScene(nombreDeLaNuevaEscena);
	}
	
	void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Animacion");
		PlayerPrefs.DeleteKey("Nivel");
		PlayerPrefs.DeleteKey("VariableTiempo");
		PlayerPrefs.DeleteKey("NPreguntas");
		PlayerPrefs.DeleteKey("VelocidadSimon");
		PlayerPrefs.DeleteKey("NSecuencias");
		PlayerPrefs.Save();
    }
}




