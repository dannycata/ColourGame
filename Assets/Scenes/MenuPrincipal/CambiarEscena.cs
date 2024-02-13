using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    private Button boton;
    public string nombreDeLaNuevaEscena = "QuizGame";
	string nombreEscenaActual = null;

    private void Start()
    {
        boton = GetComponent<Button>();
		nombreEscenaActual=SceneManager.GetActiveScene().name;
        if (boton != null)
        {
			nombreEscenaActual=SceneManager.GetActiveScene().name;
            boton.onClick.AddListener(OnClickCambiarEscena);
        }
		
		if (nombreEscenaActual == "MenuPrincipal")
		{
			PlayerPrefs.DeleteKey("VariableTiempo");
		}
    }

    private void OnClickCambiarEscena()
    {
        SceneManager.LoadScene(nombreDeLaNuevaEscena);
    }
}




