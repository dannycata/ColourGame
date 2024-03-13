using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opciones : MonoBehaviour
{
	[SerializeField] GameObject panelOpciones;
	[SerializeField] private Button volver;
	private Button boton;
	
    void Start()
    {
        panelOpciones.SetActive(false);
		boton = GetComponent<Button>();
		boton.onClick.AddListener(click);
    }

    void click()
    {
        panelOpciones.SetActive(true);
		volver.onClick.AddListener(Volver);
    }
	
	void Volver()
    {
		panelOpciones.SetActive(false);
    }
}
