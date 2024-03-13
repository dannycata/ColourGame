using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volumen : MonoBehaviour
{
    public Slider slider;
	public float value;
	public Image mute;
	
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Volumen",0.5f);
		AudioListener.volume = slider.value;
		Mute();
    }
	
	public void Slider(float valor)
	{
		value = valor;
		PlayerPrefs.SetFloat("Volumen",value);
		AudioListener.volume = slider.value;
		Mute();
	}

    void Mute()
    {
        if (value == 0)
		{
			mute.enabled=true;
		}
		else
		{
			mute.enabled=false;
		}
    }
}
