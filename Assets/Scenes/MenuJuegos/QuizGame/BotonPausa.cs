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
	
	[SerializeField] private AudioClip sound = null;
	private AudioSource audioSource = null;
	
    void Start()
    {
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
		menu.SetActive(false);
		menuopciones.SetActive(false);
		botonmenu = GetComponent<Button>();
        botonmenu.onClick.AddListener(Menu);
		volver.onClick.AddListener(Volver);
		continuar.onClick.AddListener(Continuar);
		opciones.onClick.AddListener(Opciones);
		salir.onClick.AddListener(Salir);
    }

    void Menu()
    {
		audioSource.PlayOneShot(sound);
		botonmenu.gameObject.SetActive(false);
        menu.SetActive(true);
		Timer.actualizar = false;
		Timer2.actualizar = false;
    }
	
	void Continuar()
    {
		audioSource.PlayOneShot(sound);
		botonmenu.gameObject.SetActive(true);
        menu.SetActive(false);
		Timer.actualizar = true;
		Timer2.actualizar = true;
    }
	
	void Opciones()
    {
		audioSource.PlayOneShot(sound);
		menuopciones.SetActive(true);
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
