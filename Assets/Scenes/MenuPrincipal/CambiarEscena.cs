using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    private Button boton;
    public Color nuevoColor = Color.white;
    public string nombreDeLaNuevaEscena = "QuizGame";

    private void Start()
    {
        boton = GetComponent<Button>();

        if (boton != null)
        {
            boton.onClick.AddListener(OnClickCambiarColorYEscena);
        }
    }

    private void OnClickCambiarColorYEscena()
    {
        boton.image.color = nuevoColor;
        StartCoroutine(CambiarEscenaConDelay());
    }

    private IEnumerator CambiarEscenaConDelay()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(nombreDeLaNuevaEscena);
    }
}




