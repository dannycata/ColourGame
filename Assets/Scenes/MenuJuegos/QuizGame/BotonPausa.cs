using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BotonPausa : MonoBehaviour
{
    [SerializeField] private GameObject menu = null;
	[SerializeField] private GameObject menuopciones = null;
	[SerializeField] private Button continuar;
	[SerializeField] private Button opciones;
	[SerializeField] private Button volver;
	[SerializeField] private Button salir;
	[SerializeField] private string nombreDeLaNuevaEscena = "MenuJuegos";
	private Button botonmenu;
	private Timer timer = null;
	
	[SerializeField] private AudioClip sound = null;
	private AudioSource audioSource = null;
	
    void Start()
    {
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
		menu.SetActive(false);
		menuopciones.SetActive(false);
		botonmenu = GetComponent<Button>();
		timer = GameObject.FindObjectOfType<Timer>();
        botonmenu.onClick.AddListener(Menu);
    }

    void Menu()
    {
		audioSource.PlayOneShot(sound);
		botonmenu.gameObject.SetActive(false);
        menu.SetActive(true);
		Timer.actualizar = false;
		continuar.onClick.AddListener(Continuar);
		opciones.onClick.AddListener(Opciones);
		salir.onClick.AddListener(Salir);
    }
	
	void Continuar()
    {
		audioSource.PlayOneShot(sound);
		botonmenu.gameObject.SetActive(true);
        menu.SetActive(false);
		Timer.actualizar = true;
    }
	
	void Opciones()
    {
		audioSource.PlayOneShot(sound);
		menuopciones.SetActive(true);
		volver.onClick.AddListener(Volver);
    }
	
	void Volver()
    {
		audioSource.PlayOneShot(sound);
		menuopciones.SetActive(false);
    }
	
	void Salir()
    {
		audioSource.PlayOneShot(sound);
		Invoke("quit",0.25f);
    }
	
	void quit()
	{
		SceneManager.LoadScene(nombreDeLaNuevaEscena);
	}
}
