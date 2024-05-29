using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadInputPair : MonoBehaviour
{
	[SerializeField] private AudioClip sound = null;
	[SerializeField] private GameObject paneleliminartodo= null;
	[SerializeField] private GameObject panelresetear= null;
	[SerializeField] private Button borrarcancelar;
	[SerializeField] private Button resetear;
    private float velocidad;
	private int secuencias;
    private InputField inputFieldVelocidad;
	private InputField inputFieldSecuencia;
    private Text v_warning;
	private Text r_warning;
	private string nombre;
	
	[SerializeField] Toggle toggleButton;
	
	private AudioSource audioSource = null;
	
	public static float velocidadpair;
	public static int tinvisible;
	public static string pdimensiones;

    void Start()
    {
		paneleliminartodo.SetActive(false);
		panelresetear.SetActive(false);
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
		nombre = PlayerPrefs.GetString("Nombre", "");
        inputFieldVelocidad = GameObject.Find("InputFieldVelocidad").GetComponent<InputField>();
		inputFieldVelocidad.placeholder.GetComponent<Text>().text = PlayerPrefs.GetFloat(nombre+"VelocidadPair", 40f).ToString();
        v_warning = GameObject.Find("v_warning").GetComponent<Text>();
        v_warning.enabled = false;
		r_warning = GameObject.Find("r_warning").GetComponent<Text>();
        r_warning.enabled = true;
		toggleButton.isOn = PlayerPrefs.GetInt(nombre+"TiempoInvisibleParejas", 0) == 1;
		toggleButton.onValueChanged.AddListener(delegate { OnToggleChanged(toggleButton); });

        inputFieldVelocidad.onEndEdit.AddListener(Read);
		borrarcancelar.onClick.AddListener(OnClickCancelar);
		resetear.onClick.AddListener(OnClickResetear);
		GuardarPrefabs();
    }
	
	void Update()
	{
		string dimensiones = PlayerPrefs.GetString(nombre+"Dimensiones", "4x2");
		if (dimensiones == "4x2")
		{
			r_warning.text = "Recomendado 40 o mas";
			
		}
		else if (dimensiones == "6x2")
		{
			r_warning.text = "Recomendado 60 o mas";
			
		}
		else if (dimensiones == "6x3")
		{
			r_warning.text = "Recomendado 90 o mas";
			
		}
	}

    void Read(string inputValue)
    {
        if (!string.IsNullOrEmpty(inputValue))
		{
			if (float.TryParse(inputValue, out velocidad))
			{
				v_warning.enabled = false;
				r_warning.enabled = true;
				PlayerPrefs.SetFloat(nombre+"VelocidadPair", velocidad);
				Debug.Log(velocidad);
			}
			else
			{
				r_warning.enabled = false;
				v_warning.enabled = true;
			}
		}
    }
	
	public void OnToggleChanged(Toggle changedToggle)
    {
		int toggleState=0;
		if (changedToggle.isOn) toggleState = 1;
        PlayerPrefs.SetInt(nombre+"TiempoInvisibleParejas", toggleState);
    }
	
	public void GuardarPrefabs()
	{
		velocidadpair = PlayerPrefs.GetFloat(nombre+"VelocidadPair", 40f);
		tinvisible = PlayerPrefs.GetInt(nombre+"TiempoInvisibleParejas", 0);
		pdimensiones = PlayerPrefs.GetString(nombre+"Dimensiones", "4x2");
	}
	
	public void AsignarPrefabs(float velocpair, int tinv, string dim)
	{
		PlayerPrefs.SetFloat(nombre+"VelocidadPair", velocpair);
		PlayerPrefs.SetInt(nombre+"TiempoInvisibleParejas", tinv);
		PlayerPrefs.SetString(nombre+"Dimensiones", dim);
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
				boton.onClick.AddListener(OnClickSi);
            }
            else if (boton.name == "No")
            {
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
		AsignarPrefabs(velocidadpair,tinvisible,pdimensiones);
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
				boton.onClick.AddListener(OnClickSiReset);
            }
            else if (boton.name == "No")
            {
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
		AsignarPrefabs(40f,0,"4x2");
		SceneManager.LoadScene("MenuEditor");
	}
}
