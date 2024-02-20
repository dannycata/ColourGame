using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionInicio : MonoBehaviour
{
	[SerializeField] private GameObject logo;
	
	[SerializeField] private GameObject Inicio;
	
    private void Start()
    {
		int animacion = PlayerPrefs.GetInt("Animacion", 1);
		
		if(animacion == 0)
		{
			Inicio.GetComponent<CanvasGroup>().alpha = 0f;
			Inicio.GetComponent<CanvasGroup>().interactable = false;
			Inicio.GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
        LeanTween.moveX(logo.GetComponent<RectTransform>(), 0, 1.5f).setDelay(1.5f)
		.setEase(LeanTweenType.easeOutBounce).setOnComplete(BajarAlpha);
    }

    private void BajarAlpha()
    {
        LeanTween.alpha(Inicio.GetComponent<RectTransform>(), 0f, 1f).setDelay(0.5f);
		Inicio.GetComponent<CanvasGroup>().blocksRaycasts = false;
		PlayerPrefs.SetInt("Animacion", 0);
    }
}
