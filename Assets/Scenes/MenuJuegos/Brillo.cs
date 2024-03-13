using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brillo : MonoBehaviour
{
    public Slider slider;
	public float value;
	public Image panelBrillo;
	
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Brillo",0f);
		float resta = 0.9f - slider.value;
		panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, resta);
    }
	
	public void Slider(float valor)
	{
		value = valor;
		PlayerPrefs.SetFloat("Brillo",value);
		float resta = 0.9f - slider.value;
		panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, resta);
	}
}
