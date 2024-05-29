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
	int invisible = 0;
	string nombre = null;
	
	[SerializeField] private AudioClip sound = null;
	private AudioSource audioSource = null;

	
    public void Starts()
    {
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
		panelcomienzo.SetActive(true);
		panel.SetActive(false);
		menu.SetActive(false);
		nombre = PlayerPrefs.GetString("Nombre", "");
		invisible = PlayerPrefs.GetInt(nombre+"TiempoInvisible", 0);
		maxTime = PlayerPrefs.GetFloat(nombre+"VariableTiempo", 5f);
		gameManager = mainCamera.GetComponent<GameManager>();
		timerBar = GetComponent<Image> ();
		actualizar=false;
		cuentaAtras = GameObject.Find("CuentaAtras").GetComponent<Text>();
		cuentaAtras.gameObject.SetActive(false);
		botoncomienzo = GameObject.Find("Empezar").GetComponent<Button>();
		botonmenu = GameObject.Find("BotonPausa").GetComponent<Button>();
		botonmenu.gameObject.SetActive(false);
		botoncomienzo.onClick.AddListener(CuentaAtras);
		Color alpha = timerBar.color;
		alpha.a = 255f;
		timerBar.color = alpha;
		if (invisible == 1)
		{
			alpha.a = 0f;
			timerBar.color = alpha;
		}
		if (maxTime == 0)
		{
			maxTime=1800f;
			alpha.a = 0f;
			timerBar.color = alpha;
		}
		ResetTimer();
	}
	
	public void OnPanelClicked()
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			CuentaAtras();
        }
    }
	
	public void CuentaAtras()
	{
		audioSource.PlayOneShot(sound);
		Button botonvolver = GameObject.Find("VolverMenu").GetComponent<Button>();
		botonvolver.gameObject.SetActive(false);
		botoncomienzo.gameObject.SetActive(false);
		cuentaAtras.gameObject.SetActive(true);
		StartCoroutine(CuentaAtrasCoroutine());
	}
	
	IEnumerator CuentaAtrasCoroutine()
    {
        int numeroInicial = 3;
        for (int i = numeroInicial; i > 0; i--)
        {
			audioSource.PlayOneShot(sound);
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
