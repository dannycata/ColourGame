using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BotonesDimension : MonoBehaviour
{
    [SerializeField] private AudioClip sound = null;
    string hexColor = "#A4FC92";
    private Color colorSeleccionado;
    private Color colorNormal = Color.white;
    private Button ultimoBotonSeleccionado = null;
    private AudioSource audioSource = null;
	private string nombre;
	private InputField inputFieldVelocidad;
	private Text r_warning;

    private void Start()
    {
		nombre = PlayerPrefs.GetString("Nombre", "");
        colorSeleccionado = ColorUtility.TryParseHtmlString(hexColor, out Color color) ? color : Color.white;
		inputFieldVelocidad = GameObject.Find("InputFieldVelocidad").GetComponent<InputField>();
		r_warning = GameObject.Find("r_warning").GetComponent<Text>();
        Camera mainCamera = Camera.main;
        audioSource = mainCamera.GetComponent<AudioSource>();
        ActualizarColores();
    }

    private void ActualizarColores()
    {
        string dimensiones = PlayerPrefs.GetString(nombre+"Dimensiones", "4x2");
        Button[] botones = FindObjectsOfType<Button>();

        foreach (Button boton in botones)
        {
            Text textoBoton = boton.GetComponentInChildren<Text>();
            BotonesDimension scriptBoton = boton.GetComponent<BotonesDimension>();

            if (scriptBoton != null)
            {
                if (textoBoton.text == dimensiones)
                {
                    boton.image.color = colorSeleccionado;
                    ultimoBotonSeleccionado = boton;
                }
                else
                {
                    boton.image.color = colorNormal;
                }

                boton.onClick.RemoveAllListeners();
                boton.onClick.AddListener(() => CambiarColorBoton(boton, textoBoton.text));
            }
        }
    }

    private void CambiarColorBoton(Button botonActual, string textoBoton)
    {
        audioSource.PlayOneShot(sound);
        botonActual.image.color = colorSeleccionado;
        PlayerPrefs.SetString(nombre+"Dimensiones", textoBoton);
		if (textoBoton == "4x2")
		{
			r_warning.text = "Recomendado 40 o mas";
			PlayerPrefs.SetFloat(nombre+"VelocidadPair", 40f);
			inputFieldVelocidad.placeholder.GetComponent<Text>().text = "40";
			
		}
		else if (textoBoton == "6x2")
		{
			r_warning.text = "Recomendado 60 o mas";
			PlayerPrefs.SetFloat(nombre+"VelocidadPair", 60f);
			inputFieldVelocidad.placeholder.GetComponent<Text>().text = "60";
			
		}
		else if (textoBoton == "6x3")
		{
			r_warning.text = "Recomendado 90 o mas";
			PlayerPrefs.SetFloat(nombre+"VelocidadPair", 90f);
			inputFieldVelocidad.placeholder.GetComponent<Text>().text = "90";
			
		}

        if (ultimoBotonSeleccionado != null && ultimoBotonSeleccionado != botonActual)
        {
            ultimoBotonSeleccionado.image.color = colorNormal;
        }

        ultimoBotonSeleccionado = botonActual;
    }
}
