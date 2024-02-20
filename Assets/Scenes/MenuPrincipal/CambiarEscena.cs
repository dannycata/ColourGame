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
    }

    private void OnClickCambiarEscena()
    {
        SceneManager.LoadScene(nombreDeLaNuevaEscena);
    }
	
	void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Animacion");
		PlayerPrefs.DeleteKey("Nivel");
		PlayerPrefs.DeleteKey("VariableTiempo");
		PlayerPrefs.Save();
    }
}




