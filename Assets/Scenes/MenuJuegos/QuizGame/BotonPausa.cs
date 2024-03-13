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
	
    void Start()
    {
		menu.SetActive(false);
		menuopciones.SetActive(false);
		botonmenu = GetComponent<Button>();
		timer = GameObject.FindObjectOfType<Timer>();
        botonmenu.onClick.AddListener(Menu);
    }

    void Menu()
    {
		botonmenu.gameObject.SetActive(false);
        menu.SetActive(true);
		Timer.actualizar = false;
		continuar.onClick.AddListener(Continuar);
		opciones.onClick.AddListener(Opciones);
		salir.onClick.AddListener(Salir);
    }
	
	void Continuar()
    {
		botonmenu.gameObject.SetActive(true);
        menu.SetActive(false);
		Timer.actualizar = true;
    }
	
	void Opciones()
    {
		menuopciones.SetActive(true);
		volver.onClick.AddListener(Volver);
    }
	
	void Volver()
    {
		menuopciones.SetActive(false);
    }
	
	void Salir()
    {
		SceneManager.LoadScene(nombreDeLaNuevaEscena);
    }
}
