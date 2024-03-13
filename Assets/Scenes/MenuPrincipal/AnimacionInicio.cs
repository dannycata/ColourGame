using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimacionInicio : MonoBehaviour
{
	[SerializeField] private GameObject logo;
	[SerializeField] private GameObject Inicio;
	[SerializeField] private AudioClip sound = null;
	
	private AudioSource audioSource = null;
	
    private void Start()
    {
		
		int animacion = PlayerPrefs.GetInt("Animacion", 1);
		Camera mainCamera = Camera.main;
		audioSource = mainCamera.GetComponent<AudioSource>();
		audioSource.clip = sound;
		if(animacion == 0)
		{
			Inicio.GetComponent<CanvasGroup>().alpha = 0f;
			Inicio.GetComponent<CanvasGroup>().interactable = false;
			Inicio.GetComponent<CanvasGroup>().blocksRaycasts = false;
		}else{
			//StartCoroutine(PlayConDelay(0.3f));
			audioSource.Play();
			logo.transform.localScale = Vector3.zero;
			LeanTween.scale(logo, new Vector3(2, 2, 2), 1f)
			.setEase(LeanTweenType.easeOutBounce).setOnComplete(BajarAlpha);
			//LeanTween.moveX(logo.GetComponent<RectTransform>(), 0, 1.5f).setDelay(1.5f)
			//.setEase(LeanTweenType.easeOutBounce).setOnComplete(BajarAlpha);
		}
    }

    private void BajarAlpha()
    {
        LeanTween.alpha(Inicio.GetComponent<RectTransform>(), 0f, 1f).setDelay(0.5f);
		Inicio.GetComponent<CanvasGroup>().blocksRaycasts = false;
		PlayerPrefs.SetInt("Animacion", 0);
    }
	
	IEnumerator PlayConDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.Play();
    }
}
