using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Salir : MonoBehaviour
{
    private Button boton;

    private void Start()
    {
        boton = GetComponent<Button>();
        if (boton != null)
        {
            boton.onClick.AddListener(OnClickSalir);
        }
    }

    private void OnClickSalir()
    {
        Application.Quit();
    }
}




