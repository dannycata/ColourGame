using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class BotonSimon : MonoBehaviour
{
    public int ButtonIndex { get; set; }
    [SerializeField] GameManagerSimon gm;
    [SerializeField] Color defaultColor;
	Color defaultC;
	[SerializeField] private AudioClip sound = null;

    private AudioSource audioSource = null;
	private float resetDelay;
	private Text texto;
    Button button;
	private Image imagen;
	string nombre;
	Image higlightColor;

    void Start()
    {
		nombre = PlayerPrefs.GetString("Nombre", "");
		texto = GetComponentInChildren<Text>();
		resetDelay = PlayerPrefs.GetFloat(nombre+"TiempoDestello", 0.25f);
		ColorBoton();
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
        button = GetComponent<Button>();
		Transform paneltransform = button.transform.Find("Panel");
		higlightColor = paneltransform.GetComponent<Image>();
		Transform imagenTransform = button.transform.Find("Image");
		imagen = imagenTransform.GetComponent<Image>();
		imagen.gameObject.SetActive(false);
        ResetButton();
		button.image.color = defaultC;
		button.onClick.AddListener(Click);
    }
	
	void ColorBoton()
	{
		string text = texto.text;
		if (text == "Verde")
		{
			defaultC = PlayerPrefs.HasKey(nombre + "VerdeSimon") ? HexToColor(PlayerPrefs.GetString(nombre + "VerdeSimon")) : defaultColor;
		}
		else if (text == "Rojo")
		{
			defaultC = PlayerPrefs.HasKey(nombre + "RojoSimon") ? HexToColor(PlayerPrefs.GetString(nombre + "RojoSimon")) : defaultColor;
		}
		else if (text == "Amarillo")
		{
			defaultC = PlayerPrefs.HasKey(nombre + "AmarilloSimon") ? HexToColor(PlayerPrefs.GetString(nombre + "AmarilloSimon")) : defaultColor;
		}
		else if (text == "Azul")
		{
			defaultC = PlayerPrefs.HasKey(nombre + "AzulSimon") ? HexToColor(PlayerPrefs.GetString(nombre + "AzulSimon")) : defaultColor;
		}
	}

    public void Click()
    {
        gm.PlayersPick(ButtonIndex, imagen);
		Press();
    }


    public void PressButton()
    {
        audioSource.PlayOneShot(sound);
		higlightColor.gameObject.SetActive(true);
        Invoke("ResetButton", resetDelay);
    }
	
	public void Press()
    {
        audioSource.PlayOneShot(sound);
		higlightColor.gameObject.SetActive(true);
        Invoke("ResetButton", 0.25f);
    }

    public void ResetButton()
    {
		higlightColor.gameObject.SetActive(false);
    }
	
	private Color HexToColor(string hex)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }
        else
        {
            Debug.LogWarning("Invalid hexadecimal color: " + hex);
            return Color.white;
        }
    }
}
