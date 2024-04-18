using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Scroll : MonoBehaviour
{
	[SerializeField] private AudioClip sound = null;
    [SerializeField] private GameObject estadistica;
	[SerializeField] private Button borrar;
    public Transform parentTransform;
	private string nombre;
	private GameObject panel = null;
	private Image panelBrillo;
	private float value;
	private string nombreDeLaEscenaActual;
	private string nombreModificado;
	[SerializeField] private GameObject noestadistica;

	private AudioSource audioSource = null;
	
    void Start()
    {
		nombreDeLaEscenaActual = SceneManager.GetActiveScene().name;
		nombreModificado = nombreDeLaEscenaActual.Replace("Estadisticas", "");
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
		
		string datosString = PlayerPrefs.GetString(nombre+"Datos"+nombreModificado,"");
        string[] datos = datosString.Split(',');
		
		if (borrar != null)
        {
            borrar.onClick.AddListener(OnClickBoton);
        }
		
		if (!string.IsNullOrEmpty(datos[0]))
		{
			noestadistica.SetActive(false);
			estadistica.SetActive(true);
			Text texto = estadistica.GetComponentInChildren<Text>();
			texto.text = datos[0];
			for (int i = 1; i < datos.Length-1; i++)
			{
				GameObject nuevaEstadistica = Instantiate(estadistica, parentTransform);
				
				Text nuevotexto = nuevaEstadistica.GetComponentInChildren<Text>();
				nuevotexto.text = datos[i];
			}
		}
		else
		{
			noestadistica.SetActive(true);
		}
    }
	
	private void OnClickBoton()
    {
		audioSource.PlayOneShot(sound);
		Invoke("Borrar",0.25f);
    }
	
	private void Borrar()
	{
		PlayerPrefs.DeleteKey(nombre+"Datos"+nombreModificado);
		SceneManager.LoadScene("Estadisticas"+nombreModificado);
	}
}
