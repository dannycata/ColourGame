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
	
	void Start()
	{
		boton = GetComponent<Button>();
        boton.onClick.AddListener(click);
	}
	
	public void click()
	{
		if(imagen.activeSelf && gameController.canOpen)
		{
			imagen.SetActive(false);
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
		imagen.SetActive(true);
	}
}
