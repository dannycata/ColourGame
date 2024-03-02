using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonesNivel : MonoBehaviour
{
	[SerializeField] private AudioClip sound = null;
    private string colorHexadecimal = "#7FE86E";
    private Color colorSeleccionado;
    private Color colorNormal = Color.white;
    private Button boton;
    private string texto;
	
	private AudioSource audioSource = null;

    private void Start()
    {
        ColorUtility.TryParseHtmlString(colorHexadecimal, out colorSeleccionado);
		
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
        boton = GetComponent<Button>();
        Text textoBoton = boton.GetComponentInChildren<Text>();
        texto = textoBoton.text;

        boton.onClick.AddListener(EstablecerNivel);
        ActualizarColores();
    }

    private void EstablecerNivel()
    {
		audioSource.PlayOneShot(sound);
        PlayerPrefs.DeleteKey("Nivel");
        PlayerPrefs.SetString("Nivel", texto);
        ActualizarColores();
    }

    private void ActualizarColores()
    {
        string opcion = PlayerPrefs.GetString("Nivel","Nivel Facil");

        // Si es el botón seleccionado, cambia su color
        boton.image.color = texto == opcion ? colorSeleccionado : colorNormal;

        // Obtenemos todos los botones en el mismo objeto contenedor
        BotonesNivel[] todosLosBotones = transform.parent.GetComponentsInChildren<BotonesNivel>(true);

        foreach (var otroBoton in todosLosBotones)
        {
            if (otroBoton != this && otroBoton.boton != null)
            {
                // Cambia el color según si coincide con la opción seleccionada o no
                otroBoton.boton.image.color = otroBoton.texto == opcion ? otroBoton.colorSeleccionado : otroBoton.colorNormal;
            }
        }
    }
}