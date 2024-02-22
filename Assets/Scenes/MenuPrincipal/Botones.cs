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
			StartCoroutine(OnClickSalir(0.25f));
		}
		else StartCoroutine(CambiarEscena(0.1f));
    }
	
	IEnumerator CambiarEscena(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nombreDeLaNuevaEscena);
    }
	
	IEnumerator OnClickSalir(float delay)
    {
		yield return new WaitForSeconds(delay);
        Application.Quit();
    }
	
	void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Animacion");
		PlayerPrefs.DeleteKey("Nivel");
		PlayerPrefs.DeleteKey("VariableTiempo");
		PlayerPrefs.DeleteKey("NPreguntas");
		PlayerPrefs.Save();
    }
}




