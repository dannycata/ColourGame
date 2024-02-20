using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EntrarJuego : MonoBehaviour
{
    private Button boton;
    public string nombreDeLaNuevaEscena = "QuizGame";

    private void Start()
    {
        boton = GetComponent<Button>();
        if (boton != null)
        {
            boton.onClick.AddListener(OnClickCambiarEscena);
        }
    }

    private void OnClickCambiarEscena()
    {
		PlayerPrefs.DeleteKey("CorrectAnswersQUIZ");
		PlayerPrefs.DeleteKey("IncorrectAnswersQUIZ");
		PlayerPrefs.Save();
        SceneManager.LoadScene(nombreDeLaNuevaEscena);
    }
}


