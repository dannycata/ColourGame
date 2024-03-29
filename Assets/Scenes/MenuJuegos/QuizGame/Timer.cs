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
	[SerializeField] private GameObject panelcomienzo = null;
	[SerializeField] private GameObject panel = null;
	[SerializeField] private GameObject menu = null;
	private Button botoncomienzo;
	private Button botonmenu;
	private Text cuentaAtras = null;
	public static bool actualizar = false;
	private Button boton;
	private bool respuestaEncontrada = false;
	
    public void Starts()
    {
		panelcomienzo.SetActive(true);
		panel.SetActive(false);
		menu.SetActive(false);
		maxTime = PlayerPrefs.GetFloat("VariableTiempo", 5f);
		Camera mainCamera = Camera.main;
		gameManager = mainCamera.GetComponent<GameManager>();
		timerBar = GetComponent<Image> ();
		ResetTimer();
		actualizar=false;
		cuentaAtras = GameObject.Find("CuentaAtras").GetComponent<Text>();
		cuentaAtras.gameObject.SetActive(false);
		botoncomienzo = GameObject.Find("Empezar").GetComponent<Button>();
		botonmenu = GameObject.Find("BotonPausa").GetComponent<Button>();
		botonmenu.gameObject.SetActive(false);
		botoncomienzo.onClick.AddListener(CuentaAtras);
	}
	
	public void CuentaAtras()
	{
		botoncomienzo.gameObject.SetActive(false);
		cuentaAtras.gameObject.SetActive(true);
		StartCoroutine(CuentaAtrasCoroutine());
	}
	
	IEnumerator CuentaAtrasCoroutine()
    {
        int numeroInicial = 3;
        for (int i = numeroInicial; i > 0; i--)
        {
            cuentaAtras.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
		panelcomienzo.SetActive(false);
		botonmenu.gameObject.SetActive(true);
		actualizar = true;
		gameManager.NextQuestion();
		Update();
    }

    void Update()
    {
		if(actualizar){
			if (timeLeft > 0){
				timeLeft -= Time.deltaTime;
				timerBar.fillAmount = timeLeft / maxTime;
			} else {
				//Time.timeScale = 0;
				RespuestaIncorrecta();
			}
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
