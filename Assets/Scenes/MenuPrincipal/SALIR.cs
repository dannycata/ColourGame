using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SALIR : MonoBehaviour
{
	private Button BotonAspa;
	[SerializeField] private AudioClip sound = null;
	private GameObject panel = null;
	private Button BotonSi;
	private Button BotonNo;
	private AudioSource audioSource = null;
	
    void Start()
    {
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
		panel = GameObject.Find("PanelSalir");
		panel.SetActive(false);
		BotonSi = panel.transform.Find("Panel/Si").GetComponent<Button>();
		BotonNo = panel.transform.Find("Panel/No").GetComponent<Button>();
		BotonAspa = GetComponent<Button>();
        BotonAspa.onClick.AddListener(Click);
    }
	
	private void Click()
    {
		panel.SetActive(true);
		audioSource.PlayOneShot(sound);
		BotonSi.onClick.AddListener(Si);
		BotonNo.onClick.AddListener(No);
	}
	
	private void Si()
	{
		audioSource.PlayOneShot(sound);
		Invoke("Salir",0.25f);
	}
	
	private void No()
	{
		panel.SetActive(false);
		audioSource.PlayOneShot(sound);
	}
	
	private void Salir()
	{
		LimpiarPreferencias();
		Application.Quit();
	}

    private void LimpiarPreferencias()
    {
        PlayerPrefs.DeleteKey("Animacion");
        PlayerPrefs.Save();
    }
}
