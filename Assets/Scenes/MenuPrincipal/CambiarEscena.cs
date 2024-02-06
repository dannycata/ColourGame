using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    private Button boton;  // Referencia al componente Button
    public Color nuevoColor = Color.white;  // Color que se asignará al botón
    public string nombreDeLaNuevaEscena = "Juego1";  // Nombre de la nueva escena

    private void Start()
    {
        // Obtener la referencia al componente Button del objeto actual
        boton = GetComponent<Button>();

        // Verificar si el componente Button existe
        if (boton != null)
        {
            // Agregar un listener al evento de clic del botón
            boton.onClick.AddListener(OnClickCambiarColorYEscena);
        }
        else
        {
            Debug.LogError("El objeto actual no tiene un componente Button adjunto.");
        }
    }

    // Método que se llama cuando se hace clic en el botón
    private void OnClickCambiarColorYEscena()
    {
        // Cambiar el color del botón al color especificado
        boton.image.color = nuevoColor;

        Debug.Log("Color del botón cambiado a: " + nuevoColor);

        // Iniciar la corrutina para el cambio de escena con un pequeño delay
        StartCoroutine(CambiarEscenaConDelay());
    }

    // Corrutina para cambiar de escena con un pequeño delay
    private IEnumerator CambiarEscenaConDelay()
    {
        // Esperar 1 segundo (puedes ajustar el valor según sea necesario)
        yield return new WaitForSeconds(1.0f);

        // Cambiar a la nueva escena
        SceneManager.LoadScene(nombreDeLaNuevaEscena);
    }
}




