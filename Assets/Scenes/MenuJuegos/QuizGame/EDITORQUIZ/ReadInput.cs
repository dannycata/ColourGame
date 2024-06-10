using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadInput : MonoBehaviour
{
	[SerializeField] private AudioClip sound = null;
	[SerializeField] private GameObject paneleliminartodo= null;
	[SerializeField] private GameObject panelresetear= null;
    private float tiempo;
	private int preguntas;
	[SerializeField] private Button borrarcancelar;
	[SerializeField] private Button resetear;
    private InputField inputFieldTiempo;
	private InputField inputFieldPregunta;
    private Text t_warning;
	private Text q_warning;
	private string nombre;
	private string[] datos;
	private string[] colores;
	private int maximo;
	[SerializeField] Text minmax;
	[SerializeField] Toggle toggleButton;
	
	private AudioSource audioSource = null;
	
	public static float variabletiempo;
	public static int tiempoinvisible;
	public static string datospreguntas;
	public static string prefabcolores;
	public static int npreguntas;
	public static string datospreguntascorrecta;

    void Start()
    {
		paneleliminartodo.SetActive(false);
		panelresetear.SetActive(false);
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
		nombre = PlayerPrefs.GetString("Nombre", "");
        inputFieldTiempo = GameObject.Find("InputFieldTiempo").GetComponent<InputField>();
		inputFieldPregunta = GameObject.Find("InputFieldPregunta").GetComponent<InputField>();
		inputFieldTiempo.placeholder.GetComponent<Text>().text = PlayerPrefs.GetFloat(nombre+"VariableTiempo", 5f).ToString();
        t_warning = GameObject.Find("t_warning").GetComponent<Text>();
		q_warning = GameObject.Find("q_warning").GetComponent<Text>();
        t_warning.enabled = false;
		q_warning.enabled = false;
		toggleButton.isOn = PlayerPrefs.GetInt(nombre+"TiempoInvisible", 0) == 1;
		toggleButton.onValueChanged.AddListener(delegate { OnToggleChanged(toggleButton); });
		
		string datosString = PlayerPrefs.GetString(nombre+"DatosPreguntas","");
        datos = datosString.Split(',');
		string datoscolores = PlayerPrefs.GetString(nombre+"Colores", "Rojo,Amarillo,Azul,Verde,Rosa");
		colores = datoscolores.Split(',');
		if (datosString != "" && datoscolores != "") maximo = (colores.Length * 6) + datos.Length;
		else if (datosString != "" && datoscolores == "") maximo = datos.Length;
		else maximo = (colores.Length * 12);
		minmax.text = "Min 1 - Max "+maximo;
		
		if (PlayerPrefs.GetInt(nombre+"NPreguntas", 5) <= maximo)
		{
			inputFieldPregunta.placeholder.GetComponent<Text>().text = PlayerPrefs.GetInt(nombre+"NPreguntas", 5).ToString();
		}
		else
		{
			inputFieldPregunta.placeholder.GetComponent<Text>().text = maximo.ToString();
			PlayerPrefs.SetInt(nombre+"NPreguntas", maximo);
		}

        inputFieldTiempo.onEndEdit.AddListener(Read);
		inputFieldPregunta.onEndEdit.AddListener(Read2);
		borrarcancelar.onClick.AddListener(OnClickCancelar);
		resetear.onClick.AddListener(OnClickResetear);
		GuardarPrefabs();
    }

    void Read(string inputValue)
    {
        if (!string.IsNullOrEmpty(inputValue))
		{
			if (float.TryParse(inputValue, out tiempo))
			{
				t_warning.enabled = false;
				PlayerPrefs.SetFloat(nombre+"VariableTiempo", tiempo);
			}
			else
			{
				t_warning.enabled = true;
			}
		}
    }
	
	void Read2(string inputValue)
    {
		if (!string.IsNullOrEmpty(inputValue))
		{
			if (int.TryParse(inputValue, out int result))
			{
				preguntas = result;
				q_warning.text = "Error: Fuera de rango";
				q_warning.enabled = false;
				if (preguntas >= 1 && preguntas <= maximo) {
					PlayerPrefs.SetInt(nombre+"NPreguntas", preguntas);
				} else q_warning.enabled = true;
			}
			else
			{
				q_warning.text = "Error: Inserta un numero";
				q_warning.enabled = true;
			}
		}
    }
	
	public void OnToggleChanged(Toggle changedToggle)
    {
		int toggleState=0;
		if (changedToggle.isOn) toggleState = 1;
        PlayerPrefs.SetInt(nombre+"TiempoInvisible", toggleState);
    }
	
	public void GuardarPrefabs()
	{
		if (PlayerPrefs.GetInt("Actualizar", 1) == 1)
		{
			PlayerPrefs.SetInt("Actualizar", 0);
			variabletiempo = PlayerPrefs.GetFloat(nombre+"VariableTiempo", 5f);
			tiempoinvisible = PlayerPrefs.GetInt(nombre+"TiempoInvisible", 0);
			datospreguntas = PlayerPrefs.GetString(nombre+"DatosPreguntas","");
			prefabcolores = PlayerPrefs.GetString(nombre+"Colores", "Rojo,Amarillo,Azul,Verde,Rosa");
			npreguntas = PlayerPrefs.GetInt(nombre+"NPreguntas", 5);
			datospreguntascorrecta = PlayerPrefs.GetString(nombre+"DatosPreguntasCorrecta","");
		}
	}
	
	public void AsignarPrefabs(float vtiempo, int tinv, string dpreg, string cols, int npreg, string dpregc)
	{
		PlayerPrefs.SetInt("Actualizar", 1);
		PlayerPrefs.SetFloat(nombre+"VariableTiempo", vtiempo);
		PlayerPrefs.SetInt(nombre+"TiempoInvisible", tinv);
		PlayerPrefs.SetString(nombre+"DatosPreguntas", dpreg);
		PlayerPrefs.SetString(nombre+"Colores", cols);
		PlayerPrefs.SetInt(nombre+"NPreguntas", npreg);
		PlayerPrefs.SetString(nombre+"DatosPreguntasCorrecta", dpregc);
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
		AsignarPrefabs(variabletiempo,tiempoinvisible,datospreguntas,prefabcolores,npreguntas,datospreguntascorrecta);
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
		AsignarPrefabs(5f,0,"","Rojo,Amarillo,Azul,Verde,Rosa",5,"");
		SceneManager.LoadScene("MenuEditor");
	}
}
