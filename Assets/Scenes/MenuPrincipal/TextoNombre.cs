using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoNombre : MonoBehaviour
{
	
    void Start()
    {
		Text texto = GetComponent<Text>();
		texto.text = "Jugador: " + PlayerPrefs.GetString("Nombre","");
    }
}