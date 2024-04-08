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
	private GameObject panel = null;
	private Image panelBrillo;
	private float value;
	
	private AudioSource audioSource = null;

    private void Start()
    {
		panel = GameObject.Find("PanelBrillo");
		panelBrillo = panel.GetComponent<Image>();
		value = PlayerPrefs.GetFloat("Brillo",0f);
		float resta = 0.9f - value;
		panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, resta);
		value = PlayerPrefs.GetFloat("Volumen",0.5f);
		AudioListener.volume = value;
        boton = GetComponent<Button>();
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
        if (boton != null)
        {
            boton.onClick.AddListener(OnClickBoton);
        }
    }

    private void OnClickBoton()
    {
		Text textoBoton = boton.GetComponentInChildren<Text>();
        texto = textoBoton.text;
		audioSource.PlayOneShot(sound);
		nombreEscenaActual = SceneManager.GetActiveScene().name;
		if (texto == "Salir" && nombreEscenaActual == "MenuPrincipal"){
			Invoke("Salir",0.25f);
		}
		else if (texto == "Editar"){
			Invoke("Editar",0.25f);
		}
		else if (texto == "Volver a jugar"){
			Invoke("VolverJugar",0.25f);
		}
		else Invoke("Escena",0.25f);
    }
	
	private void Salir()
	{
		LimpiarPreferencias();
		Application.Quit();
	}
	
	private void Editar()
	{
		SceneManager.LoadScene("MenuEditor");
	}
	
	private void VolverJugar()
	{
		if (NAciertos.juego == "Quiz")
		{
			SceneManager.LoadScene("QuizGame");
		}
		else if (NAciertos.juego == "Simon")
		{
			SceneManager.LoadScene("SimonGame");
		}
		else if (NAciertos.juego == "Pair")
		{
			
			SceneManager.LoadScene("Pairgame"+PlayerPrefs.GetString("Dimensiones", "4x2"));
		}
	}
	
	private void Escena()
	{
		SceneManager.LoadScene(nombreDeLaNuevaEscena);
	}
	
	private void LimpiarPreferencias()
    {
        PlayerPrefs.DeleteKey("Animacion");
        PlayerPrefs.DeleteKey("Nivel");
        PlayerPrefs.DeleteKey("VariableTiempo");
        PlayerPrefs.DeleteKey("NPreguntas");
        PlayerPrefs.DeleteKey("VelocidadSimon");
        PlayerPrefs.DeleteKey("NSecuencias");
        PlayerPrefs.DeleteKey("Colores");
        PlayerPrefs.Save();
    }
	
    void OnApplicationQuit()
    {
        LimpiarPreferencias();
    }
}




