using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CartaScript : MonoBehaviour
{
    [SerializeField] private GameObject imagen;
	[SerializeField] private GameController gameController;
	private int _ID;
	private Button boton;
	
	private AudioClip sonido = null;
	[SerializeField] private AudioClip sound = null;
	[SerializeField] private AudioClip s_incorrect = null;
	[SerializeField] private AudioClip s_correct = null;
	private Image simbol;
	[SerializeField] private Sprite i_correct = null;
	[SerializeField] private Sprite i_incorrect = null;
	[SerializeField] private Sprite interrogation = null;
	[SerializeField] private GameObject invisible = null;
	
	private AudioSource audioSource = null;
	
	void Start()
	{
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
		invisible.SetActive(false);
		sonido = sound;
		boton = GetComponent<Button>();
        boton.onClick.AddListener(click);
		Transform imagenTransform = boton.transform.Find("Simbol");
		simbol = imagenTransform.GetComponent<Image>();
	}
	
	public void click()
	{
		audioSource.PlayOneShot(sonido);
		if(imagen.activeSelf && gameController.canOpen)
		{
			imagen.SetActive(false);
			simbol.gameObject.SetActive(false);
			gameController.imageOpened(this);
		}
	}
	
	public int ID
	{
		get {return _ID;}
	}
	
	public void CambiaSprite(int id, Color color)
	{
		_ID = id;
		GetComponent<Image>().color = color;
	}
	
	public void Close()
	{
		simbol.sprite = interrogation;
		imagen.SetActive(true);
		invisible.SetActive(false);
	}
	
	public void Acierto()
	{
		sonido = s_correct;
		simbol.sprite = i_correct;
		simbol.gameObject.SetActive(true);
		audioSource.PlayOneShot(sonido);
		sonido = sound;
	}
	
	public void Fallo()
	{
		sonido = s_incorrect;
		simbol.sprite = i_incorrect;
		simbol.gameObject.SetActive(true);
		invisible.SetActive(true);
		audioSource.PlayOneShot(sonido);
		sonido = sound;
		Invoke("Close",2f);
	}
}
