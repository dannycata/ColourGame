using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Timer : MonoBehaviour
{
	private Image timerBar;
	public float maxTime = 5f;
	public float timeLeft;
	GameManager gameManager;
	[SerializeField] private List<OpcionBoton> r_buttonList = null;
	private Button boton;
	private bool respuestaEncontrada = false;
	
    public void Starts()
    {
		maxTime = PlayerPrefs.GetFloat("VariableTiempo", 5f);
		Camera mainCamera = Camera.main;
		gameManager = mainCamera.GetComponent<GameManager>();
		timerBar = GetComponent<Image> ();
		ResetTimer();
		gameManager.NextQuestion();
		Update();
    }

    void Update()
    {
        if (timeLeft > 0){
			timeLeft -= Time.deltaTime;
			timerBar.fillAmount = timeLeft / maxTime;
		} else {
			//Time.timeScale = 0;
			RespuestaIncorrecta();
        }
    }

    public void ResetTimer()
    {
		enabled=true;
        timeLeft = maxTime;
        timerBar.fillAmount = 1f;
        Time.timeScale = 1;
		respuestaEncontrada = false;
    }
	
	public void Stop()
    {
        enabled=false;
    }
	
	public void RespuestaIncorrecta()
	{
		for (int n=0; n<r_buttonList.Count ; n++)
		{
			if (!respuestaEncontrada && gameManager.question.opciones[n].correct == false)
			{
				Button boton = r_buttonList[n].GetComponent<Button>();
				boton.onClick.Invoke();
				respuestaEncontrada = true;
			}
		}
	}
}
