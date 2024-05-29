using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class ScrollNombres : MonoBehaviour
{
	[SerializeField] private AudioClip sound = null;
    [SerializeField] private GameObject estadistica;
	[SerializeField] private Button borrar;
	[SerializeField] private GameObject panelmodificar= null;
	[SerializeField] private GameObject paneleliminartodo= null;
	[SerializeField] private InputField InputNombre;
	[SerializeField] private InputField InputBuscar;
    public Transform parentTransform;
	private GameObject panel = null;
	private Image panelBrillo;
	private float value;
	private string nombreDeLaEscenaActual;
	private string nombreModificado;
	[SerializeField] private GameObject noestadistica;
	[SerializeField] private Button borrarmodificar;
	[SerializeField] private Button guardarmodificar;
	SelectorColor[] objetosConSelector;
	int activeIndex;
	public static string jugador;
	string[] datos=null;

	private AudioSource audioSource = null;
	
	Dictionary<int, Button> botoneseliminar = new Dictionary<int, Button>();
	Dictionary<int, Button> botonesentrar = new Dictionary<int, Button>();
	Dictionary<int, Button> botonesmodificar = new Dictionary<int, Button>();
	Dictionary<int, Transform> padres = new Dictionary<int, Transform>();
	
    void Start()
    {
		objetosConSelector = FindObjectsOfType<SelectorColor>();
		nombreDeLaEscenaActual = SceneManager.GetActiveScene().name;
		panelmodificar.SetActive(false);
		paneleliminartodo.SetActive(false);
		estadistica.SetActive(false);
		panel = GameObject.Find("PanelBrillo");
		panelBrillo = panel.GetComponent<Image>();
		value = PlayerPrefs.GetFloat("Brillo",0.9f);
		float resta = 0.9f - value;
		panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, resta);
		value = PlayerPrefs.GetFloat("Volumen",0.5f);
		AudioListener.volume = value;
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
		
		string datosString = PlayerPrefs.GetString("NombresJugadores","");
        datos = datosString.Split(',');
		datos = datos.Where(s => !string.IsNullOrEmpty(s)).ToArray();
		
		borrar.onClick.AddListener(OnClickBorrar);
		
		if (!string.IsNullOrEmpty(datos[0]))
		{
			noestadistica.SetActive(false);
			estadistica.SetActive(true);
			
			Text texto = estadistica.GetComponentInChildren<Text>();
			texto.text = "1. " + datos[0];
			
			Button eliminarPrincipal = estadistica.transform.Find("eliminar").GetComponent<Button>();
			Transform padrePrincipal = eliminarPrincipal.transform.parent;
			eliminarPrincipal.onClick.AddListener(() => eliminarpadre(padrePrincipal,0));
			
			Button entrarPrincipal = estadistica.transform.Find("BotonEntrar").GetComponent<Button>();
			entrarPrincipal.onClick.AddListener(() => EntrarJuego(padrePrincipal,0));
			
			Button modificarPrincipal = estadistica.transform.Find("modificar").GetComponent<Button>();
			modificarPrincipal.onClick.AddListener(() => modificarpadre(padrePrincipal,0));
			
			botoneseliminar.Add(0, eliminarPrincipal);
			botonesentrar.Add(0, entrarPrincipal);
			botonesmodificar.Add(0, modificarPrincipal);
			padres.Add(0, padrePrincipal);
			
			for (int i = 1; i < datos.Length; i++)
			{
				int indice = i;
				GameObject nuevaEstadistica = Instantiate(estadistica, parentTransform);
				
				Text nuevotexto = nuevaEstadistica.GetComponentInChildren<Text>();
				nuevotexto.text = "" + (indice+1) + ". " + datos[indice];
				
				Button eliminar = nuevaEstadistica.transform.Find("eliminar").GetComponent<Button>();
				Transform padre = eliminar.transform.parent;
				eliminar.onClick.AddListener(() => eliminarpadre(padre,indice));
				
				Button entrar = nuevaEstadistica.transform.Find("BotonEntrar").GetComponent<Button>();
				entrar.onClick.AddListener(() => EntrarJuego(padre,indice));
				
				Button modificar = nuevaEstadistica.transform.Find("modificar").GetComponent<Button>();
				modificar.onClick.AddListener(() => modificarpadre(padre,indice));

				botoneseliminar.Add(indice, eliminar);
				botonesentrar.Add(indice, entrar);
				botonesmodificar.Add(indice, modificar);
				padres.Add(indice, padre);
			}
		}
		else
		{
			noestadistica.SetActive(true);
		}
		
		InputBuscar.onValueChanged.AddListener(Buscar);
    }
	
	private void EntrarJuego(Transform padre, int indice)
	{
		audioSource.PlayOneShot(sound);
		PlayerPrefs.SetString("Nombre", datos[indice]);
		SceneManager.LoadScene("MenuPrincipal");
	}
	
	private void modificarpadre(Transform padre, int indice)
	{
		audioSource.PlayOneShot(sound);
		panelmodificar.SetActive(true);
		
		borrarmodificar.onClick.AddListener(OnClickSalirMod);
		guardarmodificar.onClick.AddListener(() => OnClickGuardarMod(padre,indice));
		InputNombre.placeholder.GetComponent<Text>().text = datos[indice].ToString();
	}
	
	private void OnClickSalirMod()
	{
		audioSource.PlayOneShot(sound);
		panelmodificar.SetActive(false);
	}
	
	private void OnClickGuardarMod(Transform padre, int indice)
	{
		audioSource.PlayOneShot(sound);
		panelmodificar.SetActive(false);
		
		string textoInput = InputNombre.text;
		if (string.IsNullOrEmpty(textoInput))
		{
			textoInput = InputNombre.placeholder.GetComponent<Text>().text;
		}
		
		if (datos[indice] != textoInput)
		{
			RenombrarPrefs(textoInput, datos[indice]);
			BorrarPrefs(datos[indice]);
		}
		
		datos[indice] = textoInput;
		
		string datosString = string.Join(",", datos);
		
		PlayerPrefs.SetString("NombresJugadores", datosString);
		PlayerPrefs.Save();
		SceneManager.LoadScene("ListaNombres");
	}
	
	private void eliminarpadre(Transform padre, int indice)
	{
		audioSource.PlayOneShot(sound);
		Destroy(padre.gameObject);
		BorrarPrefs(datos[indice]);
		
		for (int i = indice; i < datos.Length - 1; i++)
		{
			datos[i] = datos[i + 1];
		}
		
		Array.Resize(ref datos, datos.Length - 1);
		string datosString = string.Join(",", datos);
		PlayerPrefs.SetString("NombresJugadores", datosString);
		PlayerPrefs.Save();
		SceneManager.LoadScene("ListaNombres");
	}
	
	void BorrarPrefs(string nombre)
	{
		PlayerPrefs.DeleteKey(nombre+"NSecuencias");
		PlayerPrefs.DeleteKey(nombre+"TiempoDestello");
		PlayerPrefs.DeleteKey(nombre+"VelocidadSimon");
		PlayerPrefs.DeleteKey(nombre+"VerdeSimon");
		PlayerPrefs.DeleteKey(nombre+"AmarilloSimon");
		PlayerPrefs.DeleteKey(nombre+"RojoSimon");
		PlayerPrefs.DeleteKey(nombre+"AzulSimon");
		PlayerPrefs.DeleteKey(nombre+"Dimensiones");
		PlayerPrefs.DeleteKey(nombre+"VelocidadPair");
		PlayerPrefs.DeleteKey(nombre+"TiempoInvisibleParejas");
		PlayerPrefs.DeleteKey(nombre+"DatosQuiz");
		PlayerPrefs.DeleteKey(nombre+"DatosPair");
		PlayerPrefs.DeleteKey(nombre+"DatosSimon");
		PlayerPrefs.DeleteKey(nombre+"DatosPreguntas");
		PlayerPrefs.DeleteKey(nombre+"Colores");
		PlayerPrefs.DeleteKey(nombre+"VariableTiempo");
		PlayerPrefs.DeleteKey(nombre+"TiempoInvisible");
		PlayerPrefs.DeleteKey(nombre+"DatosPreguntasCorrecta");
		PlayerPrefs.DeleteKey(nombre+"NPreguntas");
	}
	
    void RenombrarPrefs(string jugador, string nombre)
    {
        PlayerPrefsEx.SetIfNotZeroOrNull(jugador + "NSecuencias", PlayerPrefs.GetInt(nombre + "NSecuencias"));
        PlayerPrefsEx.SetIfNotZeroOrNull(jugador + "VelocidadSimon", PlayerPrefs.GetFloat(nombre + "VelocidadSimon"));
        PlayerPrefsEx.SetIfNotNullOrEmpty(jugador + "VerdeSimon", PlayerPrefs.GetString(nombre + "VerdeSimon"));
        PlayerPrefsEx.SetIfNotNullOrEmpty(jugador + "AmarilloSimon", PlayerPrefs.GetString(nombre + "AmarilloSimon"));
        PlayerPrefsEx.SetIfNotNullOrEmpty(jugador + "RojoSimon", PlayerPrefs.GetString(nombre + "RojoSimon"));
        PlayerPrefsEx.SetIfNotNullOrEmpty(jugador + "AzulSimon", PlayerPrefs.GetString(nombre + "AzulSimon"));
        PlayerPrefsEx.SetIfNotNullOrEmpty(jugador + "Dimensiones", PlayerPrefs.GetString(nombre + "Dimensiones"));
        PlayerPrefsEx.SetIfNotZeroOrNull(jugador + "VelocidadPair", PlayerPrefs.GetFloat(nombre + "VelocidadPair"));
        PlayerPrefsEx.SetIfNotZeroOrNull(jugador + "TiempoInvisibleParejas", PlayerPrefs.GetInt(nombre + "TiempoInvisibleParejas"));
        PlayerPrefsEx.SetIfNotNullOrEmpty(jugador + "DatosQuiz", PlayerPrefs.GetString(nombre + "DatosQuiz"));
        PlayerPrefsEx.SetIfNotNullOrEmpty(jugador + "DatosPair", PlayerPrefs.GetString(nombre + "DatosPair"));
        PlayerPrefsEx.SetIfNotNullOrEmpty(jugador + "DatosSimon", PlayerPrefs.GetString(nombre + "DatosSimon"));
        PlayerPrefsEx.SetIfNotNullOrEmpty(jugador + "DatosPreguntas", PlayerPrefs.GetString(nombre + "DatosPreguntas"));
        PlayerPrefsEx.SetIfNotNullOrEmpty(jugador + "Colores", PlayerPrefs.GetString(nombre + "Colores"));
        PlayerPrefsEx.SetIfNotZeroOrNull(jugador + "VariableTiempo", PlayerPrefs.GetFloat(nombre + "VariableTiempo"));
        PlayerPrefsEx.SetIfNotZeroOrNull(jugador + "TiempoInvisible", PlayerPrefs.GetInt(nombre + "TiempoInvisible"));
        PlayerPrefsEx.SetIfNotNullOrEmpty(jugador + "DatosPreguntasCorrecta", PlayerPrefs.GetString(nombre + "DatosPreguntasCorrecta"));
        PlayerPrefsEx.SetIfNotZeroOrNull(jugador + "NPreguntas", PlayerPrefs.GetInt(nombre + "NPreguntas"));
        PlayerPrefsEx.SetIfNotZeroOrNull(jugador + "TiempoDestello", PlayerPrefs.GetFloat(nombre + "TiempoDestello"));
    }
	
	private void OnClickBorrar()
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
		PlayerPrefs.DeleteKey("NombresJugadores");
		SceneManager.LoadScene("ListaNombres");
	}
	
	void Buscar(string filtro)
    {
		if (!string.IsNullOrEmpty(filtro))
		{
			foreach (Transform child in parentTransform)
			{
				child.gameObject.SetActive(false);
			}

			for (int i = 0; i < datos.Length; i++)
			{
				if (datos[i].ToLower().StartsWith(filtro.ToLower())) //datos[i].ToLower().Contains(filtro.ToLower())
				{
					int indice = i;

					if (padres.ContainsKey(indice))
					{
						Transform padre = padres[indice];
						padre.gameObject.SetActive(true);
					}
				}
			}
		}
		else
		{
			foreach (Transform child in parentTransform)
			{
				if (child.name != "NoEstadistica")
				{
					child.gameObject.SetActive(true);
				}
			}
			noestadistica.SetActive(false);
		}
    }
}

public static class PlayerPrefsEx
{
    public static void SetIfNotZeroOrNull(string key, int value)
    {
        if (value != 0)
        {
            PlayerPrefs.SetInt(key, value);
        }
    }

    public static void SetIfNotZeroOrNull(string key, float value)
    {
        if (value != 0f)
        {
            PlayerPrefs.SetFloat(key, value);
        }
    }

    public static void SetIfNotNullOrEmpty(string key, string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            PlayerPrefs.SetString(key, value);
        }
    }
}
