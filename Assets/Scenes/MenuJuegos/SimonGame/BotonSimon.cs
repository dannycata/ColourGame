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
    [SerializeField] float resetDelay = .25f;
	private AudioClip sonido = null;
	[SerializeField] private AudioClip sound = null;
	[SerializeField] private AudioClip s_incorrect = null;
	[SerializeField] private AudioClip s_correct = null;
	[SerializeField] private Sprite i_correct = null;
	[SerializeField] private Sprite i_incorrect = null;
	public static bool condicionCumplida=false;

    private AudioSource audioSource = null;
	private Text texto;
    Button button;
	Image imagen;
	bool Press;
	string nombre;
	Image higlightColor;

    void Start()
    {
		nombre = PlayerPrefs.GetString("Nombre", "");
		texto = GetComponentInChildren<Text>();
		ColorBoton();
		sonido = sound;
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
	
	Color GetLighterColor(Color originalColor, float lightnessDelta)
    {
        // Convertir el color original a HSV
        Color.RGBToHSV(originalColor, out float h, out float s, out float v);
        v = Mathf.Clamp01(v + lightnessDelta);
        // Convertir de vuelta a RGB
        Color modifiedColor = Color.HSVToRGB(h, s, v);

        return modifiedColor;
    }

    public void Click()
    {
        StartCoroutine(ProcesarClick());
    }

    IEnumerator ProcesarClick()
    {
		condicionCumplida = false;
        StartCoroutine(gm.PlayersPick(ButtonIndex));

        if (GameManagerSimon.acierto)
		{
            imagen.sprite = i_correct;
			sonido = s_correct;
		}
        else 
		{
            imagen.sprite = i_incorrect;
			sonido = s_incorrect;
		}

        imagen.gameObject.SetActive(true);
		PressButton();
        yield return new WaitForSeconds(1f);
        condicionCumplida = true;
		imagen.gameObject.SetActive(false);
    }


    public void PressButton()
    {
        audioSource.PlayOneShot(sonido);
		sonido = sound;
		higlightColor.gameObject.SetActive(true);
        //button.image.color = highlightC;
        Invoke("ResetButton", resetDelay);
    }

    public void ResetButton()
    {
		higlightColor.gameObject.SetActive(false);
        //button.image.color = defaultC;
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
