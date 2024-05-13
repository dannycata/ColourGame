using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerSimon : MonoBehaviour
{
    [SerializeField] BotonSimon[] button;
    [SerializeField] List<int> colorOrder;
    [SerializeField] int pickNumber = 0;
	[SerializeField] Text texto;
	[SerializeField] GameObject panelinvisible;
	[SerializeField] private string nombreDeLaNuevaEscena = "FinJuego";
	[SerializeField] private GameObject panelcomienzo = null;
	[SerializeField] private GameObject panel = null;
	[SerializeField] private GameObject menu = null;
	[SerializeField] private Sprite i_correct = null;
	[SerializeField] private Sprite i_incorrect = null;
	[SerializeField] private AudioClip s_incorrect = null;
	[SerializeField] private AudioClip s_correct = null;
	private Button botoncomienzo;
	private Button botonmenu;
	private Text cuentaAtras = null;
	private int nsecuencias;
	private float pickDelay;
	public Coroutine rutina;
	private string nombre;
	int score=0;
	public static bool acierto;
	private bool cuentaAtrasEjecutada = false;
	
	[SerializeField] private AudioClip sound = null;
	private AudioClip sonido = null;

    private AudioSource audioSource = null;
	

    void Start()
    {
		nombre = PlayerPrefs.GetString("Nombre", "");
		panelcomienzo.SetActive(true);
		panel.SetActive(false);
		menu.SetActive(false);
		PlayerPrefs.SetString("Juego", "Simon");
		pickDelay = PlayerPrefs.GetFloat(nombre+"VelocidadSimon", 1f);
		nsecuencias = PlayerPrefs.GetInt(nombre+"NSecuencias", 5);
		cuentaAtras = GameObject.Find("CuentaAtras").GetComponent<Text>();
		cuentaAtras.gameObject.SetActive(false);
		botoncomienzo = GameObject.Find("Empezar").GetComponent<Button>();
		botonmenu = GameObject.Find("BotonPausa").GetComponent<Button>();
		botonmenu.gameObject.SetActive(false);
		botoncomienzo.onClick.AddListener(CuentaAtras);
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
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
			audioSource.PlayOneShot(sound);
            cuentaAtras.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
		panelcomienzo.SetActive(false);
		botonmenu.gameObject.SetActive(true);
		ResetGame();
        SetButtonIndex();
        StartGame();
    }
	
	void Update()
	{
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !cuentaAtrasEjecutada)
			{
				CuentaAtras();
				cuentaAtrasEjecutada=true;
			}
		}
	}
	
	public void StartGame()
	{
		texto.text = "Memoriza";
		panelinvisible.SetActive(true);
		texto.color = Color.green;
		rutina = StartCoroutine("PlayGame");
	}

    void SetButtonIndex()
    {
        for(int cnt = 0; cnt < button.Length; cnt++)
            button[cnt].ButtonIndex = cnt;
    }

    IEnumerator PlayGame()
    {
        pickNumber = 0;
        yield return new WaitForSeconds(pickDelay);

        foreach(int colorIndex in colorOrder)
        {
            button[colorIndex].PressButton();
            yield return new WaitForSeconds(pickDelay);
        }

        PickRandomColor();
		Invoke("Panel", pickDelay);
    }
	
	void Panel()
	{
		texto.text = "Tu turno";
		texto.color = Color.black;
		panelinvisible.SetActive(false);
	}

    void PickRandomColor()
    {
        int rnd = Random.Range(0, button.Length);
        button[rnd].PressButton();
        colorOrder.Add(rnd);
    }

    public void PlayersPick(int pick, Image imagen)
    {
        if(pick == colorOrder[pickNumber])
        {
            Debug.Log("Acierto");
            pickNumber++;
            if(pickNumber == colorOrder.Count)
            {
				score++;
				imagen.sprite = i_correct;
				sonido = s_correct;
				audioSource.PlayOneShot(sonido);
				StartCoroutine(ShowImage(imagen));
				if (pickNumber == nsecuencias)
				{
					PlayerPrefs.SetInt(nombre+"CorrectAnswersSimon", score);
					Invoke("CambioEscena",1.5f);
				}
				Invoke("StartGame",1f);
            }
        }
        else
        {
            Debug.Log("Fallo");
			if (rutina != null)
			{
				StopCoroutine(rutina);
				rutina = null;
			}
			imagen.sprite = i_incorrect;
			sonido = s_incorrect;
			audioSource.PlayOneShot(sonido);
			StartCoroutine(ShowImage(imagen));
			PlayerPrefs.SetInt(nombre+"CorrectAnswersSimon", score);
			
			if(pickNumber == nsecuencias)
			{
				Invoke("CambioEscena",1.5f);
			}
			Invoke("CambioEscena",1.5f);
        }
    }
	
	private IEnumerator ShowImage(Image imagen)
	{
		imagen.gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		imagen.gameObject.SetActive(false); 
	}
	
	void CambioEscena()
	{
		SceneManager.LoadScene(nombreDeLaNuevaEscena);
	}

    void ResetGame()
    {
        colorOrder.Clear();
    }
	
	void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("Animacion");
		PlayerPrefs.DeleteKey("Nivel");
		PlayerPrefs.Save();
    }
}
