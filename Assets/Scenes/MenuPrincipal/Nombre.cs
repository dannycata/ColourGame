using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Nombre : MonoBehaviour
{
	[SerializeField] private AudioClip sound = null;
	[SerializeField] private InputField inputnombre;
	[SerializeField] private Button boton;
	private GameObject panel = null;
	private Image panelBrillo;
	private float value;
	private string nombre;
	private AudioSource audioSource = null;
	
    void Start()
    {
		panel = GameObject.Find("PanelBrillo");
		panelBrillo = panel.GetComponent<Image>();
		value = PlayerPrefs.GetFloat("Brillo",0.9f);
		float resta = 0.9f - value;
		panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, resta);
		value = PlayerPrefs.GetFloat("Volumen",0.5f);
		AudioListener.volume = value;
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
        if (boton != null)
        {
            boton.onClick.AddListener(OnClickBoton);
        }
		inputnombre.placeholder.GetComponent<Text>().text = PlayerPrefs.GetString("Nombre","").ToString();
		inputnombre.onEndEdit.AddListener(Leer);
    }
	
	private void OnClickBoton()
    {
		audioSource.PlayOneShot(sound);
		Invoke("Salir",0.25f);
	}
	
	private void Salir()
	{
		LimpiarPreferencias();
		Application.Quit();
	}
	
	void Leer(string valorEntrada)
	{
		if (!string.IsNullOrEmpty(valorEntrada))
		{
			nombre = valorEntrada;
			PlayerPrefs.SetString("Nombre", nombre);
			SceneManager.LoadScene("MenuPrincipal");
		}
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
}
