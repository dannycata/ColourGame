using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opciones : MonoBehaviour
{
	[SerializeField] GameObject panelOpciones;
	[SerializeField] private Button volver;
	private Button boton;
	private bool activo = false;
	
	[SerializeField] private AudioClip sound = null;
	private AudioSource audioSource = null;
	
    void Start()
    {
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
        panelOpciones.SetActive(false);
		boton = GetComponent<Button>();
		boton.onClick.AddListener(click);
		volver.onClick.AddListener(click);
    }

    void click()
    {
		Text textoBoton = boton.GetComponentInChildren<Text>();
		audioSource.PlayOneShot(sound);
		if (textoBoton != null) {
			Invoke("Volver",0.25f);
		} else {
			Invoke("options",0.25f);
		}
	}
	
	void options()
	{
		if (activo)
		{
			panelOpciones.SetActive(false);
			activo = false;
		}
		else
		{
			panelOpciones.SetActive(true);
			activo = true;
		}
		
    }
	
	void Volver()
    {
		panelOpciones.SetActive(false);
    }
}
