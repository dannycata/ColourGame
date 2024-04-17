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

    private void Start()
    {
		nombre = PlayerPrefs.GetString("Nombre", "");
        colorSeleccionado = ColorUtility.TryParseHtmlString(hexColor, out Color color) ? color : Color.white;
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

        if (ultimoBotonSeleccionado != null && ultimoBotonSeleccionado != botonActual)
        {
            ultimoBotonSeleccionado.image.color = colorNormal;
        }

        ultimoBotonSeleccionado = botonActual;
    }
}
