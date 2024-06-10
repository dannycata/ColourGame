using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScrollColores : MonoBehaviour
{
	[SerializeField] private AudioClip sound = null;
    [SerializeField] private GameObject estadistica;
	[SerializeField] private GameObject Botonescolores;
	[SerializeField] private Button borrar;
	[SerializeField] private Button crear;
	[SerializeField] private GameObject panelcrear = null;
	[SerializeField] private GameObject paneleliminartodo= null;
	[SerializeField] private GameObject panelmodificar= null;
    public Transform parentTransform;
	private string nombre;
	private GameObject panel = null;
	private Image panelBrillo;
	private float value;
	private string nombreDeLaEscenaActual;
	private string nombreModificado;
	[SerializeField] private GameObject noestadistica;
	[SerializeField] private Button borrarcrear;
	[SerializeField] private Button guardarcrear;
	[SerializeField] private Button borrarmodificar;
	[SerializeField] private Button guardarmodificar;
	[SerializeField] private GameObject errorMessage;
	SelectorColor[] objetosConSelector;
	public Toggle[] toggles;
	private Toggle lastActiveToggle;
	int activeIndex;
	string colorespregunta=null;
	string[] datos=null;
	int[] datoscorrecta=null;

	private AudioSource audioSource = null;
	
	Dictionary<int, Button> botoneseliminar = new Dictionary<int, Button>();
	Dictionary<int, Button> botonesmodificar = new Dictionary<int, Button>();
	Dictionary<int, Transform> padres = new Dictionary<int, Transform>();
	
    void Start()
    {
		objetosConSelector = FindObjectsOfType<SelectorColor>();
		nombreDeLaEscenaActual = SceneManager.GetActiveScene().name;
		panelcrear.SetActive(false);
		paneleliminartodo.SetActive(false);
		errorMessage.SetActive(false);
		panelmodificar.SetActive(false);
		Botonescolores.SetActive(false);
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
		
		nombre = PlayerPrefs.GetString("Nombre", "");
		
		string datosString = PlayerPrefs.GetString(nombre+"DatosPreguntas","");
        datos = datosString.Split(',');
		string datosIntCorrect = PlayerPrefs.GetString(nombre+"DatosPreguntasCorrecta","");
		string[] datosStringCorrect = datosIntCorrect.Split(',');
		datoscorrecta = new int[datosStringCorrect.Length];

		for (int i = 0; i < datosStringCorrect.Length; i++)
		{
			if (int.TryParse(datosStringCorrect[i], out int result))
			{
				datoscorrecta[i] = result;
			}
		}
		
		borrar.onClick.AddListener(OnClickBorrar);
		crear.onClick.AddListener(OnClickCrear);
		borrarcrear.onClick.AddListener(OnClickSalir);
		guardarcrear.onClick.AddListener(OnClickGuardar);
		
		if (!string.IsNullOrEmpty(datos[0]))
		{
			noestadistica.SetActive(false);
			estadistica.SetActive(true);
			
			Text texto = estadistica.GetComponentInChildren<Text>();
			texto.text = "Pregunta 1";
			
			Button eliminarPrincipal = estadistica.transform.Find("eliminar").GetComponent<Button>();
			Transform padrePrincipal = eliminarPrincipal.transform.parent;
			eliminarPrincipal.onClick.AddListener(() => eliminarpadre(padrePrincipal,0));
			
			Button modificarPrincipal = estadistica.transform.Find("modificar").GetComponent<Button>();
			modificarPrincipal.onClick.AddListener(() => modificarpadre(padrePrincipal,0));
			
			botoneseliminar.Add(0, eliminarPrincipal);
			botonesmodificar.Add(0, modificarPrincipal);
			padres.Add(0, padrePrincipal);
			
			Asignacolores(datos[0].Split('/'), estadistica);
			for (int i = 1; i < datos.Length; i++)
			{
				int indice = i;
				GameObject nuevaEstadistica = Instantiate(estadistica, parentTransform);
				
				Text nuevotexto = nuevaEstadistica.GetComponentInChildren<Text>();
				nuevotexto.text = "Pregunta " + (indice+1);
				
				Button eliminar = nuevaEstadistica.transform.Find("eliminar").GetComponent<Button>();
				Transform padre = eliminar.transform.parent;
				eliminar.onClick.AddListener(() => eliminarpadre(padre,indice));
				
				Button modificar = nuevaEstadistica.transform.Find("modificar").GetComponent<Button>();
				modificar.onClick.AddListener(() => modificarpadre(padre,indice));

				botoneseliminar.Add(indice, eliminar);
				botonesmodificar.Add(indice, modificar);
				padres.Add(indice, padre);
		
				Asignacolores(datos[indice].Split('/'), nuevaEstadistica);
			}
		}
		else
		{
			noestadistica.SetActive(true);
		}
		
		foreach (Toggle toggle in toggles)
        {
            toggle.onValueChanged.AddListener(delegate { OnToggleValueChanged(toggle); });
        }
    }
	
	private void Asignacolores(string[] coloresHex, GameObject estadistica)
	{
		Transform Colores = estadistica.transform.Find("Colores");
		Image[] imagenesHijas = Colores.GetComponentsInChildren<Image>();
		for (int j = 0; j < Mathf.Min(imagenesHijas.Length, coloresHex.Length); j++)
		{
			Color color;
			if (ColorUtility.TryParseHtmlString(coloresHex[j], out color))
			{
				imagenesHijas[j].color = color;
			}
		}
	}
	
	private void modificarpadre(Transform padre, int indice)
	{
		audioSource.PlayOneShot(sound);
		panelmodificar.SetActive(true);
		Botonescolores.SetActive(true);
		
		borrarmodificar.onClick.RemoveAllListeners();
		borrarmodificar.onClick.AddListener(OnClickSalirMod);
		guardarmodificar.onClick.RemoveAllListeners();
		guardarmodificar.onClick.AddListener(() => OnClickGuardarMod(padre,indice));
		string[] colores = datos[indice].Split('/');
		
		RefrescarToggle(datoscorrecta[indice]);
		for (int i=0 ; i<6 ; i++)
		{
			char variableOpcion = (char)('A' + i);
			PlayerPrefs.SetString(nombre + variableOpcion, colores[i]);
		}
		
        foreach (SelectorColor objeto in objetosConSelector)
        {
            objeto.RefrescaColor();
        }
	}
	
	private void OnClickSalirMod()
	{
		audioSource.PlayOneShot(sound);
		panelmodificar.SetActive(false);
		Botonescolores.SetActive(false);
	}
	
	private void OnClickGuardarMod(Transform padre, int indice)
	{
		audioSource.PlayOneShot(sound);
		colorespregunta=null;
		panelmodificar.SetActive(false);
		Botonescolores.SetActive(false);
		for (int i=0 ; i<6 ; i++)
		{
			char variableOpcion = (char)('A' + i);
			colorespregunta += PlayerPrefs.GetString(nombre + variableOpcion) + "/";
		}
		colorespregunta = colorespregunta.Remove(colorespregunta.Length - 1);
		
		datos[indice] = colorespregunta;
		datoscorrecta[indice] = activeIndex;
		string datosString = string.Join(",", datos);
		string datosIntCorrect = string.Join(",", datoscorrecta);
		
		PlayerPrefs.SetString(nombre + "DatosPreguntas", datosString);
		PlayerPrefs.SetString(nombre + "DatosPreguntasCorrecta", datosIntCorrect);
		PlayerPrefs.Save();
		//SceneManager.LoadScene("ListaPreguntas");
		SceneManager.LoadScene("EditorQuizColores");
	}
	
	private void eliminarpadre(Transform padre, int indice)
	{
		audioSource.PlayOneShot(sound);
		Destroy(padre.gameObject);

		for (int i = indice; i < datos.Length - 1; i++)
		{
			datos[i] = datos[i + 1];
			datoscorrecta[i] = datoscorrecta[i + 1];
		}
		
		Array.Resize(ref datos, datos.Length - 1);
		Array.Resize(ref datoscorrecta, datoscorrecta.Length - 1);
		string datosString = string.Join(",", datos);
		string datosIntCorrect = string.Join(",", datoscorrecta);
		PlayerPrefs.SetString(nombre + "DatosPreguntas", datosString);
		PlayerPrefs.SetString(nombre + "DatosPreguntasCorrecta", datosIntCorrect);
		PlayerPrefs.Save(); 
		//SceneManager.LoadScene("ListaPreguntas");
		SceneManager.LoadScene("EditorQuizColores");
	}
	
	private void OnClickCrear()
    {
		audioSource.PlayOneShot(sound);
		UpdateActiveIndex();
		panelcrear.SetActive(true);
		Botonescolores.SetActive(true);
		for (int i=0 ; i<6 ; i++)
		{
			char variableOpcion = (char)('A' + i);
			PlayerPrefs.DeleteKey(nombre + variableOpcion);
		}
		
        foreach (SelectorColor objeto in objetosConSelector)
        {
            objeto.RefrescaColor();
        }
    }
	
	private void OnClickSalir()
    {
		audioSource.PlayOneShot(sound);
		panelcrear.SetActive(false);
		Botonescolores.SetActive(false);
    }
	
	private void OnClickGuardar()
    {
		audioSource.PlayOneShot(sound);
		if (activeIndex == -1)
		{
			errorMessage.SetActive(true);
		}
		else
		{
			colorespregunta=null;
			panelcrear.SetActive(false);
			Botonescolores.SetActive(false);
			for (int i=0 ; i<6 ; i++)
			{
				char variableOpcion = (char)('A' + i);
				colorespregunta += PlayerPrefs.GetString(nombre + variableOpcion) + "/";
			}
			colorespregunta = colorespregunta.Remove(colorespregunta.Length - 1);
			string dato = PlayerPrefs.GetString(nombre+"DatosPreguntas","");
			string correcta = PlayerPrefs.GetString(nombre+"DatosPreguntasCorrecta","");
			if (dato == "")
			{
				dato = colorespregunta;
				correcta = activeIndex.ToString();
			}
			else
			{
				dato = PlayerPrefs.GetString(nombre+"DatosPreguntas","") + "," + colorespregunta;
				correcta = PlayerPrefs.GetString(nombre+"DatosPreguntasCorrecta","") + "," + activeIndex.ToString();
			}
			PlayerPrefs.SetString(nombre+"DatosPreguntas", dato);
			PlayerPrefs.SetString(nombre+"DatosPreguntasCorrecta", correcta);
			//SceneManager.LoadScene("ListaPreguntas");
			SceneManager.LoadScene("EditorQuizColores");
		}
    }
	
	private void OnToggleValueChanged(Toggle changedToggle)
    {
		audioSource.PlayOneShot(sound);
        if (changedToggle.isOn && changedToggle != lastActiveToggle)
        {
            if (lastActiveToggle != null)
            {
                lastActiveToggle.isOn = false;
            }
            lastActiveToggle = changedToggle;
			activeIndex = System.Array.IndexOf(toggles, changedToggle);
        }
    }
	
	private void RefrescarToggle(int indice)
    {
        for (int j = 0; j < toggles.Length; j++)
        {
            toggles[j].isOn = (j == indice);
        }
    }
	
	private void UpdateActiveIndex()
    {
        activeIndex = -1;
        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                activeIndex = i;
                break;
            }
        }
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
		PlayerPrefs.DeleteKey(nombre+"DatosPreguntas");
		PlayerPrefs.DeleteKey(nombre+"DatosPreguntasCorrecta");
		for (int i=0 ; i<6 ; i++)
		{
			char variableOpcion = (char)('A' + i);
			PlayerPrefs.DeleteKey(nombre + variableOpcion);
		}
		//SceneManager.LoadScene("ListaPreguntas");
		SceneManager.LoadScene("EditorQuizColores");
	}
}
