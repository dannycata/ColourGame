using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Nombre : MonoBehaviour
{
	[SerializeField] private AudioClip sound = null;
	[SerializeField] private InputField inputnombre;
	[SerializeField] private Button entrar;
	[SerializeField] private Button error;
	private GameObject panel = null;
	private Image panelBrillo;
	private float value;
	private string nombre=null;
	private AudioSource audioSource = null;
	
    void Start()
    {
		panel = GameObject.Find("PanelBrillo");
		error.gameObject.SetActive(false);
		panelBrillo = panel.GetComponent<Image>();
		value = PlayerPrefs.GetFloat("Brillo",0.9f);
		float resta = 0.9f - value;
		panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, resta);
		value = PlayerPrefs.GetFloat("Volumen",0.5f);
		AudioListener.volume = value;
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
		entrar.onClick.AddListener(OnClickEntrar);
		//inputnombre.placeholder.GetComponent<Text>().text = PlayerPrefs.GetString("Nombre","").ToString();
		inputnombre.onEndEdit.AddListener(Leer);
		error.onClick.AddListener(quitarerror);
    }
	
	void quitarerror()
	{
		error.gameObject.SetActive(false);
		inputnombre.ActivateInputField();
	}
	
	void Leer(string valorEntrada)
	{
		Text errortexto = error.GetComponentInChildren<Text>();
		errortexto.text = "Ingresa un nombre";
		if (!string.IsNullOrEmpty(valorEntrada))
		{
			nombre = valorEntrada;
			string[] listanombres = PlayerPrefs.GetString("NombresJugadores", "").Split(',');
			PlayerPrefs.SetString("Nombre", nombre);
			if (listanombres.Any(n => n.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
			{
				errortexto.text = "El nombre ya esta en uso";
				inputnombre.text = "";
				var placeholderText = inputnombre.placeholder as Text;
                placeholderText.text = "";
				error.gameObject.SetActive(true);
			}
			else
			{
				string nombres = PlayerPrefs.GetString("NombresJugadores", "") + "," + nombre;
				PlayerPrefs.SetString("NombresJugadores", nombres);
				SceneManager.LoadScene("MenuPrincipal");
			}
		}
		else error.gameObject.SetActive(true);
	}
	
	private void LimpiarPreferencias()
    {
        PlayerPrefs.DeleteKey("Animacion");
        PlayerPrefs.Save();
    }
	
    void OnApplicationQuit()
    {
        LimpiarPreferencias();
    }
	
	void OnClickEntrar()
	{
		audioSource.PlayOneShot(sound);
		if(nombre!=null){
			Invoke("CambioEscena",0.25f);
		}
		else error.gameObject.SetActive(true);
	}
	
	void CambioEscena()
	{
		SceneManager.LoadScene("MenuPrincipal");
	}
}
