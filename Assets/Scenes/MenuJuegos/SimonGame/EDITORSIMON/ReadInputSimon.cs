using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadInputSimon : MonoBehaviour
{
	[SerializeField] private AudioClip sound = null;
	[SerializeField] private GameObject paneleliminartodo= null;
	[SerializeField] private GameObject panelresetear= null;
	[SerializeField] private Button borrarcancelar;
	[SerializeField] private Button resetear;
    private float velocidad;
	private int secuencias;
	private float destellos;
    private InputField inputFieldVelocidad;
	private InputField inputFieldSecuencia;
	private InputField inputFieldDestello;
    private Text v_warning;
	private Text s_warning;
	private Text d_warning;
	private string nombre;
	[SerializeField] private Color rojo;
	[SerializeField] private Color gris;
	
	private AudioSource audioSource = null;
	
	public static float velocidadsimon;
	public static int nsecuencias;
	public static float tiempodestello;
	public static string verdesimon;
	public static string rojosimon;
	public static string amarillosimon;
	public static string azulsimon;

    void Start()
    {
		paneleliminartodo.SetActive(false);
		panelresetear.SetActive(false);
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
		nombre = PlayerPrefs.GetString("Nombre", "");
        inputFieldVelocidad = GameObject.Find("InputFieldVelocidad").GetComponent<InputField>();
		inputFieldSecuencia = GameObject.Find("InputFieldSecuencia").GetComponent<InputField>();
		inputFieldDestello = GameObject.Find("InputFieldDestello").GetComponent<InputField>();
		inputFieldVelocidad.placeholder.GetComponent<Text>().text = PlayerPrefs.GetFloat(nombre+"VelocidadSimon", 1f).ToString();
		inputFieldSecuencia.placeholder.GetComponent<Text>().text = PlayerPrefs.GetInt(nombre+"NSecuencias", 5).ToString();
		inputFieldDestello.placeholder.GetComponent<Text>().text = PlayerPrefs.GetFloat(nombre+"TiempoDestello", 0.25f).ToString();
        v_warning = GameObject.Find("v_warning").GetComponent<Text>();
		s_warning = GameObject.Find("s_warning").GetComponent<Text>();
		d_warning = GameObject.Find("d_warning").GetComponent<Text>();
        v_warning.enabled = false;
		s_warning.enabled = false;
		d_warning.enabled = false;

        inputFieldVelocidad.onEndEdit.AddListener(Read);
		inputFieldSecuencia.onEndEdit.AddListener(Read2);
		inputFieldDestello.onEndEdit.AddListener(Read3);
		borrarcancelar.onClick.AddListener(OnClickCancelar);
		resetear.onClick.AddListener(OnClickResetear);
		GuardarPrefabs();
    }

    void Read(string inputValue)
    {
        if (!string.IsNullOrEmpty(inputValue))
		{
			if (float.TryParse(inputValue, out velocidad))
			{
				v_warning.text = "Error: Tiempo debe ser mayor a 0";
				v_warning.color = rojo;
				v_warning.enabled = false;
				if (velocidad > 0) {
					PlayerPrefs.SetFloat(nombre+"VelocidadSimon", velocidad);
				} else v_warning.enabled = true;
			}
			else
			{
				v_warning.text = "Error: Inserta un numero";
				v_warning.color = rojo;
				v_warning.enabled = true;
			}
		}
    }
	
	void Read2(string inputValue)
    {
		if (!string.IsNullOrEmpty(inputValue))
		{
			if (int.TryParse(inputValue, out int result))
			{
				secuencias = result;
				s_warning.text = "Error: Inserta un numero mayor";
				s_warning.color = rojo;
				s_warning.enabled = false;
				if (secuencias >= 1) {
					PlayerPrefs.SetInt(nombre+"NSecuencias", secuencias);
				} else s_warning.enabled = true;
			}
			else
			{
				s_warning.text = "Error: Inserta un numero";
				s_warning.color = rojo;
				s_warning.enabled = true;
			}
		}
    }
	
	void Read3(string inputValue)
    {
		if (!string.IsNullOrEmpty(inputValue))
		{
			if (float.TryParse(inputValue, out destellos))
			{
				d_warning.enabled = false;
				d_warning.color = rojo;
				d_warning.text = "Error: Tiempo debe ser mayor a 0";
				d_warning.enabled = false;
				if (destellos > 0) {
					PlayerPrefs.SetFloat(nombre+"TiempoDestello", destellos);
				} else d_warning.enabled = true;
			}
			else
			{
				d_warning.text = "Error: Inserta un numero";
				d_warning.color = rojo;
				d_warning.enabled = true;
			}
		}
    }
	
	public void GuardarPrefabs()
	{
		velocidadsimon = PlayerPrefs.GetFloat(nombre+"VelocidadSimon", 1f);
		nsecuencias = PlayerPrefs.GetInt(nombre+"NSecuencias", 5);
		tiempodestello = PlayerPrefs.GetFloat(nombre+"TiempoDestello", 0.25f);
		verdesimon = PlayerPrefs.GetString(nombre+"VerdeSimon", "#00FF00FF");
		rojosimon = PlayerPrefs.GetString(nombre+"RojoSimon", "#FF0200FF");
		amarillosimon = PlayerPrefs.GetString(nombre+"AmarilloSimon", "#0000FFFF");
		azulsimon = PlayerPrefs.GetString(nombre+"AzulSimon", "#FDFF00FF");
	}
	
	public void AsignarPrefabs(float velocsimon, int nsec, float tdest, string vsimon, string rsimon, string amsimon, string azsimon)
	{
		PlayerPrefs.SetFloat(nombre+"VelocidadSimon", velocsimon);
		PlayerPrefs.SetInt(nombre+"NSecuencias", nsec);
		PlayerPrefs.SetFloat(nombre+"TiempoDestello", tdest);
		PlayerPrefs.SetString(nombre+"VerdeSimon", vsimon);
		PlayerPrefs.SetString(nombre+"RojoSimon", rsimon);
		PlayerPrefs.SetString(nombre+"AmarilloSimon", amsimon);
		PlayerPrefs.SetString(nombre+"AzulSimon", azsimon);
	}
	
	private void OnClickCancelar()
    {
		audioSource.PlayOneShot(sound);
		paneleliminartodo.SetActive(true);
		Button[] botones = paneleliminartodo.GetComponentsInChildren<Button>();
        foreach (Button boton in botones)
        {
            if (boton.name == "Si")
            {
				boton.onClick.RemoveListener(OnClickSi);
				boton.onClick.AddListener(OnClickSi);
            }
            else if (boton.name == "No")
            {
				boton.onClick.RemoveListener(OnClickNo);
                boton.onClick.AddListener(OnClickNo);
            }
        }
	}
	
	void OnClickSi()
	{
		audioSource.PlayOneShot(sound);
        Invoke("Borrar",0.25f);
	}
	
	void OnClickNo()
	{
		audioSource.PlayOneShot(sound);
		paneleliminartodo.SetActive(false);
	}
	
	private void Borrar()
	{
		AsignarPrefabs(velocidadsimon,nsecuencias,tiempodestello,verdesimon,rojosimon,amarillosimon,azulsimon);
		SceneManager.LoadScene("MenuEditor");
	}
	
	private void OnClickResetear()
    {
		audioSource.PlayOneShot(sound);
		panelresetear.SetActive(true);
		Button[] botones = panelresetear.GetComponentsInChildren<Button>();
        foreach (Button boton in botones)
        {
            if (boton.name == "Si")
            {
				boton.onClick.RemoveListener(OnClickSiReset);
				boton.onClick.AddListener(OnClickSiReset);
            }
            else if (boton.name == "No")
            {
				boton.onClick.RemoveListener(OnClickNoReset);
                boton.onClick.AddListener(OnClickNoReset);
            }
        }
	}
	
	void OnClickSiReset()
	{
		audioSource.PlayOneShot(sound);
        Invoke("Reset",0.25f);
	}
	
	void OnClickNoReset()
	{
		audioSource.PlayOneShot(sound);
		panelresetear.SetActive(false);
	}
	
	private void Reset()
	{
		AsignarPrefabs(1f,5,0.25f,"#00FF00FF","#FF0200FF","#0000FFFF","#FDFF00FF");
		SceneManager.LoadScene("MenuEditor");
	}
}
